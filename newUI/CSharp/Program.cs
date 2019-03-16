using System;
using System.Collections.Generic;

namespace CSharp {
	public class Program {

		public static void Main() {
			KeyPress.Start();

			Data.Levels.Add(new LevelOne());
			Data.Levels.Add(new LevelTwo());

			for (int i = 0; i < Data.Levels.Count; i++) {
				Game.AddScene($"Level-{i + 1}", Data.Levels[i]);
			}

			Game.AddScene("Main Menu", new MainMenu());
			Game.AddScene("Start", new Start());
			Game.AddScene("Game Over", new GameOver());
			Game.AddScene("Complete", new Complete());

			Game.SetScene("Main Menu");
			Game.Init();
			Game.Start();
		}
	}

	
}
