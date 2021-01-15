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

        /// <summary>
        /// Checks if the left mouse button has been pressed and released.
        /// </summary>
        /// <returns></returns>
        public bool Musknappar() {
            if (mus.LeftButton == ButtonState.Pressed && gammalMus.LeftButton == ButtonState.Released) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Updates the mouse and keyboard states.
        /// </summary>
        public void UpdateStates() {
            gammalMus = mus;
            mus = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Checks if a rectangle contains the mouse.
        /// </summary>
        /// <param name="a"> The rectangle that you want to check</param>
        /// <returns>True or false</returns>
        public bool RecChecker(Rectangle a) {
            if (a.Contains(mus.Position)) {
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// Checks if the left mouse button is currently being pressed.
        /// </summary>
        /// <returns></returns>
        public bool LeftMousePressed() {
            if (mus.LeftButton == ButtonState.Pressed) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Checks if A is being pressed on the keyboard
        /// </summary>
        /// <returns></returns>
        public bool DownKey(Keys key) {
            if (keyboardState.IsKeyDown(key)) {
                return true;
            } else {
                return false;
            }
        }

        public List<Keys> NotAllowedKeys(PC pc, Grid grid) {
            List<Keys> notAllowedKeys = new List<Keys>();
            Rectangle tempD = new Rectangle(pc.hitbox.X + Game1.pcMovementSpeed, pc.hitbox.Y, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempA = new Rectangle(pc.hitbox.X - Game1.pcMovementSpeed, pc.hitbox.Y, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempS = new Rectangle(pc.hitbox.X, pc.hitbox.Y + Game1.pcMovementSpeed, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempW = new Rectangle(pc.hitbox.X, pc.hitbox.Y - Game1.pcMovementSpeed, pc.hitbox.Width, pc.hitbox.Height);
            for (int i = 0; i < grid.Height; i++) {
                for (int j = 0; j < grid.Width; j++) {
                    if (grid.BoolGrid[i,j]) {
                        if (tempD.Intersects(grid.hitBoxArray[i,j]) && !notAllowedKeys.Contains(Keys.D)) {
                            notAllowedKeys.Add(Keys.D);
                        }
                        if (tempA.Intersects(grid.hitBoxArray[i, j]) && !notAllowedKeys.Contains(Keys.A)) {
                            notAllowedKeys.Add(Keys.A);
                        }
                        if (tempS.Intersects(grid.hitBoxArray[i, j]) && !notAllowedKeys.Contains(Keys.S)) {
                            notAllowedKeys.Add(Keys.S);
                        }
                        if (tempW.Intersects(grid.hitBoxArray[i, j]) && !notAllowedKeys.Contains(Keys.W)) {
                            notAllowedKeys.Add(Keys.W);
                        }
                    }
                }
            }
            return notAllowedKeys;
        }
    }
}
