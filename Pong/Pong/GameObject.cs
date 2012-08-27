using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class GameObject
    {
        protected readonly Texture2D Texture;// todo expose height to get middle (Y for ball)
        public Vector2 Position;//todo: { get; set; }
        public Vector2 Velocity;//todo: { get; set; }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (Int32)Position.X,
                    (Int32)Position.Y,
                    Texture.Width,
                    Texture.Height);
            }
        }

        public Int32 YCenter
        {
            get { return (int)(Position.Y + Texture.Height / 2); }
            set { Position.Y = value-Texture.Height/2; }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
