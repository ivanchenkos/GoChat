using System;
using System.Threading;


namespace CSharp {
    static class KeyPress
    {
        [System.Runtime.InteropServices.DllImport(
            "user32.dll",
            CharSet = System.Runtime.InteropServices.CharSet.Auto,
            ExactSpelling = true
        )]

        public static extern short GetAsyncKeyState(int vkey);
    
        public enum Key : int {
            Enter = 0x0D,
            Space = 0x20,
            Left = 0x25,
            Up = 0x26,
            Right = 0x27,
            Down = 0x28
        };

        public delegate void keyPress( Key Key );
        public static event keyPress OnKeyPressed;
        static Thread th = new Thread(x => {
            while (!Game.Closed) {
                if (OnKeyPressed != null) {
                    foreach (int keyCode in Enum.GetValues(typeof(Key))) {
                        if (GetAsyncKeyState(keyCode) != 0) {
                            OnKeyPressed((Key) keyCode);
                        }
                    }
                }
    
                Thread.Sleep(1000 / Game.FPS);
            }
        });
    
        public static void Start() {
            th.Start();
        }
    
        public static void Stop() {
            try {
               th.Abort();
            } catch (Exception err) {
                Console.WriteLine(err);
            }
        }
    }
}