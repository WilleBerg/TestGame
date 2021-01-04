using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    class Menu {
        public int switchKey;
        public Texture2D menuTexture;
        public Rectangle startRec;
        public Rectangle settingsRec;
        public Rectangle exitRec;
        public Color recColor;
        public float alpha;
        public bool alphaSwitch;

        public Menu() {
        }

        public Menu(Texture2D menuTexture) {
            switchKey = 0;
            alpha = 0;
            alphaSwitch = false;
            this.menuTexture = menuTexture;
            startRec = new Rectangle(29, 357, 280, 80);
            settingsRec = new Rectangle(28, 474, 420, 80);
            exitRec = new Rectangle(30, 582, 180, 80);
            recColor = new Color(Color.White, 1.0f);
        }

        public void MenuButtons(UI ui) {
            if (ui.Musknappar() && ui.RecChecker(startRec)) {
                switchKey = 1;
            }
            if (ui.Musknappar() && ui.RecChecker(settingsRec)) {
                switchKey = 2;
            }
            if (ui.Musknappar() && ui.RecChecker(exitRec)) {
                switchKey = 2000;
            }
        }

        public void ColorAlhpaChange(UI ui) {
            if (!alphaSwitch && ui.RecChecker(settingsRec)) {
                recColor = new Color(Color.White, alpha);
                alpha += 0.01f;
                if (alpha > 0.5f) {
                    alphaSwitch = true;
                }
            } else if (alphaSwitch && ui.RecChecker(settingsRec)) {
                alpha -= 0.01f;
                if (alpha <= 0) {
                    alphaSwitch = false;
                }
            } else {
                alpha = 0;
            }
            
        }
    }
}
