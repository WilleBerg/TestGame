using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class PC : Char {

        public PC() {

        }
        public PC(Vector2 vector, CharGraphics graphics, int moveSpeed, List<Animation> animation) {
            this.vector = vector;
            this.graphics = graphics;
            this.moveSpeed = moveSpeed;
            this.animation = animation;
            latestTexture = graphics.texture;
            latestAnimation = animation[0];
        }

        public void AddAnimation(Animation added) {
            animation.Add(added);
        }

    }
}
