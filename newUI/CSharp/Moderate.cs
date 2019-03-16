using System;

namespace CSharp {

    static class Moderate {

        private static string ADMIN_NAME = "GodMode";

        public static bool IsAdmin(string name) {
            return name == ADMIN_NAME;
        }

        public static void SelectLevel() {
            int lvl = 0;
            string line;

            do {
                Console.Write($"Введите уровень на который хотите попасть[1-{Data.Levels.Count}]: ");
                line = Console.ReadLine();

                if (!Int32.TryParse(line, out lvl) || lvl < 1 || lvl > Data.Levels.Count) {
                    Console.WriteLine("* Введите корректное значение");
                }
            } while (lvl < 1 && lvl > Data.Levels.Count);

            Data.CurrentLevel = lvl - 1;
            Game.SetScene($"Level-{lvl}");
        }

    }

}