using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace testgame {
    public abstract class Character {
        public CharGraphics graphics;
        public Vector2 vector;
        public int moveSpeed;
        public List<Animation> animation;
        public Texture2D latestTexture;
        public Animation latestAnimation;
        public Rectangle hitbox;
        public Character() {

        }

    }
}
