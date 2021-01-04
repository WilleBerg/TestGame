using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace testgame {
    public abstract class Char {
        public CharGraphics graphics;
        public Vector2 vector;
        public int moveSpeed;
        public Char() {

        }

    }
}
