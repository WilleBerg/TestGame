using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace testgame {
    public class Hitbox {
        private bool wallBox;
        private bool zoneBox;
        public Rectangle rectangle;
        private Vector2 position;
        private int clickCount;
        public Zone connectedZone;

        public Vector2 Position { get { return position; } set { position = value; } }
        public bool WallBox { get { return wallBox; } set { wallBox = value; } }
        public bool ZoneBox { get { return zoneBox; } set { zoneBox = value; } }

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
        private void CreateRectangle(int x, int y, int size) {
            SetVectors(x, y, size);
            rectangle = new Rectangle((int) position.X, (int) position.Y, size, size);
        }
        public void SetHitbox() {
            if (clickCount == 0) {
                WallBox = true;
                ZoneBox = false;
                clickCount++;
            } else if (clickCount == 1) {
                WallBox = false;
                ZoneBox = true;
                clickCount++;
            } else if (clickCount == 2) {
                clickCount = 0;
                WallBox = false;
                ZoneBox = false;
            }
        }

        public void ChangeZone(PC pc) {
            if (pc.hitbox.Intersects(rectangle) && zoneBox) {
                Game1.world.CurrentZone = connectedZone;
            }
        }

        override
        public string ToString() {

            return ( rectangle.ToString() );
        }
    }
}
