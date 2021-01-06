using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class UI {
        public MouseState mus;
        public MouseState gammalMus;
        public KeyboardState keyboardState;

        public UI() {
            gammalMus = mus;
            mus = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        public UI(MouseState mus, MouseState gammalMus, KeyboardState keyboardState) {
            this.mus = mus;
            this.gammalMus = gammalMus;
            this.keyboardState = keyboardState;
        }

        public bool Musknappar() {
            if (mus.LeftButton == ButtonState.Pressed && gammalMus.LeftButton == ButtonState.Released) {
                return true;
            } else {
                return false;
            }

        }

        public void UpdateStates() {
            gammalMus = mus;
            mus = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        public bool RecChecker(Rectangle a) {
            if (a.Contains(mus.Position)) {
                return true;
            } else {
                return false;
            }
        }
        public bool LeftMousePressed() {
            if (mus.LeftButton == ButtonState.Pressed) {
                return true;
            } else {
                return false;
            }
        }
        public bool DownA() {
            if (keyboardState.IsKeyDown(Keys.A)) {
                return true;
            } else {
                return false;
            }
        }
        public bool DownS() {
            if (keyboardState.IsKeyDown(Keys.S)) {
                return true;
            } else {
                return false;
            }
        }
        public bool DownW() {
            if (keyboardState.IsKeyDown(Keys.W)) {
                return true;
            } else {
                return false;
            }
        }
        public bool DownD() {
            if (keyboardState.IsKeyDown(Keys.D)) {
                return true;
            } else {
                return false;
            }
        }

    }
}
