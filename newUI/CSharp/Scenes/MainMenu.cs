using System;
using System.Collections.Generic;

namespace CSharp {

    class MainMenu : Scene {

        public override void Init() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Охотник за сокровищами");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Введите соответствующущее значение:");

            Console.WriteLine("[0] Начать игру");
            Console.WriteLine("[любое другое] Выйти");

            Console.Write("\nВыбор: ");

            string line = Console.ReadLine().Trim();

            switch (line) {
                case "0":
                    Game.SetScene("Start");
                break;

                default:
                    Game.Close();
                break;
            }
        }

        public override void Update() {}

        public override void Render() {}

    }

}