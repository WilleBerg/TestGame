using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace testgame {
    public class Settings {
        public Rectangle developerButton;
        public Rectangle returnButton;
        public Texture2D settingsTexture;
        private bool devToggle;
        public float alpha;
        private bool alphaSwitch;


        public bool DevToggle { get { return devToggle; } set { devToggle = value; } }

        public Settings() {
            
        }
        public Settings(Texture2D settingsTexture) {
            developerButton = new Rectangle(12, 11, 709, 188);
            returnButton = new Rectangle(12, 275, 390, 90);
            this.settingsTexture = settingsTexture;
        }

        public void Buttons(UI ui, Menu menu) {
            if (ui.Musknappar() && ui.RecChecker(developerButton)) {
                devToggle = !devToggle;
            } else if (ui.Musknappar() && ui.RecChecker(returnButton)) {
                menu.switchKey = 0;
            }
        }
        public void ColorAlhpaChange(UI ui) {
            if (!alphaSwitch && ( ui.RecChecker(developerButton) || ui.RecChecker(returnButton))) {
                alpha += 0.008f;
                if (alpha > 0.5f) {
                    alphaSwitch = true;
                }
            } else if (alphaSwitch && ( ui.RecChecker(developerButton) || ui.RecChecker(returnButton) )) {
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
