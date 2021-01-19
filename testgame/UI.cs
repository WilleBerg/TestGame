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
        public KeyboardState oldKeyboard;
        public UI() {
            gammalMus = mus;
            mus = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        public UI(MouseState mus, MouseState gammalMus, KeyboardState keyboardState) {
            this.mus = mus;
            this.gammalMus = gammalMus;
            this.keyboardState = keyboardState;
            oldKeyboard = keyboardState;
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
            oldKeyboard = keyboardState;
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

        public bool RightMousePressed() {
            if (mus.RightButton == ButtonState.Pressed) {
                return true;
            } else {
                return false;
            }
        }

        public bool KeyPressedAndReleased(Keys key) {
            if (keyboardState.IsKeyDown(key) && oldKeyboard.IsKeyUp(key)) {
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
        /// <summary>
        /// Creates a list of currently not allowed keys. It checks if a move in any direction results in a hitbox intersect.
        /// </summary>
        /// <param name="pc">The playable character that get its move checked</param>
        /// <param name="grid">The grid of the zone that the playable character is in</param>
        /// <returns></returns>
        public List<Keys> NotAllowedKeys(PC pc, Grid grid) {
            List<Keys> notAllowedKeys = new List<Keys>();
            Rectangle tempD = new Rectangle(pc.hitbox.X + Game1.pcMovementSpeed + 5, pc.hitbox.Y, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempA = new Rectangle(pc.hitbox.X - Game1.pcMovementSpeed - 5, pc.hitbox.Y, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempS = new Rectangle(pc.hitbox.X, pc.hitbox.Y + Game1.pcMovementSpeed + 5, pc.hitbox.Width, pc.hitbox.Height);
            Rectangle tempW = new Rectangle(pc.hitbox.X, pc.hitbox.Y - Game1.pcMovementSpeed - 5, pc.hitbox.Width, pc.hitbox.Height);
            for (int i = 0; i < grid.Height; i++) {
                for (int j = 0; j < grid.Width; j++) {
                    if (grid.hitBoxArray[i,j].WallBox) {
                        if (tempD.Intersects(grid.hitBoxArray[i,j].rectangle) && !notAllowedKeys.Contains(Keys.D)) {
                            notAllowedKeys.Add(Keys.D);
                        }
                        if (tempA.Intersects(grid.hitBoxArray[i, j].rectangle) && !notAllowedKeys.Contains(Keys.A)) {
                            notAllowedKeys.Add(Keys.A);
                        }
                        if (tempS.Intersects(grid.hitBoxArray[i, j].rectangle) && !notAllowedKeys.Contains(Keys.S)) {
                            notAllowedKeys.Add(Keys.S);
                        }
                        if (tempW.Intersects(grid.hitBoxArray[i, j].rectangle) && !notAllowedKeys.Contains(Keys.W)) {
                            notAllowedKeys.Add(Keys.W);
                        }
                    }
                }
            }
            return notAllowedKeys;
        }
    }
}
