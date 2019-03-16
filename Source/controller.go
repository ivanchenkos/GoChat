package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func controller() {
	for {
		fmt.Println("Введите команду")
		input := scan()

		cm, cn := readCommand(input)

		switch cm {
		case "-wr":
			go sendFromServerConsole(cn)
		case "-shc":
			fmt.Println(Users)
		default:
			fmt.Println("Неверная команда")
		}
	}
}
func scan() string {
	in := bufio.NewScanner(os.Stdin)
	in.Scan()
	if err := in.Err(); err != nil {
		fmt.Fprintln(os.Stderr, "Ошибка ввода:", err)
	}
	return in.Text()

}
func readCommand(input string) (Command string, Content string) {
	if strings.Contains(input, " ") {
		inputCommand := input[:strings.Index(input, " ")]
		inputContent := input[strings.Index(input, " ")+1:]
		return inputCommand, inputContent
	}
	inputCommand := input
	return inputCommand, ""

}
