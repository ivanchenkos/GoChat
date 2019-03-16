using System;
using System.Collections.Generic;

namespace CSharp {

    class LevelOne : Level {

        public LevelOne() : base() {
            StartX = 1;
            StartY = 1;

            MAP_WIDTH = 18;
            MAP_HEIGHT = 7;

            Map = new char[,] {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
                {'#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            };

            Entity enemy = new Entity('-', "enemy") {
                AutoMove = true,
                Agressive = true
            };
            enemy.SetFaceColor(ConsoleColor.Red);
            enemy.SetSpeedMove(1);
            enemy.SetMove("Left");
            enemy.AutoMoveRange.Enable();
            enemy.AutoMoveRange.SetFromToX(1, 13);
            enemy.SetSpawn(10, 3);

            Entity altar = new Entity('*', "altar");
            altar.SetFaceColor(ConsoleColor.Yellow);
            altar.SetSpawn(14, 3);

            AddEntity("enemy", enemy);
            AddEntity("altar", altar);

            for (int x = 5; x < 9; x++) {
                Entity trap = new Entity(':', "trap") {
                    AutoHideToggle = true,
                    AutoHideToggleSpeed = 5,
                    AutoHideToggleDuration = x - 5,
                    Agressive = true,
                    Immortal = true
                };
                trap.SetFaceColor(ConsoleColor.DarkRed);
                trap.SetSpawn(x, 1);

                AddEntity($"trap-{x}", trap);
            }

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
            Console.WriteLine("Уровень 1 - 'Вход'");
            Console.WriteLine();

            base.Render();

            Console.WriteLine();
            Console.WriteLine("Задание:");

            Console.Write("Доберись до алтаря [");
            Entities["altar"].Render();
            Console.Write("] не поповшись стражнику [");
            Entities["enemy"].Render();
            Console.Write("] и избежав опасных шипов [");
            MapObjects[':'].Render();
            Console.Write("]");
        }

    }

}