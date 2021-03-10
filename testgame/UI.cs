using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace testgame {
    public class UI {
        public MouseState mus;
        public MouseState gammalMus;
        public KeyboardState keyboardState;
        public KeyboardState oldKeyboard;
        public Keys latestKeyPressed;
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
            Keys[] keys = { Keys.W, Keys.S, Keys.A, Keys.D };
            for (int i = 0; i < keys.Length; i++) {
                if (DownKey(keys[i])) {
                    latestKeyPressed = keys[i];
                }
            }
            for (int i = 0; i < 2; i++) {
                for (int j = 2; j < 4; j++) {
                    if (DownKey(keys[i]) && DownKey(keys[j])) {
                        latestKeyPressed = keys[i];
                    }
                }
            }
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
            Rectangle tempD = new Rectangle(pc.GetHitBoxX() + Game1.pcMovementSpeed + 5, pc.GetHitBoxY(), pc.Hitbox.Width, pc.Hitbox.Height);
            Rectangle tempA = new Rectangle(pc.GetHitBoxX()- Game1.pcMovementSpeed - 5, pc.GetHitBoxY(), pc.Hitbox.Width, pc.Hitbox.Height);
            Rectangle tempS = new Rectangle(pc.GetHitBoxX(), pc.GetHitBoxY() + Game1.pcMovementSpeed + 5, pc.Hitbox.Width, pc.Hitbox.Height);
            Rectangle tempW = new Rectangle(pc.GetHitBoxX(), pc.GetHitBoxY() - Game1.pcMovementSpeed - 5, pc.Hitbox.Width, pc.Hitbox.Height);
            for (int i = 0; i < grid.Height; i++) {
                for (int j = 0; j < grid.Width; j++) {
                    if (grid.hitBoxArray[i, j].WallBox) {
                        if (tempD.Intersects(grid.hitBoxArray[i, j].Rectangle) && !notAllowedKeys.Contains(Keys.D)) {
                            notAllowedKeys.Add(Keys.D);
                        }
                        if (tempA.Intersects(grid.hitBoxArray[i, j].Rectangle) && !notAllowedKeys.Contains(Keys.A)) {
                            notAllowedKeys.Add(Keys.A);
                        }
                        if (tempS.Intersects(grid.hitBoxArray[i, j].Rectangle) && !notAllowedKeys.Contains(Keys.S)) {
                            notAllowedKeys.Add(Keys.S);
                        }
                        if (tempW.Intersects(grid.hitBoxArray[i, j].Rectangle) && !notAllowedKeys.Contains(Keys.W)) {
                            notAllowedKeys.Add(Keys.W);
                        }
                    }
                }
            }
            return notAllowedKeys;
        }
    }
}
