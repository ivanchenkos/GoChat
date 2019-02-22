package main

import "fmt"

func regSucces() {
	fmt.Println("[REG]--Аккаунт не обнаружен, внесение нового аккаунта в базу данных")
}
func regFailure() {
	fmt.Println("[REG]--Такой аккаунт уже существует")
}
func regg(result *resultStruct) {
	fmt.Println("[REG]--Запрос на регистрацию :" + result.Nickname + "/" + result.Password)
}
func logSucces() {
	fmt.Println("[LOG]--Успешный вход")
}
func logFailure() {
	fmt.Println("[LOG]--Аккаунт не найден")
}
func logg(result *resultStruct) {
	fmt.Println("[LOG]--Запрос на вход :" + result.Nickname + "/" + result.Password)
}
func msgs(user *User, message string) {
	fmt.Println("[MSG]--" + user.Nickname + " : " + message)
}
