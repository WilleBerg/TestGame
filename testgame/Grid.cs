using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame {
    [Serializable]
    public class Grid {
        private int width;
        private int height;
        private int hitBoxSize;
        private string saveFile;
        private bool showDisabledHitboxes;
        private bool setHitboxToggle;
        public Vector2 vectorDelta;
        [XmlIgnore]
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
            hitBoxArray = new Hitbox[height, width];
        }

        /// <summary>
        /// Fills the hitbox array with default hitboxes, the size Grid.HitboxSize.
        /// </summary>
        public void CreateHitBoxes() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    hitBoxArray[i, j] = new Hitbox(false, false, j, i, HitBoxSize);
                }
            }
        }
        /// <summary>
        /// Changes the property of the hitbox you click on while ingame.
        /// Only works in devmode and gridEdit toggled.
        /// </summary>
        /// <param name="ui"></param>
        public void SetHitbox(UI ui) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (ui.LeftMousePressed() && ui.RecChecker(hitBoxArray[i, j].Rectangle)) {
                        hitBoxArray[i, j].SetHitbox();
                    }

                }
            }
        }
        /// <summary>
        /// Writes the whole hitboxgrid to a .txt file.
        /// </summary>
        public void WriteGrid() {
            TextWriter tw = new StreamWriter("Hitboxes/SavedList.txt");
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

        public void SaveToXML(string filename) {
            using (var stream = new FileStream(filename, FileMode.Create)) {
                var XML = new XmlSerializer(typeof(Grid));
                XML.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Loades the grid from the saveFile .txt file.
        /// </summary>
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
