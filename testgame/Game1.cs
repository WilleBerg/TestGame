using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace testgame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int resX = 1280;
        public static int resY = 720;

        public static int pcMovementSpeed = 5;
        public static int currentFrameCount = 0;
        public static int textureCount = 0;

        public static List<Keys> notAllowedKeys = new List<Keys>();

        bool toggleGrid = false;
        

        Texture2D charFrontTexture;
        Texture2D testZone;
        Texture2D menuTexture;
        Texture2D startTexture;
        Texture2D settingsTexture;
        Texture2D settingsMenuTexture;
        Texture2D devToggleTexture;
        Texture2D returnTexture;
        Texture2D exitTexture;
        Texture2D charBackTexture;
        Texture2D charRightTexture;
        Texture2D charLeftTexture;
        Texture2D rumTexture;
        Texture2D charHitboxTexture;

        Texture2D charFrontAnimation1;
        Texture2D charFrontAnimation2;
        Texture2D charBackAnimation1;
        Texture2D charBackAnimation2;
        Texture2D charRightAnimation1;
        Texture2D charRightAnimation2;
        Texture2D charLeftAnimation1;
        Texture2D charLeftAnimation2;



        Texture2D gridTexture;

        Vector2 ballvector = new Vector2( resX / 2 - 65, resY / 2);
        Vector2 testZoneVector;

        Menu menu = new Menu();

        CharGraphics ballGraphics = new CharGraphics();

        UI ui = new UI();

        Settings settings = new Settings();

        PC character = new PC();
        Animation frontAnimation = new Animation();
        Animation backAnimation = new Animation();
        Animation rightSideAnimation = new Animation();
        Animation leftSideAnimation = new Animation();

        Zone zone = new Zone();
        ZoneGraphics zoneGraphics = new ZoneGraphics();
        public Grid roomGrid = new Grid(160, 90, new Vector2(0,0), 12, 12);
        
        

        List<Character> currentCharacters = new List<Character>();

        SpriteFont debug;

        string kukollon = "0,0";

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = resX;
            _graphics.PreferredBackBufferHeight = resY;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            charFrontTexture = Content.Load<Texture2D>("charfront");
            charBackTexture = Content.Load<Texture2D>("charback");
            charRightTexture = Content.Load<Texture2D>("charRight");
            charLeftTexture = Content.Load<Texture2D>("charLeft");
            charFrontAnimation1 = Content.Load<Texture2D>("charfronanimation1");
            charFrontAnimation2 = Content.Load<Texture2D>("charfronanimation2");
            charBackAnimation1 = Content.Load<Texture2D>("charbackanimation1");
            charBackAnimation2 = Content.Load<Texture2D>("charbackanimation2");
            charRightAnimation1 = Content.Load<Texture2D>("charRightAnimation1");
            charRightAnimation2 = Content.Load<Texture2D>("charRightAnimation2");
            charLeftAnimation1 = Content.Load<Texture2D>("charLeftAnimation1");
            charLeftAnimation2 = Content.Load<Texture2D>("charLeftAnimation2");
            rumTexture = Content.Load<Texture2D>("rumstor3");
            gridTexture = Content.Load<Texture2D>("gridSquare");
            settingsMenuTexture = Content.Load<Texture2D>("settingsTexture");
            returnTexture = Content.Load<Texture2D>("return");
            devToggleTexture = Content.Load<Texture2D>("enableDev");
            charHitboxTexture = Content.Load<Texture2D>("charHitboxTexture");

            testZone = Content.Load<Texture2D>("game1testpic");
            menuTexture = Content.Load<Texture2D>("menu");
            startTexture = Content.Load<Texture2D>("start");
            settingsTexture = Content.Load<Texture2D>("settings");
            exitTexture = Content.Load<Texture2D>("exit");

            ui = new UI();

            ballGraphics = new CharGraphics(charFrontTexture);
            zoneGraphics = new ZoneGraphics(rumTexture, resX, resY);
            menu = new Menu(menuTexture);
            settings = new Settings(settingsMenuTexture);

            frontAnimation = new Animation(new List<Texture2D> { charFrontTexture }, "front");
            frontAnimation.AddTexture(charFrontAnimation1);
            frontAnimation.AddTexture(charFrontAnimation2);
            

            backAnimation = new Animation(new List<Texture2D> { charBackTexture  }, "back");
            backAnimation.AddTexture(charBackAnimation1);
            backAnimation.AddTexture(charBackAnimation2);

            rightSideAnimation = new Animation(new List<Texture2D> { charRightTexture }, "rightSide");
            rightSideAnimation.AddTexture(charRightAnimation1);
            rightSideAnimation.AddTexture(charRightTexture);
            rightSideAnimation.AddTexture(charRightAnimation2);
            rightSideAnimation.AddTexture(charRightTexture);

            leftSideAnimation = new Animation(new List<Texture2D> { charLeftTexture }, "leftSide");
            leftSideAnimation.AddTexture(charLeftAnimation1);
            leftSideAnimation.AddTexture(charLeftTexture);
            leftSideAnimation.AddTexture(charLeftAnimation2);
            leftSideAnimation.AddTexture(charLeftTexture);

            roomGrid.CreateRectangleArray();
            roomGrid.SetVectors();
            roomGrid.BoolGrid = new bool[roomGrid.Height, roomGrid.Width];
            roomGrid.LoadGrid();


            character = new PC(ballvector, ballGraphics, pcMovementSpeed, new List<Animation> { frontAnimation }, new Rectangle((int) ballvector.X, (int) ballvector.Y, 66, 108));
            currentCharacters.Add(character);
            character.AddAnimation(backAnimation);
            character.AddAnimation(rightSideAnimation);
            character.AddAnimation(leftSideAnimation);

            zone = new Zone(testZoneVector, zoneGraphics, currentCharacters, character, roomGrid);



            debug = Content.Load<SpriteFont>("debug");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
                roomGrid.WriteGrid();
            }
                
            ui.UpdateStates();
            kukollon = "Zone Coordinates; X: " + zone.vector.X + " Y: " + zone.vector.Y + "     Ball Coordinates; X: " + character.vector.X + " Y: " + character.vector.Y +
                menu.alpha + "   Frame Count " + currentFrameCount + "   Texture Count " + textureCount + "     " + roomGrid.Height + "     " + roomGrid.Width + "\n" 
                + "Grid view: " + toggleGrid + "\n" + "Grid Edit: " + roomGrid.SetHitboxToggle;
            for (int i = 0; i < notAllowedKeys.Count; i++) {
                kukollon += "   " + notAllowedKeys[i].ToString();
            }
            zone.Move(ui.keyboardState, ui);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (menu.switchKey) {
                case 0:
                    menu.Buttons(ui);
                    DrawMenu();
                    break;
                // Main game;
                case 1:
                    _spriteBatch.Begin();
                    notAllowedKeys = ui.NotAllowedKeys(character, roomGrid);
                    DrawZone(zone);
                    DrawGrid(roomGrid);
                    if (ui.DownKey(Keys.M)) {
                        menu.switchKey = 0;
                    }
                    if (settings.DevToggle) {
                        ToggleGridView(roomGrid);
                        if (roomGrid.SetHitboxToggle) {
                            roomGrid.SetHitBox(ui);
                            ResetGrid(roomGrid);
                        }
                        if (ui.KeyPressedAndReleased(Keys.P)) {
                            roomGrid.SetHitboxToggle = !roomGrid.SetHitboxToggle;
                        }
                        _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.White);
                    }

                    //roomGrid.SetHitBox(ui);
                    DrawPlayableCharacter(character);
                    _spriteBatch.End();
                    break;
                // Settings
                case 2:
                    settings.Buttons(ui, menu);
                    DrawSettings();
                    break;
                case 2000:


                    roomGrid.WriteGrid();

                    Exit();
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }
        
        public void ResetGrid(Grid grid) {
            if (ui.DownKey(Keys.L)) {
                for (int i = 0; i < grid.Height; i++) {
                    for (int j = 0; j < grid.Width; j++) {
                        grid.BoolGrid[i, j] = false;
                    }
                }
            }
        }
        public void ToggleGridView(Grid grid) {
            if (ui.KeyPressedAndReleased(Keys.O)) {
                if (toggleGrid) {
                    toggleGrid = false;
                } else {
                    toggleGrid = true;
                }
            } else if (ui.KeyPressedAndReleased(Keys.I)) {
                grid.showDisabledHitBoxes = !grid.showDisabledHitBoxes;
            }
        }
        public void DrawGrid(Grid grid) {
            for (int i = 0; i < grid.Height; i++) {
                for (int j = 0; j < grid.Width; j++) {
                    grid.hitBoxArray[i, j].X = (int)grid.vectorDelta.X + (int)grid.VectorArray[i, j].X;
                    grid.hitBoxArray[i, j].Y = (int)grid.vectorDelta.Y + (int)grid.VectorArray[i, j].Y;
                    if (grid.BoolGrid[i,j] && toggleGrid) {
                        _spriteBatch.Draw(gridTexture, grid.hitBoxArray[i,j], Color.Red);
                    } else if (toggleGrid && grid.showDisabledHitBoxes) {
                        _spriteBatch.Draw(gridTexture, grid.hitBoxArray[i, j], Color.White);
                    }
                }
            }
        }
        /// <summary>
        /// Draws the playable character and implements its animation.
        /// </summary>
        /// <param name="character"> The playable character that gets drawn</param>
        public void DrawPlayableCharacter(PC character) {
            character.hitbox.X = (int) character.vector.X;
            character.hitbox.Y = (int) character.vector.Y;
            if (settings.DevToggle) {
                _spriteBatch.Draw(charHitboxTexture, character.hitbox, Color.Blue);
            }
            for (int i = 0; i < character.animation.Count; i++) {

                bool doubleKey1 = (ui.DownKey(Keys.D) || ui.DownKey(Keys.A)) && ui.DownKey(Keys.W);
                bool doubleKey2 = ui.DownKey(Keys.S) && (ui.DownKey(Keys.D) || ui.DownKey(Keys.A));
                bool doubleKey3 = ui.DownKey(Keys.W) && ui.DownKey(Keys.S);
                bool doubleKey4 = ui.DownKey(Keys.D) && ui.DownKey(Keys.A);

                if ((ui.DownKey(Keys.S) || doubleKey2) && !doubleKey3 && character.animation[i].AnimationType() == 0) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);

                } else if ((ui.DownKey(Keys.W) || doubleKey1) && !doubleKey3 && character.animation[i].AnimationType() == 1) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);

                } else if (ui.DownKey(Keys.A) && !doubleKey1 && !doubleKey4 && !doubleKey2 && character.animation[i].AnimationType() == 2) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);

                } else if (ui.DownKey(Keys.D) && !doubleKey1 && !doubleKey4 && !doubleKey2 && character.animation[i].AnimationType() == 3) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);

                } else if (!ui.DownKey(Keys.A) && !ui.DownKey(Keys.D) && !ui.DownKey(Keys.S) && !ui.DownKey(Keys.W)) {
                    _spriteBatch.Draw(character.latestTexture, character.vector, Color.White);

                } else if (doubleKey3 || doubleKey4) {
                    _spriteBatch.Draw(character.latestTexture, character.vector, Color.White);
                }
            }
        }
        public void DrawAnimation(Animation animation, Character character) {
            currentFrameCount = animation.frameCount;
            if (animation.currAnimation > animation.textureList.Count - 1) {
                _spriteBatch.Draw(character.latestTexture, character.vector, Color.White);
                animation.currAnimation = 1;
                animation.frameCount = 0;
            } else {
                _spriteBatch.Draw(animation.textureList[animation.currAnimation], character.vector, Color.White);
                animation.frameCount++;
                if (animation.frameCount > 30) {
                    animation.currAnimation++;
                    animation.frameCount = 0;
                }
            }
        }
        void DrawZone(World zone) {
            _spriteBatch.Draw(zone.graphics.texture, zone.vector, Color.White);
        }
        void DrawMenu() {
            _spriteBatch.Begin();
            menu.ColorAlhpaChange(ui);
            
            _spriteBatch.Draw(menu.menuTexture, new Vector2(0, 0), Color.White);
            if (ui.RecChecker(menu.startRec)) {
                _spriteBatch.Draw(startTexture, menu.startRec, Color.White * menu.alpha);
            } else {
                _spriteBatch.Draw(startTexture, menu.startRec, Color.Transparent);
            }
            if (ui.RecChecker(menu.settingsRec)) {
                _spriteBatch.Draw(settingsTexture, menu.settingsRec, menu.recColor * menu.alpha);
            } else {
                _spriteBatch.Draw(settingsTexture, menu.settingsRec, Color.Transparent);
            }
            if (ui.RecChecker(menu.exitRec)) {
                _spriteBatch.Draw(exitTexture, menu.exitRec, menu.recColor * menu.alpha);
            } else {
                _spriteBatch.Draw(exitTexture, menu.exitRec, Color.Transparent);
            }
            if (settings.DevToggle) {
                _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.Red);
            }
            

            _spriteBatch.End();

        }
        public void DrawSettings() {
            _spriteBatch.Begin();
            settings.ColorAlhpaChange(ui);
            _spriteBatch.Draw(settings.settingsTexture, new Vector2(0, 0), Color.White);
            if (ui.RecChecker(settings.developerButton)) {
                _spriteBatch.Draw(devToggleTexture, settings.developerButton, Color.White * settings.alpha);
            } else {
                _spriteBatch.Draw(devToggleTexture, settings.developerButton, Color.Transparent);
            }
            if (ui.RecChecker(settings.returnButton)) {
                _spriteBatch.Draw(returnTexture, settings.returnButton, Color.White * settings.alpha);
            } else {
                _spriteBatch.Draw(returnTexture, settings.returnButton, Color.Transparent);
            }
            _spriteBatch.End();
        }
    }
}
