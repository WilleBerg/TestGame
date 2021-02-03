using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class ZoneGraphics : WorldGraphics {



        public ZoneGraphics() {
        }
        public ZoneGraphics(int resX, int resY) {
            this.resX = resX;
            this.resY = resY;
        }
        public ZoneGraphics(Texture2D texture, int resX, int resY) {
            this.texture = texture;
            this.resX = resX;
            this.resY = resY;
        }
    }
}
