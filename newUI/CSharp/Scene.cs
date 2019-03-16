using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp {

    abstract class Scene {
        public abstract void Init();
        public abstract void Update();
        public abstract void Render();

    }

}