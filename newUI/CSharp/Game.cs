using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp {

    static class Game {

        public static Player Player = new Player("Hero") {
            HealthBase = 3
        };

        public static int FPS = 5;
        private static Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>();
        private static Scene SelectScene;

        public static bool Closed { get; private set; } = false;
        public static bool Paused { get; private set; } = false;

        public static void Init() {
            Player.SetSpeedSpawn(10);
        }

        public static void Start() {
            Paused = false;

            while(!Closed) {
                if (!Paused) Loop();

                Thread.Sleep(1000 / FPS);
            }
        }

        public static void Close() {
            KeyPress.Stop();
            Closed = true;
        }

        public static void Pause() {
            Paused = true;
        }

        public static void AddScene(string name, Scene scene) {
            Scenes.Add(name, scene);
        }

        public static void SetScene(string name) {
            if (Scenes.ContainsKey(name)) {
                Console.Clear();
                SelectScene = Scenes[name];
                SelectScene.Init();
            }
        }

        public static void RemoveScene(string name) {
            Scenes.Remove(name);
        }

        private static void Loop() {
            SelectScene.Update();
            Console.Clear();
            SelectScene.Render();
        }
    }


}