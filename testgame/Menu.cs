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
            startRec = new Rectangle(29, 357, 280, 80);
            settingsRec = new Rectangle(28, 474, 420, 80);
            exitRec = new Rectangle(30, 582, 180, 80);
            recColor = new Color(Color.White, 1.0f);
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
