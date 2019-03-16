using System;
using System.Collections.Generic;

namespace CSharp {

    class Player : Entity {

        public Player(string name) : base('+', name) {
            EnableBody();
            SetFaceColor(ConsoleColor.DarkCyan);

            KeyPress.OnKeyPressed += KeyPressed;
        }

        private void KeyPressed(KeyPress.Key key) {
            Move["Left"] = key == KeyPress.Key.Left;
            Move["Up"] = key == KeyPress.Key.Up;
            Move["Right"] = key == KeyPress.Key.Right;
            Move["Down"] = key == KeyPress.Key.Down;
        }

        new public void Damage() {
            if (Moderate.IsAdmin(Name)) return;
            
            Health -= 1;

            if (IsAlive()) {
                SetLocation(SpawnX, SpawnY);
            } else {
                Game.SetScene("Game Over");
            }
        }

    }
}