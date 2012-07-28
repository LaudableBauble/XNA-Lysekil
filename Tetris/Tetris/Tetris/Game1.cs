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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _square;
        private Vector2 _position;
        private Vector2 _stop;
        private List<Figure> _figures;
        private InputState _input;
        private int _cellWidth;
        private float _gravity;

        private Vector2 _move;
        private int _rotate;

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
            _position = new Vector2(350, 0);
            _input = new InputState();
            _figures = new List<Figure>();
            _cellWidth = 16;
            _gravity = 8;

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

            //Read the keyboard and gamepad.
            _input.Update();

            //Check for rotation and movement input.
            if (_input.IsNewKeyPress(Keys.Up)) { _rotate = _rotate + 1 > 3 ? 0 : _rotate + 1; }
            else if (_input.IsNewKeyPress(Keys.Right)) { _move.X = _cellWidth; }
            else if (_input.IsNewKeyPress(Keys.Left)) { _move.X = -_cellWidth; }
            else if (_input.IsKeyDown(Keys.Down)) { _move.Y = _gravity; }

            //Add gravity.
            _move.Y += _gravity;

            // TODO: Add your update logic here
            if (RemoveRow())
            {
                //ToDo   
            }

            //Check buttons
            // - Check turn/rotate
            // - Check down

            if (ShouldMove())
            {
                Move(gameTime);
            }
            else
            {
                CreateNew();
            }

            base.Update(gameTime);
        }

        private void CreateNew()
        {
            throw new System.NotImplementedException();
        }

        private void Move(GameTime gameTime)
        {
            _position += new Vector2(0, (float)gameTime.ElapsedGameTime.TotalMilliseconds * .1f);
        }

        private bool ShouldMove()
        {
            throw new System.NotImplementedException();
        }

        private bool RemoveRow()
        {
            throw new System.NotImplementedException();
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

            _spriteBatch.Draw(_square, _position, Color.Blue);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
