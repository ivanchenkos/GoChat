using System;
using System.Collections.Generic;

namespace CSharp {

    abstract class Level : Scene {
        protected int MAP_HEIGHT;
        protected int MAP_WIDTH;

        protected char[,] Map;
        protected int StartX = 0;
        protected int StartY = 0;

        protected Dictionary<char, Entity> MapObjects = new Dictionary<char, Entity>();
        protected Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        public Level() {
            AddEntity("Game.Player", Game.Player);
        }

        public override void Init() {
            Game.Player.ResetMove();
            Game.Player.SetSpawn(StartX, StartY);

            foreach (Entity entity in Entities.Values) {
                entity.Init();
            }
        }

        public override void Update() {
            foreach (Entity entity in Entities.Values) {
                entity.Update();

                if (!entity.IsAlive()) continue;

                if (!Game.Player.Equals(entity)) {
                    if (entity.Agressive && entity.Visible && Collize(entity, Game.Player)) {
                        Game.Player.Damage();
                        entity.Damage();
                    }
                }

                EntityMove(entity);
            }
            Game.Player.ResetMove();
        }

        protected void EntityMove(Entity entity) {
            if (entity.CanMove) {
                if (entity.Move["Left"]) {
                    if (CanMove(entity.X - 1, entity.Y)) entity.MoveLeft();
                    else if (entity.AutoMove) {
                        entity.Move["Left"] = false;
                        entity.Move["Right"] = true;
                    }
                }
                else if (entity.Move["Up"]) {
                    if (CanMove(entity.X, entity.Y - 1)) entity.MoveUp();
                    else if (entity.AutoMove) {
                        entity.Move["Up"] = false;
                        entity.Move["Down"] = true;
                    }
                }
                else if (entity.Move["Right"]) {
                    if (CanMove(entity.X + 1, entity.Y)) entity.MoveRight();
                    else if (entity.AutoMove) {
                        entity.Move["Left"] = true;
                        entity.Move["Right"] = false;
                    }
                }
                else if (entity.Move["Down"]) {
                    if (CanMove(entity.X, entity.Y + 1)) entity.MoveDown();
                    else if (entity.AutoMove) {
                        entity.Move["Up"] = true;
                        entity.Move["Down"] = false;
                    }
                }

                if (entity.X >= MAP_WIDTH) entity.SetLocationX(MAP_WIDTH - 1);
                else if (entity.X < 0) entity.SetLocationX(0);

                if (entity.Y >= MAP_HEIGHT) entity.SetLocationY(MAP_HEIGHT - 1);
                else if (entity.Y < 0) entity.SetLocationY(0);
            }
        }

        protected bool Collize(Entity first, Entity second) {
            return first.X == second.X && first.Y == second.Y;
        }

        protected bool CanMove(int x, int y) {
            if (x < 0 || x >= MAP_WIDTH || y < 0 || y >= MAP_HEIGHT) return false;

            char face = Map[y, x];

            if (MapObjects.ContainsKey(face)) {
                return !MapObjects[face].Body;
            }

            return true;
        }

        public override void Render() {
            char[,] res = Map.Clone() as char[,];

            foreach (Entity entity in Entities.Values) {
                if (entity.Visible) res[entity.Y, entity.X] = entity.Face;
            }

            for (int y = 0; y < MAP_HEIGHT; y++) {
                for (int x = 0; x < MAP_WIDTH; x++) {
                    if (MapObjects.ContainsKey(res[y, x])) {
                        MapObjects[res[y, x]].RenderForMap();
                    } else Console.Write(res[y, x]);
                }
                Console.WriteLine();
            }

            if (Game.Player.Health <= 1) Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Game.Player.Name} - Здоровье: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Game.Player.Health);
            Console.ResetColor();
        }

        protected void AddMapObject(Entity obj) {
            if (!MapObjects.ContainsKey(obj.Face)) MapObjects.Add(obj.Face, obj);
        }

        protected void AddEntity(string name, Entity entity) {
            AddMapObject(entity);
            Entities.Add(name, entity);
        }

    }

}