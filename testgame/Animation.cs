using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class Animation {
        public List<Texture2D> textureList;
        public string typeOfAnimation;
        public int frameCount;
        public int currAnimation;
        public Animation() {
        }

        public Animation(List<Texture2D> textureList, string typeOfAnimation) {
            this.textureList = textureList;
            this.typeOfAnimation = typeOfAnimation;
            frameCount = 0;
            currAnimation = 1;
        }
        public void AddTexture(Texture2D texture) {
            textureList.Add(texture);
        }
        public int AnimationType() {
            string tempType = typeOfAnimation;
            if (tempType == "front") {
                return 0;
            } else if (tempType == "back") {
                return 1;
            } else if (tempType == "leftSide") {
                return 2;
            } else if (tempType == "rightSide") {
                return 3;
            } else {
                return 4;
            }
        }
    }
}
