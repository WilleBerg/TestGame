using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testgame {
    public class Background {
        private Texture2D texture;
        public Vector2 vector;
        private float speed;


        public float Speed { get { return speed; } set { speed = value; } }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        

        public Background(Texture2D texture, Vector2 vector, float speed) {
            this.texture = texture;
            this.vector = vector;
            this.speed = speed;
        }

        public Background() {
        }
    }
}
