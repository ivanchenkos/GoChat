using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp {

    class GameOver : Scene {
        public override void Init() {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("!!!Игра окончена!!!");

            Console.WriteLine();
            Console.WriteLine("Нажмите клавишу Enter...");
            Console.ResetColor();
            Console.ReadLine();

            Data.CurrentLevel = 0;
            Game.SetScene("Main Menu");
        }

        public override void Update() {}

        public override void Render() {}

    }

}