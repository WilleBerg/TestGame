using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class Grid {
        public int[,] gridInt;
        public Rectangle[,] hitBoxArray;
        private bool[,] boolGrid;
        private bool[,] needsMove;
        public string[,] debugStringArray;
        private int width;
        private int height;
        private  Vector2[,] vectorArray;
        public Vector2[,] VectorArray { get { return vectorArray; } set { vectorArray = value; } }
        public Vector2 vectorDelta;


        public bool[,] BoolGrid { get { return boolGrid; } set { boolGrid = value; } }
        /// Outdated boolarray, maybe remove
        public bool[,] NeedsMove { get { return needsMove; } set { needsMove = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public Grid() {

        }

        public Grid(int gridWidth, int gridHeight, Vector2 vectorDelta) {
            this.width = gridWidth;
            this.height = gridHeight;
            this.vectorDelta = vectorDelta;
            needsMove = new bool[gridHeight, gridWidth];
            vectorArray = new Vector2[gridHeight, gridWidth];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    vectorArray[i, j] = new Vector2(0, 0);
                }
            }
        }

        public Grid(Rectangle[,] hitBoxGrid) {
            this.hitBoxArray = hitBoxGrid;
        }

        public Grid(Rectangle[,] hitBoxGrid, string[,] debugStringGrid, int gridWidth, int gridHeight, Vector2[,] vectorGrid) : this(hitBoxGrid) {
            this.debugStringArray = debugStringGrid;
            this.width = gridWidth;
            this.height = gridHeight;
            this.vectorArray = vectorGrid;
        }

        /// <summary>
        /// Creates a rectangle array the size of (Grid.width x Grid.height)
        /// </summary>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public void CreateRectangleArray(int width, int height)
        {
            Rectangle[,] temp = new Rectangle[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    temp[i, j] = new Rectangle((int)vectorDelta.X, (int)vectorDelta.Y, width, height);
                }
            }
            hitBoxArray = temp;
        }
        /// <summary>
        /// Sets the vectors inside the Grid.hitBoxGrid.
        /// Make sure to initialize the Grid.hitBoxGrid beforehand.
        /// </summary>
        public void SetVectors()
        {
            for (int i = 0; i < height; i++) {
                if (i != 0) {
                    for (int j = 0; j < width - 1; j++) {
                        hitBoxArray[i, j].Y += i * 24;
                        vectorArray[i, j].Y += i * 24;
                    }
                }
                for (int j = 0; j < width; j++) {
                    if (j != 0) {
                        hitBoxArray[i, j].X += j * 24;
                        vectorArray[i, j].X += j * 24;
                    }
                }
            }
        }
        public void SetHitBox(UI ui) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (ui.LeftMousePressed() && ui.RecChecker(hitBoxArray[i,j])) {
                        boolGrid[i, j] = true;
                    }
                }
            }
            
        }
        private void NeedMove() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    needsMove[i, j] = true;
                }
            }
        }
        /// <summary>
        /// Checks if a character intersects with a Grid.Hitbox.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>True if the character hitbox intersects with the grid</returns>
        public bool CharacterIntersect(Char character) {

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (boolGrid[i,j]) {
                        if (character.hitbox.Intersects(hitBoxArray[i,j])) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
    
}
