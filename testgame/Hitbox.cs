using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Xml.Serialization;

namespace testgame {
    [Serializable]
    public class Hitbox : IMovable, IHittable{
        private bool wallBox;
        private bool zoneBox;
        private Rectangle rectangle;
        private Vector2 position;
        private int clickCount;
        public Zone connectedZone;


        public Vector2 Position { get { return position; } set { position = value; } }
        public bool WallBox { get { return wallBox; } set { wallBox = value; } }
        public bool ZoneBox { get { return zoneBox; } set { zoneBox = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public Hitbox() {

        }
        public Hitbox(bool wallBox, bool zoneBox, int x, int y, int size) {
            this.wallBox = wallBox;
            this.zoneBox = zoneBox;
            CreateRectangle(x, y, size);
        }

        /// <summary>
        /// Sets the hitboxes vector.
        /// </summary>
        /// <param name="x">The x position in the grid</param>
        /// <param name="y">The y position in the grid</param>
        /// <param name="size">The size of the hitbox rectangle</param>
        private void SetVectors(int x, int y, int size) {
            position.X += x * size;
            position.Y += y * size;
        }
        /// <summary>
        /// Creates the rectangle for the hitbox.
        /// </summary>
        /// <param name="x">The X position of the hitbox</param>
        /// <param name="y">The Y position of the hitbox</param>
        /// <param name="size"> The size of the hitbox</param>
        private void CreateRectangle(int x, int y, int size) {
            SetVectors(x, y, size);
            rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
        }
        /// <summary>
        /// Changes the hitbox property.
        /// Called from Grid.SetHitBox
        /// </summary>
        public void SetHitbox() {
            if (clickCount == 0) {
                WallBox = true;
                ZoneBox = false;
                if (Game1.ui.Musknappar()) {
                    clickCount++;
                }
            } else if (clickCount == 1) {
                WallBox = false;
                ZoneBox = true;
                if (Game1.ui.Musknappar()) {
                    clickCount++;
                }
            } else if (clickCount == 2) {
                if (Game1.ui.Musknappar()) {
                    clickCount = 0;
                }
                WallBox = false;
                ZoneBox = false;
            }
        }

        /// <summary>
        /// Checks if the zone should be changed
        /// </summary>
        /// <param name="pc">The playable character</param>
        public bool ChangeZone(PC pc) {
            if (pc.Hitbox.Intersects(rectangle) && zoneBox) {
                return true;
            } else return false;
        }

        
        override
        public string ToString() {

            return ( rectangle.ToString() );
        }

        public void SetHitboxX(int value) {
            rectangle.X = value;
        }

        public void SetHitBoxY(int value) {
            rectangle.Y = value;
        }

        public int GetHitBoxX() {
            return rectangle.X;
        }

        public int GetHitBoxY() {
            return rectangle.Y;
        }

        public float getX() {
            return position.X;
        }

        public float getY() {
            return position.Y;
        }

        public void setX(float input) {
            position.X = input;
        }

        public void setY(float input) {
            position.Y = input;
        }
    }
}
