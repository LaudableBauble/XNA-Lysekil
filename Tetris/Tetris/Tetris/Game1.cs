using System;
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
        private Texture2D _background;
        private SpriteFont _font;
        private List<Block> _blocks;
        private InputState _input;
        private Effect _tintEffect;
        private float _gravity;
        private Figure _currentFigure;
        private Figure _debugFigure;
        private bool _gameOver;

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

            // Window size.
            _graphics.PreferredBackBufferWidth = (int)Helper.WIDTH * 10;
            _graphics.PreferredBackBufferHeight = (int)Helper.HEIGHT * 18;
            _graphics.ApplyChanges();

            //Initialize the game.
            _input = new InputState();
            _blocks = new List<Block>();
            _gravity = Helper.GRAVITY;
            _gameOver = false;

            //Add the first figure.
            _currentFigure = Helper.RandomFigure();
            _currentFigure.Move(new Vector2(Helper.WIDTH * 15, 0));
            _blocks.AddRange(_currentFigure.Blocks);

            //Set the debug figure.
            _debugFigure = _currentFigure;

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
            _square = Content.Load<Texture2D>("Square[3]");
            _background = Content.Load<Texture2D>("Tetris_Background[1]");
            _tintEffect = Content.Load<Effect>("BlockTint");
            _font = Content.Load<SpriteFont>("Debug");
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
        /// Let the game handle user input.
        /// </summary>
        protected void HandleInput()
        {
            //Read the keyboard and gamepad.
            _input.Update();

            // Allows the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) { this.Exit(); }
            if (_input.IsKeyDown(Keys.Escape)) { this.Exit(); }

            //Let the current figure handle input.
            _currentFigure.HandleInput(_input);

            #region Debug
            //Select a figure.
            if (_input.IsNewLeftMouseClick())
            {
                Block debug = _blocks.Find(item => item.Contains(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
                _debugFigure = debug == null || debug.Parent == null ? _currentFigure : (Figure)debug.Parent;
            }
            //Pause the game, ie. stop the gravity.
            if (_input.IsNewKeyPress(Keys.P)) { _gravity = _gravity == 0 ? Helper.GRAVITY : 0; }
            #endregion
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Handle input.
            HandleInput();

            //Quit the cycle if game over.
            if (_gameOver) { return; }

            //Check if a new figure block should be launched.
            if (_currentFigure.IsSleeping)
            {
                //Check for game over.
                if (_blocks.Exists(b => b.Parent.IsSleeping && b.Position.Y <= 0))
                {
                    _gameOver = true;
                    return;
                }
                else
                {
                    _currentFigure = _debugFigure = Helper.RandomFigure();
                    _currentFigure.Move(new Vector2(Helper.WIDTH * 15, 0));
                    _blocks.AddRange(_currentFigure.Blocks);
                }
            }

            //Update the current figure.
            _currentFigure.Update(gameTime, _blocks, _gravity, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //Remove all completed rows.
            if (_currentFigure.IsSleeping) { RemoveCompletedRows(); }

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

            //Initialize the tint shader.
            _tintEffect.CurrentTechnique = _tintEffect.Techniques["ColorTint"];
            _tintEffect.CurrentTechnique.Passes[0].Apply();

            //Draw background.
            _spriteBatch.Begin();
            //_spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.End();

            //Draw all figures.
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, _tintEffect);
            _blocks.ForEach(block => block.Draw(_spriteBatch, _square, _tintEffect));
            _spriteBatch.End();

            //Draw some debug data.
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Pause with P and select a figure to debug with left mouse button.", new Vector2(10, 15), Color.Black);
            _spriteBatch.DrawString(_font, "----------", new Vector2(10, 30), Color.Black);
            _spriteBatch.DrawString(_font, "Left: " + _debugFigure.Left, new Vector2(10, 45), Color.Black);
            _spriteBatch.DrawString(_font, "Right: " + _debugFigure.Right, new Vector2(10, 60), Color.Black);
            _spriteBatch.DrawString(_font, "Top: " + _debugFigure.Top, new Vector2(10, 75), Color.Black);
            _spriteBatch.DrawString(_font, "Bottom: " + _debugFigure.Bottom, new Vector2(10, 90), Color.Black);
            _spriteBatch.DrawString(_font, "Offset: " + _debugFigure.Left % Helper.WIDTH, new Vector2(10, 105), Color.Black);
            _spriteBatch.DrawString(_font, "IsSleeping: " + _debugFigure.IsSleeping, new Vector2(10, 120), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Remove any completed rows.
        /// </summary>
        private void RemoveCompletedRows()
        {
            //The number of rows and columns.
            int rowCount = GraphicsDevice.Viewport.Height / (int)Helper.HEIGHT;
            int colCount = GraphicsDevice.Viewport.Width / (int)Helper.WIDTH;

            //The list to rule them all.
            List<List<Block>> rows = new List<List<Block>>();

            //Start from the bottom and work your way up.
            for (int y = 0; y < rowCount; y++)
            {
                //A list to hold the current row.
                List<Block> row = new List<Block>();

                for (int x = 0; x < colCount; x++)
                {
                    //Get all blocks on the given row.
                    Vector2 pos = new Vector2(Helper.WIDTH * (colCount - x) - (Helper.WIDTH / 2),
                        Helper.HEIGHT * (rowCount - y) - (Helper.HEIGHT / 2));
                    _blocks.ForEach(item => { if (item.Contains(pos)) { row.Add(item); } });
                }

                //If the row is complete, save it to the main list.
                //TODO: Note that multiple blocks on the same position will break this check.
                if (row.Count == colCount) { rows.Add(row); }
            }

            //If a complete row has been found, continue.
            if (rows.Count > 0)
            {
                //Remove all completed rows.
                rows.ForEach(list => list.ForEach(block => { _blocks.Remove(block); block.Parent.RemoveBlock(block); }));

                //Find the lowest removed row, ie. the highest y-coordinate, and the amount to move the blocks above.
                float y = rows[0][0].Position.Y;
                Vector2 amount = new Vector2(0, Helper.HEIGHT * rows.Count);

                //Find all relevant blocks and move them down.
                List<Block> bl = _blocks.FindAll(block => block.Position.Y < y);
                bl.ForEach(block => block.Position += amount);
            }
        }
    }
}