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

            // Window size
            _graphics.PreferredBackBufferWidth = 16 * 20;
            _graphics.PreferredBackBufferHeight = 16 * 40;
            _graphics.ApplyChanges();

            //Initialize the game.
            _input = new InputState();
            _blocks = new List<Block>();
            _gravity = 16;
            _gameOver = false;

            //Add the first figure.
            _currentFigure = Factory.RandomFigure();
            _currentFigure.Move(new Vector2(Factory.WIDTH * 15, 0));
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
            _square = Content.Load<Texture2D>("Square[2]");
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

            #region Debug
            //Select a figure.
            if (_input.IsNewLeftMouseClick())
            {
                Block debug = _blocks.Find(item => item.Contains(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
                _debugFigure = debug == null || debug.Parent == null ? _currentFigure : (Figure)debug.Parent;
            }
            //Pause the game, ie. stop the gravity.
            if (_input.IsNewKeyPress(Keys.P)) { _gravity = _gravity == 0 ? 16 : 0; }
            #endregion

            //Check if a new figure block should be launched.
            if (_currentFigure.IsSleeping && !_gameOver)
            {
                //Check for game over.
                if (_blocks.Exists(b => !_currentFigure.Blocks.Contains(b) &&
                    b.Contains(new Vector2(Math.Min(Factory.WIDTH * 15 + 1, GraphicsDevice.Viewport.Width - 1), 1))))
                {
                    _gameOver = true;
                }
                else
                {
                    _currentFigure = Factory.RandomFigure();
                    _debugFigure = _currentFigure;
                    _currentFigure.Move(new Vector2(Factory.WIDTH * 15, 0));
                    _blocks.AddRange(_currentFigure.Blocks);
                }
            }

            #region Input
            //Check for rotation and movement input.
            if (!_currentFigure.IsSleeping)
            {
                if (_input.IsNewKeyPress(Keys.Up))
                {
                    if (IsRotationAllowed()) { _currentFigure.Rotate(); }
                }
                else if (_input.IsNewKeyPress(Keys.Right))
                {
                    Vector2 move = new Vector2(Factory.WIDTH, 0);
                    if (IsMoveAllowed(_currentFigure, move, out move, true)) { _currentFigure.Move(move); }
                }
                else if (_input.IsNewKeyPress(Keys.Left))
                {
                    Vector2 move = new Vector2(-Factory.WIDTH, 0);
                    if (IsMoveAllowed(_currentFigure, move, out move, true)) { _currentFigure.Move(move); }
                }
                else if (_input.IsKeyDown(Keys.Down))
                {
                    Vector2 move = new Vector2(0, _gravity);
                    if (IsMoveAllowed(_currentFigure, move, out move, false)) { _currentFigure.Move(move); }
                }
            }
            #endregion

            //Add gravity to all blocks not sleeping and not part of the current figure.
            Vector2 m = new Vector2(0, 2 * _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            foreach (Block block in _blocks.FindAll(item => !item.IsSleeping && !_currentFigure.Blocks.Contains(item)))
            {
                //Figure out the gravity movement for the block.
                if (IsMoveAllowed(block, m, out m, false)) { block.Position += m; }
                else { block.IsSleeping = true; }
            }

            //Add gravity to the current figure if it is not sleeping.
            m = new Vector2(0, _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (IsMoveAllowed(_currentFigure, m, out m, false)) { _currentFigure.Move(m); }
            else { _currentFigure.IsSleeping = true; }

            //Check for floor collision for all blocks not sleeping and not part of the current figure.
            foreach (Block block in _blocks.FindAll(item => !item.IsSleeping && !_currentFigure.Blocks.Contains(item)))
            {
                if (block.Position.Y + block.Height >= GraphicsDevice.Viewport.Height)
                {
                    block.IsSleeping = true;
                    block.Position = new Vector2(block.Position.X, GraphicsDevice.Viewport.Height - block.Height);
                }
            }

            //Check for wall and floor collisions for the current figure.
            _currentFigure.Left = MathHelper.Max(_currentFigure.Left, 0);
            _currentFigure.Right = MathHelper.Min(_currentFigure.Right, GraphicsDevice.Viewport.Width);
            if (!_currentFigure.IsSleeping)
            {
                if (_currentFigure.Bottom >= GraphicsDevice.Viewport.Height)
                {
                    _currentFigure.IsSleeping = true;
                    _currentFigure.Bottom = GraphicsDevice.Viewport.Height;
                }
            }

            //All blocks that have a sleeping sibling should be put to sleep themselves.
            List<Block> ls = _blocks.FindAll(block => !block.IsSleeping && block.Parent.IsSleeping);
            if (ls.Count > 0)
            {
                ls.ForEach(block => block.IsSleeping = true);
            }

            //Check that all blocks is in one of the _cellSize-columns and, if sleeping, also is in one of the _cellSize-rows.
            foreach (Block block in _blocks)
            {
                if ((block.Position.X % Factory.WIDTH).CompareTo(0) != 0)
                {
                    block.Position = new Vector2((float)(Factory.WIDTH * (int)Math.Round(block.Position.X / Factory.WIDTH)), block.Position.Y);
                }
                if ((block.Position.Y % Factory.HEIGHT).CompareTo(0) != 0 && block.IsSleeping)
                {
                    block.Position = new Vector2(block.Position.X, (float)(Factory.HEIGHT * (int)Math.Round(block.Position.Y / Factory.HEIGHT)));
                }
            }
            //Check that currentFigure is in one of the _cellSize-rows.
            if ((_currentFigure.Bottom % Factory.HEIGHT).CompareTo(0) != 0 && _currentFigure.IsSleeping)
            {
                _currentFigure.Bottom = (float)(Factory.HEIGHT * (int)Math.Round(_currentFigure.Bottom / Factory.HEIGHT));
            }

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
            _spriteBatch.DrawString(_font, "Offset: " + _debugFigure.Left % Factory.WIDTH, new Vector2(10, 105), Color.Black);
            _spriteBatch.DrawString(_font, "IsSleeping: " + _debugFigure.IsSleeping, new Vector2(10, 120), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// See if a move is allowed by a figure.
        /// </summary>
        /// <param name="figure">The figure to move.</param>
        /// <param name="move">The desired move amount.</param>
        /// <param name="assist">The move assist.</param>
        /// <param name="allowNegativeY">Whether to allow the figure to find a position above the current one.</param>
        /// <returns>Whether the move is valid.</returns>
        private bool IsMoveAllowed(IMovable entity, Vector2 move, out Vector2 assist, bool allowNegativeY)
        {
            //Set some startup variables.
            bool valid = false;
            assist = move;

            //Try to find an acceptable position by granting the figure some leeway.
            for (int x = 0; x <= Factory.WIDTH / 2; x++)
            {
                for (int y = 0; y <= Factory.HEIGHT / 2; y++)
                {
                    //Do four tests; (x, y), (x, -y), (-x, y), (-x, -y).
                    for (int n = 0; n < 4; n++)
                    {
                        //Decide upon the x, y configuration.
                        Vector2 config = Vector2.Zero;
                        switch (n)
                        {
                            case 0: { config = new Vector2(x, y); break; }
                            case 1: { config = allowNegativeY ? new Vector2(x, -y) : new Vector2(x, y); break; }
                            case 2: { config = new Vector2(-x, y); break; }
                            case 3: { config = allowNegativeY ? new Vector2(-x, -y) : new Vector2(-x, y); break; }
                        }

                        //Project the current figure to the new position and see whether the move was valid.
                        entity.Move(move + config);
                        valid = !_blocks.Exists(block => entity.IsIntersecting(block));
                        entity.Move(-(move + config));

                        //If the move was valid, stop here.
                        if (valid) { assist = move + config; return valid; }
                    }
                }
            }

            //Return the result (Hint: fail).
            return valid;
        }
        /// <summary>
        /// See if a rotation is allowed by a figure.
        /// </summary>
        /// <returns>Whether the rotation is valid.</returns>
        private bool IsRotationAllowed()
        {
            //Project the current figure to the new position.
            var proj = new Figure(_currentFigure) { Left = _currentFigure.Left, Bottom = _currentFigure.Bottom };
            proj.Rotate();

            //Return whether the movement is valid.
            return !_blocks.Exists(block => !_currentFigure.Blocks.Contains(block) && proj.Intersects(block));
        }
        /// <summary>
        /// Remove any completed rows.
        /// </summary>
        private void RemoveCompletedRows()
        {
            //The number of rows and columns.
            int rowCount = GraphicsDevice.Viewport.Height / (int)Factory.HEIGHT;
            int colCount = GraphicsDevice.Viewport.Width / (int)Factory.WIDTH;

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
                    Vector2 pos = new Vector2(Factory.WIDTH * (colCount - x) - (Factory.WIDTH / 2),
                        Factory.HEIGHT * (rowCount - y) - (Factory.HEIGHT / 2));
                    _blocks.ForEach(item => { if (item.Contains(pos)) { row.Add(item); } });
                }

                //If the row is complete, save it to the main list.
                //TODO: Note that multiple blocks on the same position will break this check.
                if (row.Count == colCount) { rows.Add(row); }
            }

            //Remove all completed rows, wake all blocks up and sort them by position.
            rows.ForEach(list => list.ForEach(block => _blocks.Remove(block)));
            if (rows.Count > 0) { _blocks.ForEach(block => block.IsSleeping = false); }
            _blocks.Sort((x, y) => Comparer<float>.Default.Compare(y.Position.Y, x.Position.Y));
        }
    }
}