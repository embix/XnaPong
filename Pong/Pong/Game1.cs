using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private GameObject _topWall;
        private GameObject _bottomWall;
        private GameObject _playerOne;
        private GameObject _playerTwo;
        private GameObject _ball;
        private Boolean _roundIsRunning;
        private KeyboardState _keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var wallTexture = Content.Load<Texture2D>("wall");
            _topWall = new GameObject(wallTexture,Vector2.Zero);
            _bottomWall = new GameObject(wallTexture, new Vector2(0, Window.ClientBounds.Height - wallTexture.Height));

            var paddleTexture = Content.Load<Texture2D>("paddle");
            Vector2 position;

            position = new Vector2(0, (Window.ClientBounds.Height - paddleTexture.Height)/2);
            _playerOne = new GameObject(paddleTexture, position);

            position = new Vector2((Window.ClientBounds.Width - paddleTexture.Width),
                                   (Window.ClientBounds.Height - paddleTexture.Height)/2);
            _playerTwo  = new GameObject(paddleTexture, position);

            var ballTexture = Content.Load<Texture2D>("ball");
            position = new Vector2(_playerOne.BoundingBox.Right + 1, (Window.ClientBounds.Height - ballTexture.Height)/2);
            _ball = new GameObject(ballTexture, position, new Vector2(8f, -8f));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(_roundIsRunning)
                _ball.Position += _ball.Velocity * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.Space))
                _roundIsRunning = true;
            if (_keyboardState.IsKeyDown(Keys.W)) {
                if (!_roundIsRunning)
                    _ball.Position.Y -= 10f;
                _playerOne.Position.Y -= 10f;
            }
            if (_keyboardState.IsKeyDown(Keys.S)) {
                if (!_roundIsRunning)
                    _ball.Position.Y += 10f;
                _playerOne.Position.Y += 10f;
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
                _playerTwo.Position.Y -= 10f;
            if (_keyboardState.IsKeyDown(Keys.Down))
                _playerTwo.Position.Y += 10f;

            CheckPaddleWallCollision();
            CheckBallCollision();

            base.Update(gameTime);
        }

        private void CheckBallCollision()
        {
            if (_ball.BoundingBox.Intersects(_topWall.BoundingBox))
            {
                _ball.Velocity.Y *= -1;
                _ball.Position += _ball.Velocity;
            }
            if (_ball.BoundingBox.Intersects(_bottomWall.BoundingBox))
            {
                _ball.Velocity.Y *= -1;
                _ball.Position += _ball.Velocity;
            }
            if (_ball.BoundingBox.Intersects(_playerOne.BoundingBox))
            {
                _ball.Velocity.X *= -1;
                _ball.Position += _ball.Velocity;
            }
            if (_ball.BoundingBox.Intersects(_playerTwo.BoundingBox))
            {
                _ball.Velocity.X *= -1;
                _ball.Position += _ball.Velocity;
            }

            if ((_ball.Position.X < -_ball.BoundingBox.Width) || (_ball.Position.X > Window.ClientBounds.Width))
            {
                _roundIsRunning = false;
                SetInStartPostion();
            }
        }

        private void SetInStartPostion()
        {
            _playerOne.Position.Y = (Window.ClientBounds.Height - _playerOne.BoundingBox.Height)/2;
            _playerTwo.Position.Y = (Window.ClientBounds.Height - _playerTwo.BoundingBox.Height) / 2;
            _ball.Position.X = _playerOne.BoundingBox.Right + 1;
            _ball.Position.Y = (Window.ClientBounds.Height - _ball.BoundingBox.Height)/2;
            _ball.Velocity = new Vector2(8f, -8f);
        }

        private void CheckPaddleWallCollision()
        {
            if (_playerOne.BoundingBox.Intersects(_topWall.BoundingBox))
                _playerOne.Position.Y = _topWall.BoundingBox.Bottom;
            if (_playerOne.BoundingBox.Intersects(_bottomWall.BoundingBox))
                _playerOne.Position.Y = _bottomWall.BoundingBox.Y -
                                        _playerOne.BoundingBox.Height;
            if (_playerTwo.BoundingBox.Intersects(_topWall.BoundingBox))
                _playerTwo.Position.Y = _topWall.BoundingBox.Bottom;
            if (_playerTwo.BoundingBox.Intersects(_bottomWall.BoundingBox))
                _playerTwo.Position.Y = _bottomWall.BoundingBox.Y -
                                        _playerTwo.BoundingBox.Height;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            
            _topWall.Draw(_spriteBatch);
            _bottomWall.Draw(_spriteBatch);
            _playerOne.Draw(_spriteBatch);
            _playerTwo.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
