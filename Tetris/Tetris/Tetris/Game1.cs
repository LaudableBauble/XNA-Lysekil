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
            _graphics.PreferredBackBufferWidth = 16 * 50;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            //Initialize the game.
            _input = new InputState();
            _figures = new List<Figure>();
            _cellWidth = 32;
            _gravity = 16;

            //Add the first figure.
            _currentFigure = Factory.RandomFigure();
            _currentFigure.Move(new Vector2(_cellWidth * 15, 0));
            _figures.Add(_currentFigure);

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
            //Read the keyboard and gamepad.
            _input.Update();

            // Allows the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) { this.Exit(); }
            if (_input.IsKeyDown(Keys.Escape)) { this.Exit(); }

            //Check if a new figure block should be launched.
            if (_currentFigure.IsSleeping)
            {
                _currentFigure = Factory.RandomFigure();
                _currentFigure.Move(new Vector2(_cellWidth * 15, 0));
                _figures.Add(_currentFigure);
            }

            //Check for rotation and movement input.
            if (_input.IsNewKeyPress(Keys.Up))
            {
                _rotate = _rotate + 1 > 3 ? 0 : _rotate + 1;
            }
            else if (_input.IsNewKeyPress(Keys.Right))
            {
                var proj = new Figure(_currentFigure) { Right = _currentFigure.Right, Bottom = _currentFigure.Bottom };
                proj.Move(new Vector2(_cellWidth, 0));
                if (!_figures.Exists(fig => fig != _currentFigure && fig.Intersects(proj)))
                    _move.X = _cellWidth;
            }
            else if (_input.IsNewKeyPress(Keys.Left))
            {
                var proj = new Figure(_currentFigure) { Left = _currentFigure.Left, Bottom = _currentFigure.Bottom };
                proj.Move(new Vector2(-_cellWidth, 0));
                if (!_figures.Exists(fig => fig != _currentFigure && fig.Intersects(proj)))
                    _move.X = -_cellWidth;
            }
            else if (_input.IsKeyDown(Keys.Down))
            {
                var proj = new Figure(_currentFigure) { Left = _currentFigure.Left, Bottom = _currentFigure.Bottom };
                proj.Move(new Vector2(0, _gravity));
                if (!_figures.Exists(fig => fig != _currentFigure && fig.Intersects(proj)))
                    _move.Y = _gravity;
            }

            //Add gravity.
            var project = new Figure(_currentFigure) { Left = _currentFigure.Left, Bottom = _currentFigure.Bottom };
            project.Move(new Vector2(0, _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds));
            if (!_figures.Exists(fig => fig != _currentFigure && fig.Intersects(project)))
            {
                _move.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else { _currentFigure.IsSleeping = true; }

            //Update the position and reset the movement variable.
            _currentFigure.Move(_move);
            _move = Vector2.Zero;

            //Check for wall collisions.
            _currentFigure.Left = MathHelper.Max(_currentFigure.Left, 0);
            _currentFigure.Right = MathHelper.Min(_currentFigure.Right, GraphicsDevice.Viewport.Width);

            //Check for floor collision.
            if (_currentFigure.Bottom >= GraphicsDevice.Viewport.Height)
            {
                _currentFigure.IsSleeping = true;
                _currentFigure.Bottom = GraphicsDevice.Viewport.Height;
            }

            //Call the base method.
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Clear the screen.
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw all figures.
            _spriteBatch.Begin();
            _figures.ForEach(figure => figure.Draw(_spriteBatch, _square));
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool ProjectIntersection(
    }
}
