using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace testgame {
    public abstract class World {
        public WorldGraphics graphics;
        public Vector2 vector;
        public List<Character> currCharacters;
        public PC pc;
    }
}
