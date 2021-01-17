using System;
using System.Collections.Generic;
using System.IO;
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
        public int hitBoxWidth;
        public int hitBoxHeight;
        public bool showDisabledHitBoxes;
        private int width;
        private int height;
        private  Vector2[,] vectorArray;
        private bool setHitboxToggle;
        public List<Event> eventList;
        private string hitBoxString;

        public bool SetHitboxToggle { get { return setHitboxToggle; } set { setHitboxToggle = value; } }
        public Vector2[,] VectorArray { get { return vectorArray; } set { vectorArray = value; } }
        public Vector2 vectorDelta;


        public bool[,] BoolGrid { get { return boolGrid; } set { boolGrid = value; } }
        /// Outdated boolarray, maybe remove
        public bool[,] NeedsMove { get { return needsMove; } set { needsMove = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public Grid() {

        }

        public Grid(int gridWidth, int gridHeight, Vector2 vectorDelta, int hitBoxWidth, int hitBoxHeight, string hitboxstring) {
            this.width = gridWidth;
            this.height = gridHeight;
            this.vectorDelta = vectorDelta;
            this.hitBoxHeight = hitBoxHeight;
            this.hitBoxWidth = hitBoxWidth;
            this.hitBoxString = hitboxstring;
            needsMove = new bool[gridHeight, gridWidth];
            vectorArray = new Vector2[gridHeight, gridWidth];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    vectorArray[i, j] = new Vector2(0, 0);
                }
            }
            showDisabledHitBoxes = false;
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
        /// Creates a rectangle array the size of (Grid.width x Grid.height) and puts it inside Grid.hitBoxArray
        /// </summary>
        public void CreateRectangleArray()
        {
            Rectangle[,] temp = new Rectangle[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    temp[i, j] = new Rectangle((int)vectorDelta.X, (int)vectorDelta.Y, hitBoxWidth, hitBoxHeight);
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
                        hitBoxArray[i, j].Y += i * hitBoxHeight;
                        vectorArray[i, j].Y += i * hitBoxHeight;
                    }
                }
                for (int j = 0; j < width; j++) {
                    if (j != 0) {
                        hitBoxArray[i, j].X += j * hitBoxWidth;
                        vectorArray[i, j].X += j * hitBoxWidth;
                    }
                }
            }
        }
        /// <summary>
        /// While active, you can change the hitboxgrid ingame using left mouse button.
        /// </summary>
        /// <param name="ui">Main user interface</param>
        public void SetHitBox(UI ui) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (ui.Musknappar() && ui.RecChecker(hitBoxArray[i,j])) {
                        boolGrid[i, j] = !boolGrid[i, j];
                    }
                    
                }
            }
        }
        /// <summary>
        /// Outdated method, might have use later.
        /// </summary>
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
        /// <param name="character">The character that gets checked</param>
        /// <returns>True if the character hitbox intersects with the grid</returns>
        public bool CharacterIntersect(Character character) {

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
        /// <summary>
        /// Writes the grids boolean array to a .txt file, so that it can be read when game starts.
        /// </summary>
        public void WriteGrid() {
            TextWriter tw = new StreamWriter("SavedList.txt");
            TextWriter tw1 = new StreamWriter("HitBox.txt");
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    tw.WriteLine(BoolGrid[i, j].ToString());
                    if (BoolGrid[i,j]) {
                        tw1.WriteLine(i + ";" + j);
                    }
                }
            }
            tw.Close();
            tw1.Close();
        }
        /// <summary>
        /// Loads the grid from a .txt file.
        /// </summary>
        public void LoadGrid() {
            int counter1 = 0;
            string[] allLines = File.ReadAllLines(hitBoxString);
            if (allLines.Length == width * height) {
                for (int i = 0; i < height; i++) {
                    for (int j = 0; j < width; j++) {
                        if (allLines[counter1] == "True") {
                            boolGrid[i, j] = true;
                            counter1++;
                        } else if (allLines[counter1] == "False") {
                            boolGrid[i, j] = false;
                            counter1++;
                        }
                    }
                }
            }
        }
    }
    
}
