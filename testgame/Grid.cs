using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    public class Grid {
        private int width;
        private int height;
        private int hitBoxSize;
        private string saveFile;
        private bool showDisabledHitboxes;
        private bool setHitboxToggle;
        public Vector2 vectorDelta;
        private int clickCount;
        public Hitbox[,] hitBoxArray;

        public bool SetHitboxToggle { get { return setHitboxToggle; } set { setHitboxToggle = value; } }
        public bool ShowDisabledHitBoxes { get { return showDisabledHitboxes; } set { showDisabledHitboxes = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int HitBoxSize { get { return hitBoxSize; } set { hitBoxSize = value; } }
        public string SaveFile { get; set; }

        public Grid() { }

        public Grid(int width, int height, int hitBoxSize, string saveFile) {
            this.width = width;
            this.height = height;
            this.hitBoxSize = hitBoxSize;
            this.saveFile = saveFile;
            showDisabledHitboxes = false;
            setHitboxToggle = false;
            clickCount = 0;
            hitBoxArray = new Hitbox[height, width];
        }

        public void CreateHitBoxes() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    hitBoxArray[i, j] = new Hitbox(false, false, j, i, HitBoxSize);
                }
            }
        }
        public void SetHitbox(UI ui) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (ui.Musknappar() && ui.RecChecker(hitBoxArray[i, j].rectangle)) {
                        hitBoxArray[i, j].SetHitbox();
                    }

                }
            }
        }
        public void WriteGrid() {
            TextWriter tw = new StreamWriter("SavedList.txt");
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    string temp = "";
                    
                    if (hitBoxArray[i,j].WallBox) {
                        temp += i + ";" + j;
                        temp += ":" + hitBoxArray[i, j].WallBox.ToString();
                        temp += "," + hitBoxArray[i, j].ZoneBox.ToString();
                        tw.WriteLine(temp);
                    } else if (hitBoxArray[i,j].ZoneBox) {
                        temp += i + ";" + j;
                        temp += ":" + hitBoxArray[i, j].WallBox.ToString();
                        temp += "," + hitBoxArray[i, j].ZoneBox.ToString();
                        tw.WriteLine(temp);
                    }
                    
                }
            }
            tw.Close();
        }
        public void LoadGrid() {
            string[] allLines = File.ReadAllLines(saveFile);
            for (int i = 0; i < allLines.Length; i++) {
                string[] temp = allLines[i].Split(':');
                string[] numbers = temp[0].Split(';');
                string[] booleans = temp[1].Split(',');

                int iNum = Int32.Parse(numbers[0]);
                int jNum = Int32.Parse(numbers[1]);
                bool wallBox = false;
                bool zoneBox = false;
                if (booleans[0] == "True") {
                    wallBox = true;
                }
                if (booleans[1] == "True") {
                    zoneBox = true;
                }

                hitBoxArray[iNum, jNum].WallBox = wallBox;
                hitBoxArray[iNum, jNum].ZoneBox = zoneBox;
            }
        }

    }
    
}
