package main

import (
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net"
	"net/http"

	"github.com/gorilla/websocket"

	"github.com/mongodb/mongo-go-driver/bson"

	"github.com/mongodb/mongo-go-driver/mongo"
)

var mongoClient *mongo.Client
var collection *mongo.Collection
var collectionMsgs *mongo.Collection
var errorMongoConnect error

func main() {
	go controller()
	// configuration
	//
	configByte, _ := ioutil.ReadFile("../config.json")
	configJSON := new(config)
	if err := json.Unmarshal(configByte, configJSON); err != nil {
		fmt.Println(err)
	}
	//
	http.Handle("/", http.StripPrefix("/", http.FileServer(http.Dir("../"+configJSON.UIFolderName+"/"))))
	http.HandleFunc("/reg", func(w http.ResponseWriter, r *http.Request) {
		conn, _ := upgrader.Upgrade(w, r, nil) // error ignored for sake of simplicity
		// connecting to mongo data base
		if configJSON.IgnoreAckProtection == "n" {
			mongoClient, errorMongoConnect = mongo.Connect(context.TODO(), "mongodb://"+configJSON.MongoIP+":"+configJSON.MongoPort)
			if errorMongoConnect != nil {
				log.Fatal(errorMongoConnect)
			}
		}

		for {
			// Check the connection

			if configJSON.IgnoreAckProtection == "n" {
				if err := mongoClient.Ping(context.TODO(), nil); err != nil {
					log.Fatal(err)
				}
				collection = mongoClient.Database("GoChat").Collection("Users")
				collectionMsgs = mongoClient.Database("GoChat").Collection("Messages")
			}

			// Read message from browser
			_, msg, err := conn.ReadMessage()
			if err != nil {
				return
			}

			var result = &resultStruct{}

			if ResultErr := json.Unmarshal(msg, result); ResultErr != nil {
				log.Println(ResultErr)
			}
			if result.Commandtype == "msg" {
				for _, usr := range Users {
					if usr.Connection == conn {
						msgs(usr, result.SendMessage)
						send(result.SendMessage, conn)
						if configJSON.IgnoreAckProtection == "n" {
							Msgs := InsertMsgStruct{usr.Nickname, result.SendMessage}
							collectionMsgs.InsertOne(context.TODO(), Msgs)
						}

					}
				}

			}
			if result.Commandtype == "reg" {
				if configJSON.IgnoreAckProtection == "n" {
					DbFindResult := new(DbFindResultStruct)
					regg(result)
					filter := bson.D{{"Nickname", result.Nickname}}
					CollErr := collection.FindOne(context.TODO(), filter).Decode(DbFindResult)
					if CollErr != nil {
						insertStr := InsertStruct{result.Nickname, result.Password}
						regSucces()
						collection.InsertOne(context.TODO(), insertStr)
					} else {
						regFailure()
					}
				}

			}
			if result.Commandtype == "log" {

				if configJSON.IgnoreAckProtection == "y" {
					logSucces()

					pgsend, _ := json.Marshal(NewHTML{"log", "sucs"})

					newuser := &User{conn, result.Nickname}
					go newuser.pingUser()
					Users = append(Users, newuser)

					conn.WriteMessage(1, pgsend)
				}
				if configJSON.IgnoreAckProtection == "n" {
					DbFindResult := new(DbFindResultStruct)
					logg(result)
					filter := bson.D{{"Nickname", result.Nickname}, {"Password", result.Password}}
					CollErr := collection.FindOne(context.TODO(), filter).Decode(DbFindResult)
					if CollErr != nil {

						logFailure()

					} else {

						logSucces()

						pgsend, _ := json.Marshal(NewHTML{"log", "sucs"})

						newuser := &User{conn, DbFindResult.Nickname}
						go newuser.pingUser()
						Users = append(Users, newuser)

						conn.WriteMessage(1, pgsend)
					}
				}

			}
		}
	})
	fmt.Println(configJSON)
	fmt.Println("Сервер запущен на " + configJSON.IP + ":" + configJSON.Port)
	http.ListenAndServe(configJSON.IP+":"+configJSON.Port, nil)
	net.Listen("tcp", configJSON.IP+":"+configJSON.Port)

}
func send(message string, conn *websocket.Conn) {
	// Write message back to browser
	for _, User := range Users {
		if User.Connection != nil {

			var userNickname string
			for _, usr := range Users {
				if usr.Connection == conn {
					userNickname = usr.Nickname
				}
			}

			msg, _ := json.Marshal(RecMSG{"msg", message, userNickname})
			if err := User.Connection.WriteMessage(websocket.TextMessage, msg); err != nil {

			}
		} else {
			fmt.Println("Can't send message, connections is empty")
		}

	}
}
func sendFromServerConsole(message string) {
	// Write message back to browser
	for _, User := range Users {
		if User.Connection != nil {

			msg, _ := json.Marshal(RecMSG{"msg", message, "Server"})

			if err := User.Connection.WriteMessage(websocket.TextMessage, msg); err != nil {
				fmt.Println(err)
			}
		} else {
			fmt.Println("Can't send message, connections is empty")
		}

	}
}
