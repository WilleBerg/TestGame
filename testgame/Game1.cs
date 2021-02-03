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

        public static int resX = 1920;
        public static int resY = 1080;

        public static int targetResX = 1920;
        public static int targetResY = 1080;

        public static int pcMovementSpeed = 5;
        public static int currentFrameCount = 0;
        public static int textureCount = 0;

        public static List<Keys> notAllowedKeys = new List<Keys>();

        bool toggleGrid = false;
        bool switchedZone = false;
        bool allowedToSwitchZone = true;
        Zone nextZone;

        float changeZoneColorAlpha = 1.0f;

        Color zoneSwitchColor = new Color(Color.White, 1.0f);

        public static World world = new World();

        public enum GameState {
            TitleScreen,
            Settings,
            InGame,
            Pause,
            Exit
        };
        public enum LoadStates {
            FirstRun,
            TitleScreen,
            Settings,
            BedRoom,
            Hallway,
            Pause
        };

        public LoadStates currentLoadState = LoadStates.FirstRun;
        public static GameState currentGameState = GameState.TitleScreen;


        public ContentHandler contentHandler;

        // Menu/UI textures
        Texture2D testZone;
        Texture2D menuTexture;
        Texture2D startTexture;
        Texture2D settingsTexture;
        Texture2D settingsMenuTexture;
        Texture2D devToggleTexture;
        Texture2D returnTexture;
        Texture2D exitTexture;


        //Room textures
        Texture2D rumTexture;
        Texture2D hallwayTexture;

        //Background textures
        Texture2D tree1LargeTexture;
        Texture2D tree1SmallTexture;
        Texture2D hallwayBackgroundLarge;
        Texture2D black;


        Texture2D testSprite;

        // Main character textures
        Texture2D charBackTexture;
        Texture2D charRightTexture;
        Texture2D charLeftTexture;
        Texture2D charFrontTexture;
        Texture2D charFrontAnimation1;
        Texture2D charFrontAnimation2;
        Texture2D charBackAnimation1;
        Texture2D charBackAnimation2;
        Texture2D charRightAnimation1;
        Texture2D charRightAnimation2;
        Texture2D charLeftAnimation1;
        Texture2D charLeftAnimation2;
        Texture2D charHitboxTexture;


        Texture2D gridTexture;

        Vector2 ballvector = new Vector2(resX / 2 - 65, resY / 2);
        Vector2 testZoneVector;

        Menu menu = new Menu();

        CharGraphics ballGraphics = new CharGraphics();

        public static UI ui = new UI();

        Settings settings = new Settings();

        PC character = new PC();
        Animation frontAnimation = new Animation();
        Animation backAnimation = new Animation();
        Animation rightSideAnimation = new Animation();
        Animation leftSideAnimation = new Animation();

        Zone zone = new Zone();
        ZoneGraphics zoneGraphics = new ZoneGraphics();
        Zone hallwayZone = new Zone();
        ZoneGraphics hallwayGraphics = new ZoneGraphics();
        Grid roomGrid = new Grid(240, 135, 12, "FirstRoomHitbox.txt");
        Grid hallwayGrid = new Grid(240, 135, 12, "HallwayRoomHitbox.txt");
        private FrameCounter _frameCounter = new FrameCounter();

        List<Character> currentCharacters = new List<Character>();
        Background hallwayBackground = new Background();



        SpriteFont debug;

        string kukollon = "0,0";

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentHandler = new ContentHandler(Content.ServiceProvider, Content.RootDirectory);
            IsMouseVisible = true;


        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = resX;
            _graphics.PreferredBackBufferHeight = resY;
            //_graphics.ToggleFullScreen();
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            

            //Character and character animation textures

            black = Content.Load<Texture2D>("Background/black");
            gridTexture = Content.Load<Texture2D>("Misc/gridSquare");
            debug = Content.Load<SpriteFont>("debug");

            switch (world.CurrentLoadState) {
                case LoadStates.FirstRun: // TODO: Move this to initialize. Probably smarter than this solution. That way you dont have to LoadContent inside firstrun
                    ui = new UI();

                    ballGraphics = new CharGraphics(charFrontTexture);
                    zoneGraphics = new ZoneGraphics(rumTexture, resX, resY);
                    hallwayGraphics = new ZoneGraphics(hallwayTexture, resX, resY);
                    menu = new Menu(menuTexture);
                    settings = new Settings(settingsMenuTexture);


                    // PC animation textures getting loaded
                    frontAnimation = new Animation(new List<Texture2D> { charFrontTexture }, "front");
                    frontAnimation.AddTexture(charFrontAnimation1);
                    frontAnimation.AddTexture(charFrontAnimation2);


                    backAnimation = new Animation(new List<Texture2D> { charBackTexture }, "back");
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

                    // Creation of the first rooms grid
                    roomGrid.CreateHitBoxes();
                    roomGrid.LoadGrid();

                    hallwayGrid.CreateHitBoxes();
                    hallwayGrid.LoadGrid();

                    // PC gets created and animations gets added.
                    character = new PC(ballvector, ballGraphics, pcMovementSpeed, new List<Animation> { frontAnimation },
                                new Rectangle((int)ballvector.X, (int)ballvector.Y, (int)( 66 * 1.5 ), (int)( 108 * 1.5 )));

                    currentCharacters.Add(character);
                    character.AddAnimation(backAnimation);
                    character.AddAnimation(rightSideAnimation);
                    character.AddAnimation(leftSideAnimation);

                    zone = new Zone(contentHandler, testZoneVector, zoneGraphics, currentCharacters, character, roomGrid);
                    zone.StartVector = new Vector2(915, 470);
                    zone.ZoneStartVector = new Vector2(-280, -20);
                    zone.setState(LoadStates.BedRoom);

                    hallwayBackground = new Background(hallwayBackgroundLarge, new Vector2(-200, -330), new Vector2(-200, -330), 3);
                    hallwayZone = new Zone(contentHandler, new Vector2(0, -456), hallwayGraphics, currentCharacters,
                        character, hallwayGrid, hallwayBackground, new Vector2(826, 456));
                    hallwayZone.ZoneStartVector = new Vector2(0, -456);
                    hallwayZone.setState(LoadStates.Hallway);

                    // -280

                    world.CurrCharacters.Add(character);
                    world.CurrentZone = zone;
                    world.PlayableCharacter = character;
                    for (int i = 97; i < 109; i++) {
                        roomGrid.hitBoxArray[37, i].connectedZone = hallwayZone;
                    }
                    for (int i = 68; i < 78; i++) {
                        hallwayGrid.hitBoxArray[55, i].connectedZone = zone;
                    }
                    
                    world.CurrentLoadState = LoadStates.TitleScreen;
                    LoadContent();
                    break;
                    
                case LoadStates.TitleScreen:
                    settingsMenuTexture = Content.Load<Texture2D>("UI/Large/settingsTexture");
                    testZone = Content.Load<Texture2D>("game1testpic");
                    menu.menuTexture = Content.Load<Texture2D>("UI/Large/menu");
                    startTexture = Content.Load<Texture2D>("UI/Large/start");
                    settingsTexture = Content.Load<Texture2D>("UI/Large/settings");
                    exitTexture = Content.Load<Texture2D>("UI/Large/exit");
                    break;
                case LoadStates.Settings:
                    returnTexture = Content.Load<Texture2D>("UI/Large/return");
                    devToggleTexture = Content.Load<Texture2D>("UI/Large/enableDev");
                    break;
                case LoadStates.BedRoom:
                    zone.graphics.texture = Content.Load<Texture2D>("Zone/Large/rumstor3");
                    break;
                case LoadStates.Hallway:
                    hallwayZone.graphics.texture = Content.Load<Texture2D>("Zone/Large/hallwayv2");
                    tree1LargeTexture = Content.Load<Texture2D>("Background/tree1large");
                    tree1SmallTexture = Content.Load<Texture2D>("Background/tree1");
                    hallwayBackground.Texture = Content.Load<Texture2D>("Background/hallwayBackgroundLarge");
                    break;
            }

            // Background

            character.animation[0].textureList[0] = Content.Load<Texture2D>("Character/Large/charfront");
            character.animation[0].textureList[1] = Content.Load<Texture2D>("Character/Large/charfronanimation1");
            character.animation[0].textureList[2] = Content.Load<Texture2D>("Character/Large/charfronanimation2");

            character.animation[1].textureList[0] = Content.Load<Texture2D>("Character/Large/charback");
            character.animation[1].textureList[1] = Content.Load<Texture2D>("Character/Large/charbackanimation1");
            character.animation[1].textureList[2] = Content.Load<Texture2D>("Character/Large/charbackanimation2");

            character.animation[2].textureList[0] = Content.Load<Texture2D>("Character/Large/charRight");
            character.animation[2].textureList[1] = Content.Load<Texture2D>("Character/Large/charRightAnimation1");
            character.animation[2].textureList[2] = Content.Load<Texture2D>("Character/Large/charRight");
            character.animation[2].textureList[3] = Content.Load<Texture2D>("Character/Large/charRightAnimation2");
            character.animation[2].textureList[4] = Content.Load<Texture2D>("Character/Large/charRight");

            character.animation[3].textureList[0] = Content.Load<Texture2D>("Character/Large/charLeft");
            character.animation[3].textureList[1] = Content.Load<Texture2D>("Character/Large/charLeftAnimation1");
            character.animation[3].textureList[2] = Content.Load<Texture2D>("Character/Large/charLeft");
            character.animation[3].textureList[3] = Content.Load<Texture2D>("Character/Large/charLeftAnimation2");
            character.animation[3].textureList[4] = Content.Load<Texture2D>("Character/Large/charLeft");

            character.graphics.texture = Content.Load<Texture2D>("Character/Large/charfront");
            character.latestTexture = character.animation[0].textureList[0];

            charHitboxTexture = Content.Load<Texture2D>("Character/Large/charHitboxTexture");
            testSprite = Content.Load<Texture2D>("Character/Tiny/charfrontSmall");



        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                WritePlayerPos();
                Exit();
            }

            ui.UpdateStates();

            // Horrible debug string.
            kukollon = "Zone Coordinates; X: " + world.CurrentZone.vector.X + " Y: " + world.CurrentZone.vector.Y + "     Ball Coordinates; X: " + character.vector.X + " Y: " + character.vector.Y +
                menu.alpha + "   Frame Count " + currentFrameCount + "   Texture Count " + textureCount + "     " + roomGrid.Height + "     " + roomGrid.Width + "\n"
                + "Grid view: " + toggleGrid + "\n" + "Grid Edit: " + world.CurrentZone.grid.SetHitboxToggle + " \n \n \n" + world.CurrentZone.grid.hitBoxArray[0, 0].ToString() + " Not allowed keys: ";
            for (int i = 0; i < notAllowedKeys.Count; i++) {
                kukollon += "   " + notAllowedKeys[i].ToString();
            }
            kukollon += "   Zone has background?  " + world.CurrentZone.HasBackground + "  ";
            // Makes it possible to move in the zone
            world.CurrentZone.Move(ui.keyboardState, ui);

            base.Update(gameTime);
        }
        void WritePlayerPos() {
            TextWriter tw = new StreamWriter("PlayerPosition.txt");
            string tmp = "";
            tmp = "Player Pos X: " + world.PlayableCharacter.vector.X + " Y: " + world.PlayableCharacter.vector.Y;
            tw.WriteLine(tmp);
            tw.Close();
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);

            kukollon += "FPS: " + fps;
            kukollon += "\n Mouse Pos: X; " + ui.mus.X + " Y; " + ui.mus.Y;

            Console.WriteLine("Fps: " + fps);
            Console.WriteLine(kukollon);


            switch (currentGameState) {
                case GameState.TitleScreen:
                    menu.Buttons(ui);
                    DrawMenu();
                    if (currentGameState != GameState.TitleScreen) {
                        Content.Unload();
                        world.CurrentLoadState = world.CurrentZone.loadState;
                        LoadContent();
                    }
                    break;
                case GameState.InGame:
                    _spriteBatch.Begin();
                    notAllowedKeys = ui.NotAllowedKeys(world.PlayableCharacter, world.CurrentZone.grid);
                    if (switchedZone) {
                        changeZoneColorAlpha -= 0.1f;
                        _spriteBatch.Draw(world.CurrentZone.graphics.texture, world.CurrentZone.vector, zoneSwitchColor * changeZoneColorAlpha);
                        if (changeZoneColorAlpha < 0) {
                            // Calculates the difference between the zones current position and the position
                            // it gets after zone switch. This is used when moving the nextZone.grid.
                            Vector2 delta = new Vector2(0, 0);
                            delta.X = nextZone.vector.X - nextZone.ZoneStartVector.X;
                            delta.Y = nextZone.vector.Y - nextZone.ZoneStartVector.Y;
                            world.PlayableCharacter.vector.X = nextZone.StartVector.X;
                            world.PlayableCharacter.vector.Y = nextZone.StartVector.Y;
                            world.CurrentZone = nextZone;
                            world.CurrentZone.vector = world.CurrentZone.ZoneStartVector;
                            for (int i = 0; i < world.CurrentZone.grid.Height; i++) {
                                for (int j = 0; j < world.CurrentZone.grid.Width; j++) {
                                    Vector2 tmp = world.CurrentZone.grid.hitBoxArray[i, j].Position;
                                    world.CurrentZone.grid.hitBoxArray[i, j].Position = new Vector2(tmp.X - delta.X, tmp.Y - delta.Y);
                                }
                            }
                            if (world.CurrentZone.HasBackground) { world.CurrentZone.Background.vector = world.CurrentZone.Background.startVector; }
                            changeZoneColorAlpha = 1;
                            _spriteBatch.End();
                            world.CurrentLoadState = world.CurrentZone.loadState;
                            Content.Unload();
                            LoadContent();
                            break;
                        }
                    } else {
                        DrawZone(world.CurrentZone);
                    }
                    DrawGrid(world.CurrentZone.grid);
                    if (ui.DownKey(Keys.M)) {
                        currentGameState = GameState.TitleScreen;
                        world.CurrentLoadState = LoadStates.TitleScreen;
                        Content.Unload();
                        LoadContent();
                        break;
                    }
                    if (settings.DevToggle) {
                        ToggleGridView(world.CurrentZone.grid);
                        if (world.CurrentZone.grid.SetHitboxToggle) {
                            world.CurrentZone.grid.SetHitbox(ui);
                            ResetGrid(world.CurrentZone.grid);
                        }
                        if (ui.KeyPressedAndReleased(Keys.P)) {
                            world.CurrentZone.grid.SetHitboxToggle = !world.CurrentZone.grid.SetHitboxToggle;
                        }
                        if (ui.KeyPressedAndReleased(Keys.U)) {
                            world.CurrentZone.grid.WriteGrid();
                        }
                        _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.White);
                    }

                    //roomGrid.SetHitBox(ui);
                    DrawPlayableCharacter(world.PlayableCharacter);
                    _spriteBatch.End();
                    break;
                // Settings
                case GameState.Settings:
                    settings.Buttons(ui, menu);
                    DrawSettings();
                    if (currentGameState != GameState.Settings) {
                        Content.Unload();
                    }
                    break;
                case GameState.Exit:
                    WritePlayerPos();
                    Exit();
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }
        /// <summary>
        /// Resets the grid by pressing L.
        /// </summary>
        /// <param name="grid"></param>
        public void ResetGrid(Grid grid) {
            if (ui.DownKey(Keys.L)) {
                for (int i = 0; i < grid.Height; i++) {
                    for (int j = 0; j < grid.Width; j++) {
                        grid.hitBoxArray[i, j].WallBox = false;
                        grid.hitBoxArray[i, j].ZoneBox = false;
                    }
                }
            }
        }
        /// <summary>
        /// With this method you can toggle if the grid should be drawn or not.
        /// </summary>
        /// <param name="grid">The grid that gets toggled</param>
        public void ToggleGridView(Grid grid) {
            if (ui.KeyPressedAndReleased(Keys.O)) {
                if (toggleGrid) {
                    toggleGrid = false;
                } else {
                    toggleGrid = true;
                }
            } else if (ui.KeyPressedAndReleased(Keys.I)) {
                grid.ShowDisabledHitBoxes = !grid.ShowDisabledHitBoxes;
            }
        }
        /// <summary>
        /// Draws the grid ingame. Also makes sure the vectors of the grid are correct. Needs to be used currently for grid funcionality.
        /// </summary>
        /// <param name="grid">The grid getting drawn.</param>
        public void DrawGrid(Grid grid) {
            switchedZone = false;
            Color recColor = new Color(Color.Black, 1.0f);
            for (int i = 0; i < grid.Height; i++) {
                for (int j = 0; j < grid.Width; j++) {
                    grid.hitBoxArray[i, j].rectangle.X = (int)grid.vectorDelta.X + (int)grid.hitBoxArray[i, j].Position.X;
                    grid.hitBoxArray[i, j].rectangle.Y = (int)grid.vectorDelta.Y + (int)grid.hitBoxArray[i, j].Position.Y;
                    if (!switchedZone) {
                        switchedZone = grid.hitBoxArray[i, j].ChangeZone(world.PlayableCharacter);
                    } else {
                        nextZone = grid.hitBoxArray[i, j].connectedZone;
                        break;
                    }
                    if (grid.hitBoxArray[i, j].WallBox && toggleGrid) {
                        _spriteBatch.Draw(gridTexture, grid.hitBoxArray[i, j].rectangle, Color.Red);
                    } else if (toggleGrid && grid.hitBoxArray[i, j].ZoneBox) {
                        _spriteBatch.Draw(gridTexture, grid.hitBoxArray[i, j].rectangle, Color.Blue);
                    } else if (toggleGrid && grid.ShowDisabledHitBoxes) {
                        _spriteBatch.Draw(gridTexture, grid.hitBoxArray[i, j].rectangle, Color.White);
                    }
                }
                if (switchedZone) {
                    break;
                }
            }
        }
        /// <summary>
        /// Draws the playable character and implements its animation.
        /// </summary>
        /// <param name="character"> The playable character that gets drawn</param>
        public void DrawPlayableCharacter(PC character) {
            character.hitbox.X = (int)character.vector.X;
            character.hitbox.Y = (int)character.vector.Y;
            if (settings.DevToggle) {
                _spriteBatch.Draw(charHitboxTexture, character.hitbox, Color.Blue);
            }
            for (int i = 0; i < character.animation.Count; i++) {

                bool doubleKey1 = ( ui.DownKey(Keys.D) || ui.DownKey(Keys.A) ) && ui.DownKey(Keys.W);
                bool doubleKey2 = ui.DownKey(Keys.S) && ( ui.DownKey(Keys.D) || ui.DownKey(Keys.A) );
                bool doubleKey3 = ui.DownKey(Keys.W) && ui.DownKey(Keys.S);
                bool doubleKey4 = ui.DownKey(Keys.D) && ui.DownKey(Keys.A);

                if (( ui.DownKey(Keys.S) || doubleKey2 ) && !doubleKey3 && character.animation[i].AnimationType() == 0) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);

                } else if (( ui.DownKey(Keys.W) || doubleKey1 ) && !doubleKey3 && character.animation[i].AnimationType() == 1) {
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
        /// <summary>
        /// Draws a characters animation. 
        /// </summary>
        /// <param name="animation">The animations that will get drawn</param>
        /// <param name="character">The character that has the animation</param>
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
        /// <summary>
        /// Draws a zone.
        /// </summary>
        /// <param name="zone">The zone that gets drawn</param>
        void DrawZone(Zone zone) {
            if (zone.HasBackground) {
                _spriteBatch.Draw(zone.Background.Texture, zone.Background.vector, Color.White);
            }
            _spriteBatch.Draw(zone.graphics.texture, zone.vector, Color.White);

        }
        /// <summary>
        /// Draws the main menu.
        /// </summary>
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

        /// <summary>
        /// Draws the settings in main menu.
        /// </summary>
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
