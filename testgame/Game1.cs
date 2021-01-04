using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace testgame {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int resX = 1280;
        public static int resY = 720;

        public int pcMovementSpeed = 5;

        Texture2D ballTexture;
        Texture2D testZone;
        Texture2D menuTexture;
        Texture2D startTexture;
        Texture2D settingsTexture;
        Texture2D exitTexture;

        Vector2 ballvector = new Vector2( resX / 2 - 65, resY / 2);
        Vector2 testZoneVector;

        Menu menu = new Menu();

        CharGraphics ballGraphics = new CharGraphics();

        UI ui = new UI();

        PC ball = new PC();

        Zone zone = new Zone();

        ZoneGraphics zoneGraphics = new ZoneGraphics();

        List<Char> currentCharacters = new List<Char>();

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

            ballTexture = Content.Load<Texture2D>("ball");
            testZone = Content.Load<Texture2D>("game1testpic");
            menuTexture = Content.Load<Texture2D>("menu");
            startTexture = Content.Load<Texture2D>("start");
            settingsTexture = Content.Load<Texture2D>("settings");
            exitTexture = Content.Load<Texture2D>("exit");

            ui = new UI();

            ballGraphics = new CharGraphics(ballTexture);
            zoneGraphics = new ZoneGraphics(testZone, resX, resY);
            menu = new Menu(menuTexture);

            ball = new PC(ballvector, ballGraphics, pcMovementSpeed);
            currentCharacters.Add(ball);

            zone = new Zone(testZoneVector, zoneGraphics, currentCharacters, ball);



            debug = Content.Load<SpriteFont>("debug");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ui.UpdateStates();
            kukollon = "Zone Coordinates; X: " + zone.vector.X + " Y: " + zone.vector.Y + "     Ball Coordinates; X: " + ball.vector.X + " Y: " + ball.vector.Y + "\n " +
                menu.alpha;
            zone.Move(ui.keyboardState);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (menu.switchKey) {
                case 0:
                    menu.MenuButtons(ui);
                    DrawMenu();
                    break;
                case 1:
                    _spriteBatch.Begin();
                    DrawZone(zone);
                    DrawCharacter(ball);
                    _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.Red);
                    _spriteBatch.End();
                    break;
                case 2000:
                    Exit();
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }

        void DrawCharacter(Char character) {
            _spriteBatch.Draw(character.graphics.texture, character.vector, Color.White);
        }
        void DrawZone(World zone) {
            _spriteBatch.Draw(zone.graphics.texture, zone.vector, Color.White);
        }
        void DrawMenu() {
            _spriteBatch.Begin();
            menu.ColorAlhpaChange(ui);
            
            _spriteBatch.Draw(menu.menuTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(startTexture, menu.startRec, Color.White * menu.alpha);
            _spriteBatch.Draw(settingsTexture, menu.settingsRec, menu.recColor * menu.alpha);
            _spriteBatch.Draw(exitTexture, menu.exitRec, menu.recColor);
            _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.Red);
            _spriteBatch.End();

        }
    }
}
