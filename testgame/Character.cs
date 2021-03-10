using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace testgame {
    public abstract class Character : IMovable, IHittable {
        protected CharGraphics graphics;
        protected Vector2 vector;
        protected int moveSpeed;
        protected List<Animation> animation;
        protected Texture2D latestTexture;
        protected Animation latestAnimation;
        protected Rectangle hitbox;
        protected string[] characterTextureStrings;
        protected List<Animation> animation1;
        protected Texture2D latestTexture1;

        public CharGraphics Graphics {
            get { return graphics; }
            set { graphics = value; }
        }
        public int MoveSpeed {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public List<Animation> Animation { get => animation; set => animation = value; }
        public Texture2D LatestTexture { get => latestTexture; set => latestTexture = value; }
        public Animation LatestAnimation { get => latestAnimation; set => latestAnimation =  value ; }
        public Rectangle Hitbox { get => hitbox; set => hitbox =  value ; }
        public string[] CharacterTextureStrings { get => characterTextureStrings; set => characterTextureStrings =  value ; }
        public List<Animation> Animation1 { get => animation1; set => animation1 =  value ; }
        public Texture2D LatestTexture1 { get => latestTexture1; set => latestTexture1 =  value ; }
        public Vector2 Vector { get => vector; set => vector =  value ; }

        public Character() {

        }
        public Character(Vector2 vector, CharGraphics graphics, int moveSpeed, List<Animation> animation, Rectangle rectangle) {
            this.vector = vector;
            this.graphics = graphics;
            this.moveSpeed = moveSpeed;
            this.animation = animation;
            hitbox = rectangle;
            latestTexture = graphics.texture;
            latestAnimation = animation[0];
        }

        public float getX() {
            return vector.X;
        }

        public float getY() {
            return vector.Y;
        }

        public void setX(float input) {
            vector.X = input;
        }

        public void setY(float input) {
            vector.Y = input;
        }

        public void SetHitboxX(int value) {
            hitbox.X = value;
        }

        public void SetHitBoxY(int value) {
            hitbox.Y = value;
        }

        public int GetHitBoxX() {
            return hitbox.X;
        }

        public int GetHitBoxY() {
            return hitbox.Y;
        }
    }
}
