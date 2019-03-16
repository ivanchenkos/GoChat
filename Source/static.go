package main

import "github.com/gorilla/websocket"

type DbFindResultStruct struct {
	Nickname string `bson:"Nickname"`
	Password string `bson:"Password"`
}

type resultStruct struct {
	Commandtype string
	Nickname    string
	Password    string
	Message     string
	SendMessage string
}

type config struct {
	IP                  string
	Port                string
	UIFolderName        string
	MongoIP             string
	MongoPort           string
	IgnoreAckProtection string
}

type RecMSG struct {
	Commandtype string
	SendMessage string
	NickName    string
}

type NewHTML struct {
	Commandtype string
	Resp        string
}

type InsertStruct struct {
	Nickname string `bson:"Nickname"`
	Password string `bson:"Password"`
}

type InsertMsgStruct struct {
	Nickname string `bson:"Nickname"`
	Message  string `bson:"Message"`
}

var upgrader = websocket.Upgrader{
	ReadBufferSize:  1024,
	WriteBufferSize: 1024,
}
