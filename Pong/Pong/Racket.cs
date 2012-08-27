using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Racket : GameObject
    {
        private const Single BasePlayerSpeed = 10f;
        private Boolean _wasMovedOnLastUpdate;

        public Racket(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public Racket(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {
        }

        public void Update(GameTime gameTime)
        {
            var updateSpan = 50*(float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_wasMovedOnLastUpdate)
            {
                Position += Velocity*updateSpan;
                ResetMovingState();
            }
        }

        public void ResetMovingState()
        {
            Velocity = new Vector2(0,0);
            _wasMovedOnLastUpdate = false;// reset state
        }

        public void MoveUp()
        {
            Velocity.Y -= BasePlayerSpeed;
            _wasMovedOnLastUpdate = true;
        }

        public void MoveDown()
        {
            Velocity.Y += BasePlayerSpeed;
            _wasMovedOnLastUpdate = true;
        }

        
    }
}
