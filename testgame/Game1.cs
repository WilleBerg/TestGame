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

        Vector2 ballvector = new Vector2( resX / 2 - 65, resY / 2);
        Vector2 testZoneVector;

        CharGraphics ballGraphics = new CharGraphics();

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

            ballGraphics = new CharGraphics(ballTexture);
            zoneGraphics = new ZoneGraphics(testZone, resX, resY);

            ball = new PC(ballvector, ballGraphics, pcMovementSpeed);
            currentCharacters.Add(ball);

            zone = new Zone(testZoneVector, zoneGraphics, currentCharacters, ball);

            debug = Content.Load<SpriteFont>("debug");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState state = Keyboard.GetState();
            kukollon = "Zone Coordinates; X: " + zone.vector.X + " Y: " + zone.vector.Y + "     Ball Coordinates; X: " + ball.vector.X + " Y: " + ball.vector.Y ;
            zone.Move(state);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();
            DrawZone(zone);
            DrawCharacter(ball);
            _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.Red);
            _spriteBatch.End();

            

            base.Draw(gameTime);
        }

        void DrawCharacter(Char character) {
            _spriteBatch.Draw(character.graphics.texture, character.vector, Color.White);
        }
        void DrawZone(World zone) {
            _spriteBatch.Draw(zone.graphics.texture, zone.vector, Color.White);
        }
    }
}
