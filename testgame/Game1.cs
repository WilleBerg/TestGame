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
        public int currentFrameCount = 0;
        public int textureCount = 0;

        Texture2D charFrontTexture;
        Texture2D testZone;
        Texture2D menuTexture;
        Texture2D startTexture;
        Texture2D settingsTexture;
        Texture2D exitTexture;
        Texture2D charBackTexture;
        Texture2D charRightTexture;
        Texture2D charLeftTexture;

        Texture2D charFrontAnimation1;
        Texture2D charFrontAnimation2;
        Texture2D charBackAnimation1;
        Texture2D charBackAnimation2;
        Texture2D charRightAnimation1;
        Texture2D charRightAnimation2;
        Texture2D charLeftAnimation1;
        Texture2D charLeftAnimation2;

        Vector2 ballvector = new Vector2( resX / 2 - 65, resY / 2);
        Vector2 testZoneVector;

        Menu menu = new Menu();

        CharGraphics ballGraphics = new CharGraphics();

        UI ui = new UI();

        PC character = new PC();
        Animation frontAnimation = new Animation();
        Animation backAnimation = new Animation();
        Animation rightSideAnimation = new Animation();
        Animation leftSideAnimation = new Animation();

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

            testZone = Content.Load<Texture2D>("game1testpic");
            menuTexture = Content.Load<Texture2D>("menu");
            startTexture = Content.Load<Texture2D>("start");
            settingsTexture = Content.Load<Texture2D>("settings");
            exitTexture = Content.Load<Texture2D>("exit");

            ui = new UI();

            ballGraphics = new CharGraphics(charFrontTexture);
            zoneGraphics = new ZoneGraphics(testZone, resX, resY);
            menu = new Menu(menuTexture);

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




            character = new PC(ballvector, ballGraphics, pcMovementSpeed, new List<Animation> { frontAnimation });
            currentCharacters.Add(character);
            character.AddAnimation(backAnimation);
            character.AddAnimation(rightSideAnimation);
            character.AddAnimation(leftSideAnimation);

            zone = new Zone(testZoneVector, zoneGraphics, currentCharacters, character);



            debug = Content.Load<SpriteFont>("debug");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ui.UpdateStates();
            kukollon = "Zone Coordinates; X: " + zone.vector.X + " Y: " + zone.vector.Y + "     Ball Coordinates; X: " + character.vector.X + " Y: " + character.vector.Y + "\n " +
                menu.alpha + "   Frame Count " + currentFrameCount + "   Texture Count " + textureCount ;
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
                    DrawCharacter(character);
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
            for (int i = 0; i < character.animation.Count; i++) {
                bool doubleKey1 = (ui.DownD() || ui.DownA()) && ui.DownW();
                bool doubleKey2 = ui.DownS() && (ui.DownD() || ui.DownA());
                if ((ui.DownS() || doubleKey2) && character.animation[i].AnimationType() == 0) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);
                } else if ((ui.DownW() || doubleKey1) && character.animation[i].AnimationType() == 1) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);
                } else if (ui.DownA() && !doubleKey1 && !doubleKey2 && character.animation[i].AnimationType() == 2) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);
                } else if (ui.DownD() && !doubleKey1 && !doubleKey2 && character.animation[i].AnimationType() == 3) {
                    character.latestTexture = character.animation[i].textureList[0];
                    character.latestAnimation = character.animation[i];
                    DrawAnimation(character.animation[i], character);
                } else if (!ui.DownA() && !ui.DownD() && !ui.DownS() && !ui.DownW()) {
                    textureCount++;
                    _spriteBatch.Draw(character.latestTexture, character.vector, Color.White);
                } 
            }
        }
        void DrawAnimation(Animation animation, Char character) {
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
            
            _spriteBatch.DrawString(debug, kukollon, new Vector2(0, 0), Color.Red);
            _spriteBatch.End();

        }
    }
}
