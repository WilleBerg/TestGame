using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class Grid {
        public int[,] gridInt;
        public Rectangle[,] hitBoxGrid;
        public bool[,] boolGrid;
        public string[,] debugStringGrid;
        public int gridWidth;
        public int gridHeight;
        public Vector2[,] vectorGrid;
        public Vector2 vectorDelta;

        public Grid() {

        }

        public Grid(int gridWidth, int gridHeight, Vector2 vectorDelta) {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            this.vectorDelta = vectorDelta;
        }

        public Grid(Rectangle[,] hitBoxGrid) {
            this.hitBoxGrid = hitBoxGrid;
        }

        public Grid(Rectangle[,] hitBoxGrid, string[,] debugStringGrid, int gridWidth, int gridHeight, Vector2[,] vectorGrid) : this(hitBoxGrid) {
            this.debugStringGrid = debugStringGrid;
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            this.vectorGrid = vectorGrid;
        }
    }
}
