using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace testgame {
    [Serializable]
    public class Zone {
        public Zone() {

        }
        public Grid grid;
        public Zone[] connectedZones;
        public Graphics graphics;
        public Vector2 vector;
        public List<Character> currCharacters;
        public PC pc;
        public ContentHandler contentHandler;
        public Game1.LoadStates loadState;

        private bool shouldLoadContent;
        private List<string> textureStrings;
        private Vector2 startVector;
        private Vector2 zoneStartVector;
        private Background background;
        private bool hasBackground;
        private float alpha;


        public bool ShouldLoadContent { get { return shouldLoadContent; } set { shouldLoadContent = value; } }
        public List<string> TextureStrings { get { return textureStrings; } set { textureStrings = value; } }
        public float Alpha { get { return alpha; } set { alpha = value; } }
        public bool HasBackground { get { return hasBackground; } set { hasBackground = value; } }
        public Vector2 StartVector { get { return startVector; } set { startVector = value; } }
        public Background Background { get { return background; } set { background = value; } }
        public Vector2 ZoneStartVector { get { return zoneStartVector; } set { zoneStartVector = value; } }

        
        public Zone(ContentHandler contentHandler, Vector2 vector, WorldGraphics graphics, List<Character> currentCharacters, PC pc, Grid grid) {
            this.vector = vector;
            this.graphics = graphics;
            currCharacters = currentCharacters;
            this.pc = pc;
            this.grid = grid;
            this.hasBackground = false;
            alpha = 1;
            textureStrings = new List<string>();
        }
        public Zone(ContentHandler contentHandler, Vector2 vector, WorldGraphics graphics, List<Character> currentCharacters, PC pc, Grid grid, Background background, Vector2 startVector) {
            this.vector = vector;
            this.graphics = graphics;
            currCharacters = currentCharacters;
            this.pc = pc;
            this.grid = grid;
            this.background = background;
            this.startVector = startVector;
            hasBackground = true;
            alpha = 1;
            textureStrings = new List<string>();
        }

        public void setState(Game1.LoadStates loadState) {
            this.loadState = loadState;
        }

        //public void LoadTextures() {
        //    graphics.texture = contentHandler.Load<Texture2D>(textureStrings[0]);
        //}
        /// <summary>
        /// Moves the zone and character when using WASD ingame
        /// </summary>
        /// <param name="state">An updating KeyboardState</param>
        /// <param name="ui">The main user interface</param>
        public void Move(KeyboardState state, UI ui) {

            if (Game1.currentGameState == Game1.GameState.InGame) {
                if (!Game1.notAllowedKeys.Contains(Keys.D) 
                    && vector.X - pc.moveSpeed >= graphics.resX - graphics.texture.Width 
                    && state.IsKeyDown(Keys.D) 
                    && pc.vector.X >= graphics.resX / 2 - pc.graphics.texture.Width / 2) {
                    vector.X -= pc.moveSpeed;
                    grid.vectorDelta.X -= pc.moveSpeed;
                    if (hasBackground) {
                        background.vector.X -= background.Speed;
                    }

                } else if (!Game1.notAllowedKeys.Contains(Keys.D) && pc.vector.X + pc.moveSpeed <= graphics.resX - pc.graphics.texture.Width && state.IsKeyDown(Keys.D)) {
                    pc.vector.X += pc.moveSpeed;
                }
                if (!Game1.notAllowedKeys.Contains(Keys.A) && vector.X + pc.moveSpeed <= 0 && state.IsKeyDown(Keys.A) && pc.vector.X <= graphics.resX / 2 - pc.graphics.texture.Width / 2) {
                    vector.X += pc.moveSpeed;
                    grid.vectorDelta.X += pc.moveSpeed;
                    if (hasBackground) {
                        background.vector.X += background.Speed;
                    }

                } else if (!Game1.notAllowedKeys.Contains(Keys.A) && pc.vector.X >= 0 && state.IsKeyDown(Keys.A)) {
                    pc.vector.X -= pc.moveSpeed;
                }
                if (!Game1.notAllowedKeys.Contains(Keys.S) && vector.Y - pc.moveSpeed >= graphics.resY - graphics.texture.Height && state.IsKeyDown(Keys.S) && pc.vector.Y >= graphics.resY / 2 - pc.graphics.texture.Height) {
                    vector.Y -= pc.moveSpeed;
                    grid.vectorDelta.Y -= pc.moveSpeed;
                    if (hasBackground) {
                        background.vector.Y -= background.Speed;
                    }

                } else if (!Game1.notAllowedKeys.Contains(Keys.S) && pc.vector.Y + pc.moveSpeed <= ( graphics.resY - pc.graphics.texture.Height ) && state.IsKeyDown(Keys.S)) {
                    pc.vector.Y += pc.moveSpeed;
                }
                if (!Game1.notAllowedKeys.Contains(Keys.W) && vector.Y + pc.moveSpeed <= 0 && state.IsKeyDown(Keys.W) && pc.vector.Y <= graphics.resY / 2 - pc.graphics.texture.Height / 2) {
                    vector.Y += pc.moveSpeed;
                    grid.vectorDelta.Y += pc.moveSpeed;
                    if (hasBackground) {
                        background.vector.Y += background.Speed;
                    }

                } else if (!Game1.notAllowedKeys.Contains(Keys.W) && pc.vector.Y - pc.moveSpeed >= 0 && state.IsKeyDown(Keys.W)) {
                    pc.vector.Y -= pc.moveSpeed;
                } 
            }

        }


    }
}
