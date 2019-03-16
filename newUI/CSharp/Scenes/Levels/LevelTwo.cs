using System;
using System.Collections.Generic;

namespace CSharp {

    class LevelTwo : Level {

        public LevelTwo() : base() {
            StartX = 8;
            StartY = 0;

            MAP_WIDTH = 18;
            MAP_HEIGHT = 19;

            Map = new char[,] {
                {'#', '#', '#', '#', '#', '#', '#', '#', '0', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
            };

            for (int y = 1; y < 16; y += 2) {
                bool isEven = y % 2 == 0;
                for (int x = isEven ? 1 : 2; x < 16; x += 3) {
                    Entity enemy = new Entity('-', "enemy") {
                        AutoMove = true,
                        Agressive = true
                    };
                    enemy.SetFaceColor(ConsoleColor.Red);
                    enemy.SetSpeedMove(1);
                    enemy.AutoMoveRange.Enable();
                    enemy.AutoMoveRange.SetFromTo(x-1, y, x+2, y);
                    enemy.SetMove(isEven ? "Left" : "Right");
                    enemy.SetSpawn(x, y);

                    AddEntity($"enemy-{x}:{y}", enemy);
                }
            }

            for (int x = 3; x < 12; x++) {

                if (x >= 6 && x < 9) continue;
                bool isLeft = x < 6;

                Entity enemy = new Entity('-', "enemy") {
                    AutoMove = true,
                    Agressive = true
                };
                enemy.SetFaceColor(ConsoleColor.Red);
                enemy.SetSpeedMove(1);
                enemy.SetMove(isLeft ? "Left" : "Right");
                enemy.SetSpawn(x, 17);

                AddEntity($"enemy-{x}:{17}", enemy);
            }

            Entity altar = new Entity('*', "altar");
            altar.SetFaceColor(ConsoleColor.Yellow);
            altar.SetSpawn(8, 18);

            AddEntity("altar", altar);


            Entity wall = new Entity('#', "Wall");
            wall.EnableBody();

            AddMapObject(wall);
        }


        public override void Init() {
            base.Init();
        }

        public override void Update() {
            base.Update();

            if (Collize(Entities["altar"], Game.Player)) Game.SetScene("Complete");
        }

        public override void Render() {
            Console.WriteLine("Уровень 2 - 'В глубь лабиринта'");
            Console.WriteLine();

            base.Render();

            Console.WriteLine();
            Console.WriteLine("Задание:");

            Console.Write("Доберись до алтаря [");
            Entities["altar"].Render();
            Console.Write("] не поповшись ни одному стражнику [");
            MapObjects['-'].Render();
            Console.Write("]");
        }

    }

}