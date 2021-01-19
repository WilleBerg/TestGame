using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class Menu {
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
            startRec = new Rectangle((int) (29 * 1.5), (int) (357 * 1.5) , (int) (280 * 1.5), (int) (80 * 1.5));
            settingsRec = new Rectangle(Scale(28, 1.5), Scale(474,1.5), Scale(420,1.5), Scale(80,1.5));
            exitRec = new Rectangle(Scale(30, 1.5), Scale(582,1.5), Scale(180,1.5), Scale(80,1.5));
            recColor = new Color(Color.White, 1.0f);
        }

        private int Scale(int nmr, double scale) {
            return (int)( nmr * scale );
        }

        /// <summary>
        /// Checks if the main menu buttons has been pressed, if they hade it switches scene.
        /// </summary>
        /// <param name="ui"></param>
        public void Buttons(UI ui) {
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

        /// <summary>
        /// Changes Alpha of the main menu buttons. Used for the glow when hovering buttons in main menu.
        /// </summary>
        /// <param name="ui"></param>
        public void ColorAlhpaChange(UI ui) {
            if (!alphaSwitch && (ui.RecChecker(settingsRec) || ui.RecChecker(startRec) || ui.RecChecker(exitRec))) {
                recColor = new Color(Color.White, alpha);
                alpha += 0.008f;
                if (alpha > 0.5f) {
                    alphaSwitch = true;
                }
            } else if (alphaSwitch && (ui.RecChecker(settingsRec) || ui.RecChecker(startRec) || ui.RecChecker(exitRec))) {
                alpha -= 0.008f;
                if (alpha <= 0) {
                    alphaSwitch = false;
                }
            } else {
                alpha = 0;
            }
            
        }
    }
}
