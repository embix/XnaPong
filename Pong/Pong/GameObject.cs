using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class GameObject
    {
        private readonly Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (Int32)Position.X,
                    (Int32)Position.Y,
                    _texture.Width,
                    _texture.Height);
            }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            _texture = texture;
            Position = position;
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
