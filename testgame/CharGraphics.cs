using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class CharGraphics : Graphics {
        public CharGraphics() {

        }
        public CharGraphics(Texture2D texture) {
            this.texture = texture;
        }

    }
}
