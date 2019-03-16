using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp {

    class Complete : Scene {
        public override void Init() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("!!!Поздравляю!!!");

            if (Data.CurrentLevel < Data.Levels.Count - 1) {
                Console.WriteLine("Уровень пройден!");
            } else {
                Console.WriteLine("Игра пройдена!");
            }

            Console.ResetColor();
            Console.WriteLine();

            if (Moderate.IsAdmin(Game.Player.Name)) {
                Console.WriteLine("Сделайте выбор:");

                Console.WriteLine("[0] Выбрать уровень");
                Console.WriteLine("[1] Следующий уровень");
                Console.WriteLine("[2] Главное меню");
                Console.WriteLine("[любое другое] Выход");

                Console.Write("\nВыбор: ");

                string line = Console.ReadLine().Trim();

                switch (line) {
                    case "0":
                        Moderate.SelectLevel();
                    break;

                    case "1":
                        NextLevel();
                    break;

                    case "2":
                        Game.SetScene("Main Menu");
                    break;

                    default:
                        Game.Close();
                    break;
                }
            } else {
                Console.WriteLine("Нажмите клавишу Enter...");
                Console.ReadLine();
                
                NextLevel();
            }
        }

        private void NextLevel() {
            Data.CurrentLevel++;

            if (Data.Levels.Count > Data.CurrentLevel) {
                Game.SetScene($"Level-{Data.CurrentLevel + 1}");
            } else {
                Data.CurrentLevel = 0;
                Game.SetScene("Main Menu");
            }
        }

        public override void Update() {}

        public override void Render() {}

    }

}