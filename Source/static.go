package main

import "github.com/gorilla/websocket"

type DbFindResultStruct struct {
	Nickname string `bson:"Nickname"`
	Password string `bson:"Password"`
}

var endBlockLog = "log----------------------------------------"
var endBlockReg = "reg----------------------------------------"

type resultStruct struct {
	Commandtype string
	Nickname    string
	Password    string
	Message     string
	SendMessage string
}

type config struct {
	IP           string
	Port         string
	UIFolderName string
	MongoIP      string
	MongoPort    string
}

type RecMSG struct {
	Commandtype string
	SendMessage string
	NickName    string
}

type NewHTML struct {
	Commandtype string
	Resp        string
	NewPage     string
}

type InsertStruct struct {
	Nickname string `bson:"Nickname"`
	Password string `bson:"Password"`
}

var upgrader = websocket.Upgrader{
	ReadBufferSize:  1024,
	WriteBufferSize: 1024,
}
