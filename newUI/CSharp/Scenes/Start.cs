using System;
using System.Collections.Generic;

namespace CSharp {

    class Start : Scene {

        public override void Init() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Начало");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Твоей целью является прохождение всех уровней лабиринта.\n");

            string name;

            do {
                Console.Write("Введи свое имя: ");
                name = Console.ReadLine().Trim();
                
                if (String.IsNullOrEmpty(name)) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("* Имя не может быть пустым");
                    Console.ResetColor();
                }
            } while (String.IsNullOrEmpty(name));

            Game.Player.Name = name;

            if (Moderate.IsAdmin(name)) {
                Moderate.SelectLevel();
            } else {
                Data.CurrentLevel = 0;
                Game.SetScene("Level-1");
            }
        }

        public override void Update() {}

        public override void Render() {}

    }

}