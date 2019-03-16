using System;
using System.Collections.Generic;

namespace CSharp {

    class Entity {

        public class MoveRange {

            public int FromX = 0;
            public int FromY = 0;
            public int ToX = 0;
            public int ToY = 0;

            public bool Enabled { get; private set; } = false;

            public MoveRange() {}

            public MoveRange(int fromX, int fromY, int toX, int toY) {
                SetFromTo(fromX, fromY, toX, toY);
            }

            public void SetFromTo(int fromX, int fromY, int toX, int toY) {
                FromX = fromX;
                FromY = fromY;
                ToX = toX;
                ToY = toY;
            }

            public void SetFromToX(int fromX, int toX) {
                FromX = fromX;
                ToX = toX;
            }

            public void SetFromToY(int fromY, int toY) {
                FromY = fromY;
                ToY = toY;
            }

            public void Enable() {
                Enabled = true;
            }

            public void Disable() {
                Enabled = false;
            }
        }

        public string Name;
        public char Face { get; private set; }
        public ConsoleColor FaceColor { get; private set; } = ConsoleColor.White;
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;
        public bool Body { get; private set; } = false;
        private bool BodyDefault = false;

        public Dictionary<string, bool> Move = new Dictionary<string, bool>();
        public int SpeedMove { get; private set; } = 0;
        private int MoveCounter = 0;
        public bool CanMove { get; private set; } = true;

        public int Health = 1;
        public int HealthBase = 3;
        public bool Immortal = false;

        public bool Visible { get; private set; } = true;

        public bool AutoMove = false;
        public MoveRange AutoMoveRange { get; private set; } = new MoveRange();

        public bool Agressive = false;

        private int SpawnCounter = 0;
        public int SpeedSpawn { get; private set; } = 0;
        public int SpawnX { get; private set; } = 0;
        public int SpawnY { get; private set; } = 0;

        public bool AutoHideToggle = false;
        public int AutoHideToggleSpeed = 0;
        public int AutoHideToggleDuration = 0;
        private int AutoHideToggleDurationCounter = 0;
        private int AutoHideToggleCounter = 0;
        

        public Entity(char fase, string name = "No name") {
            Face = fase;

            Move.Add("Left", false);
            Move.Add("Up", false);
            Move.Add("Right", false);
            Move.Add("Down", false);
        }

        public void Render() {
            if (!Visible) return;

            RenderForMap();
        }

        public void RenderForMap() {
            Console.ForegroundColor = FaceColor;
            Console.Write(Face);
            Console.ResetColor();
        }

        public void Update() {
            if (IsAlive()) {
                if (MoveCounter < SpeedMove) MoveCounter++;
                else if (!CanMove) {
                    CanMove = true;
                    MoveCounter = 0;
                }

                if (AutoMove && AutoMoveRange.Enabled && CanMove) {
                    if (!Move["Right"]) Move["Left"] = X > AutoMoveRange.FromX;
                    if (!Move["Left"]) Move["Right"] = X < AutoMoveRange.ToX;

                    if (!Move["Down"]) Move["Up"] = Y > AutoMoveRange.FromY;
                    if (!Move["Up"]) Move["Down"] = Y < AutoMoveRange.ToY;
                }

                if (AutoHideToggle) {
                    if (AutoHideToggleDurationCounter > 0) AutoHideToggleDurationCounter--;
                    else {
                        if (AutoHideToggleCounter < AutoHideToggleSpeed) AutoHideToggleCounter++;
                        else {
                            AutoHideToggleCounter = 0;
                            Visible = !Visible;
                            if (BodyDefault) Body = Visible;
                        }
                    }
                }
            } else {
                Death();
            }
        }

        protected void Death() {
            SpawnCounter++;
            if (Visible) Visible = false;
            if (SpawnCounter >= SpeedSpawn) Spawn();
        }

        public void SetSpawn(int x, int y) {
            SpawnX = x;
            SpawnY = y;
        }

        public void Init() {
            SpawnCounter = 0;
            MoveCounter = 0;
            AutoHideToggleCounter = 0;
            SetLocation(SpawnX, SpawnY);
            Health = HealthBase;
            AutoHideToggleDurationCounter = AutoHideToggleDuration;
        }

        public void Spawn() {
            Init();
            Visible = true;
        }

        public void Damage() {
            if (Immortal) return;

            Health -= 1;

            if (!IsAlive()) {
                Death();
            }
        }

        public bool IsAlive() {
            return Health > 0;
        }

        public void Show() {
            Visible = true;
        }

        public void Hide() {
            Visible = false;
        }

        public void SetFace(char face) {
            Face = face;
        }

        public void SetFaceColor(ConsoleColor color) {
            FaceColor = color;
        }

        public void EnableBody() {
            BodyDefault = true;
            Body = true;
        }

        public void DisableBody() {
            BodyDefault = false;
            Body = false;
        }

        public void SetSpeedMove(int speed) {
            SpeedMove = speed;
        }

        public void SetSpeedSpawn(int speed) {
            SpeedSpawn = speed;
        }

        public void SetMove(string side) {
            ResetMove();
            Move[side] = true;
        }

        public void SetLocation(int x, int y) {
            X = x;
            Y = y;
        }

        public void SetLocationX(int x) {
            X = x;
        }

        public void SetLocationY(int y) {
            Y = y;
        }

        public void MoveLeft() {
            if (!CanMove) return;
            X -= 1;
            CanMove = false;
        }

        public void MoveUp() {
            if (!CanMove) return;
            Y -= 1;
            CanMove = false;
        }

        public void MoveRight() {
            if (!CanMove) return;
            X += 1;
            CanMove = false;
        }

        public void MoveDown() {
            if (!CanMove) return;
            Y += 1;
            CanMove = false;
        }

        public void ResetMove() {
            Move["Left"] = false;
            Move["Up"] = false;
            Move["Right"] = false;
            Move["Down"] = false;
        }

    }
}