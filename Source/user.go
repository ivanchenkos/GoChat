package main

import (
	"fmt"
	"time"

	"github.com/gorilla/websocket"
)

var Users = make([]*User, 0)

type User struct {
	Connection *websocket.Conn
	Nickname   string
}

func (user *User) pingUser() {
	fmt.Println("Начата проверка для :", user.Nickname)
	defer fmt.Println("Подключение закрыто для :", user.Nickname)

	for {
		timer := time.NewTimer(10 * time.Second)
		<-timer.C
		if err := user.Connection.WriteControl(websocket.PingMessage, []byte{}, time.Now().Add(10*time.Second)); err != nil {
			user.Connection.Close()
			for index, usr := range Users {
				if usr == user {
					Users = deleteUser(index, Users)
				}
			}
			break
		}
	}
}
func deleteUser(idx int, usrs []*User) []*User {
	return append(usrs[:idx], usrs[idx+1:]...)
}
