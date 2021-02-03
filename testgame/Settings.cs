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
            developerButton = new Rectangle(Scale(12), Scale(11), Scale(709), Scale(188));
            returnButton = new Rectangle(Scale(12), Scale(275), Scale(390), Scale(90));
            this.settingsTexture = settingsTexture;
            devToggle = true;
        }

        //TODO: Fix correct scaling
        private int Scale(int nmbr) {
            return (int)( nmbr * 1.5 );
        }

        public void Buttons(UI ui, Menu menu) {
            if (ui.Musknappar() && ui.RecChecker(developerButton)) {
                devToggle = !devToggle;
            } else if (ui.Musknappar() && ui.RecChecker(returnButton)) {
                Game1.currentGameState = Game1.GameState.TitleScreen;
                Game1.world.CurrentLoadState = Game1.LoadStates.TitleScreen;
                
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
