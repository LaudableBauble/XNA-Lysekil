using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _square;
        private Vector2 _stop;
        private List<Figure> _figures;
        private InputState _input;
        private int _cellWidth;
        private float _gravity;

        private Vector2 _move;
        private int _rotate;
        private Figure _currentFigure;

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
            // TODO: Add your initialization logic here.
            this.IsMouseVisible = true;

            // Window size
            _graphics.PreferredBackBufferWidth = 16 * 10;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            _input = new InputState();
            _figures = new List<Figure>();
            _currentFigure = new Figure();
            _figures.Add(_currentFigure);
            _currentFigure.Position = new Vector2(16 * 5, 0);
            _cellWidth = 16;
            _gravity = 16;

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

            // TODO: use this.Content to load your game content here
            _square = Content.Load<Texture2D>("Square[1]");
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
            // Allows the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) { this.Exit(); }
            if (_input.IsKeyDown(Keys.Escape)) { this.Exit(); }

            //Read the keyboard and gamepad.
            _input.Update();
            _move = Vector2.Zero;

            if (_currentFigure.IsSleeping)
            {
                _currentFigure = new Figure();
                _currentFigure.Position = new Vector2(16 * 5, 0);
                _figures.Add(_currentFigure);
            }

            //Check for rotation and movement input.
            if (_input.IsNewKeyPress(Keys.Up))
            {
                _rotate = _rotate + 1 > 3 ? 0 : _rotate + 1;
            }
            else if (_input.IsNewKeyPress(Keys.Right))
            {
                var windowRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                var figureRect = new Rectangle((int)_currentFigure.Position.X + _cellWidth, (int)_currentFigure.Position.Y,
                                                     (int)_currentFigure.Width, (int)_currentFigure.Height);
                if (windowRect.Contains(figureRect))
                    _move.X = _cellWidth;
            }
            else if (_input.IsNewKeyPress(Keys.Left))
            {
                var windowRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                var figureRect = new Rectangle((int)_currentFigure.Position.X - _cellWidth, (int)_currentFigure.Position.Y,
                                                     (int)_currentFigure.Width, (int)_currentFigure.Height);
                if (windowRect.Contains(figureRect))
                    _move.X = -_cellWidth;
            }
            else if (_input.IsKeyDown(Keys.Down))
            {
                if (GraphicsDevice.Viewport.Height > _currentFigure.Position.Y + _currentFigure.Height)
                {
                    _move.Y = _gravity;

                }
                else
                {
                    _currentFigure.IsSleeping = true;
                }
            }

            //Add gravity.
            if (GraphicsDevice.Viewport.Height > _currentFigure.Position.Y + _currentFigure.Height)
            {
                _move.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _currentFigure.IsSleeping = true;
            }

            _currentFigure.Position += _move;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var figure in _figures)
            {
                _spriteBatch.Draw(_square, figure.Position, Color.Blue);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
