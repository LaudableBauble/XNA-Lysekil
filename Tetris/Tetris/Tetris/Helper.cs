using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public enum MovementAction
    {
        None, Left, Right, Down, Rotate
    }

    public static class Helper
    {
        public const float WIDTH = 32;
        public const float HEIGHT = 32;
        public const float GRAVITY = 16;

        /// <summary>
        /// Get a random figure.
        /// </summary>
        /// <returns>The randomized figure.</returns>
        public static Figure RandomFigure()
        {
            switch (new Random().Next(7))
            {
                case 0: { return Square(); }
                case 1: { return Straight(); }
                case 2: { return HookRight(); }
                case 3: { return HookLeft(); }
                case 4: { return TwixRight(); }
                case 5: { return TwixLeft(); }
                case 6: { return Arrow(); }
                default: { return Square(); }
            }
        }
        /// <summary>
        /// Create a square figure.
        /// </summary>
        /// <returns>The square figure.</returns>
        public static Figure Square()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Blue;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = Vector2.Zero });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT) });

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create a straight figure.
        /// </summary>
        /// <returns>The straight figure.</returns>
        public static Figure Straight()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Red;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = Vector2.Zero });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT * 2) });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT * 3) });

            figure.CenterBlock = figure.Blocks[1];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create a right hook figure.
        /// </summary>
        /// <returns>The right hook figure.</returns>
        public static Figure HookRight()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Yellow;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = Vector2.Zero });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT * 2) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT * 2) });

            //Set the center block.
            figure.CenterBlock = figure.Blocks[2];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create a left hook figure.
        /// </summary>
        /// <returns>The left hook figure.</returns>
        public static Figure HookLeft()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Yellow;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT * 2) });
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT * 2) });

            figure.CenterBlock = figure.Blocks[2];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create a right twix figure.
        /// </summary>
        /// <returns>The right twix figure.</returns>
        public static Figure TwixRight()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Green;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH * 2, 0) });

            figure.CenterBlock = figure.Blocks[2];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create a left twix figure.
        /// </summary>
        /// <returns>The left twix figure.</returns>
        public static Figure TwixLeft()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Green;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = Vector2.Zero });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH * 2, HEIGHT) });

            figure.CenterBlock = figure.Blocks[1];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// Create an arrow figure.
        /// </summary>
        /// <returns>The arrow figure.</returns>
        public static Figure Arrow()
        {
            //Create the figure.
            Figure figure = new Figure();

            //Set some attributes.
            figure.Color = Color.Purple;

            //Add and create the blocks.
            figure.AddBlock(new Block() { Position = new Vector2(0, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH * 2, 0) });
            figure.AddBlock(new Block() { Position = new Vector2(WIDTH, HEIGHT) });

            figure.CenterBlock = figure.Blocks[1];

            //Return the figure.
            return figure;
        }
        /// <summary>
        /// See if a move is allowed by a figure.
        /// </summary>
        /// <param name="figure">The figure to move.</param>
        /// <param name="move">The desired move amount.</param>
        /// <param name="assist">The move assist.</param>
        /// <param name="allowNegativeY">Whether to allow the figure to find a position above the current one.</param>
        /// <returns>Whether the move is valid.</returns>
        public static bool IsMoveAllowed(Figure figure, Vector2 move, List<Block> blocks, out Vector2 assist, bool allowNegativeY)
        {
            //Set some startup variables.
            bool valid = false;
            assist = move;

            //Try to find an acceptable position by granting the figure some leeway.
            for (int x = 0; x <= WIDTH / 2; x++)
            {
                for (int y = 0; y <= HEIGHT / 2; y++)
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
                        figure.Move(move + config);
                        valid = !blocks.Exists(block => figure.Intersects(block));
                        figure.Move(-(move + config));

                        //If the move was valid, stop here.
                        if (valid) { assist = move + config; return valid; }
                    }
                }
            }

            //Return the result (Hint: fail).
            return valid;
        }
        /// <summary>
        /// See if a move is allowed by a figure.
        /// </summary>
        /// <param name="figure">The figure to move.</param>
        /// <param name="move">The desired move amount.</param>
        /// <returns>Whether the move is valid.</returns>
        public static bool IsMoveAllowed(Figure figure, Vector2 move, List<Block> blocks)
        {
            //Set some startup variables.
            bool valid = false;

            //Project the current figure to the new position and see whether the move was valid.
            figure.Move(move);
            valid = !blocks.Exists(block => figure.Intersects(block));
            figure.Move(-move);

            //Return the result.
            return valid;
        }
        /// <summary>
        /// See if a rotation is allowed by a figure.
        /// </summary>
        /// <returns>Whether the rotation is valid.</returns>
        public static bool IsRotationAllowed(Figure figure, List<Block> blocks)
        {
            //Project the current figure to the new position.
            var proj = new Figure(figure) { Left = figure.Left, Bottom = figure.Bottom };
            proj.Rotate();

            //Return whether the movement is valid.
            return !blocks.Exists(block => !figure.Blocks.Contains(block) && proj.Intersects(block));
        }
        /// <summary>
        /// Define this block as a rectangle.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns>A rectangle matching the block's bounds.</returns>
        public static Rectangle ToRectangle(Block block)
        {
            return new Rectangle((int)Math.Round(block.Position.X), (int)Math.Round(block.Position.Y), (int)WIDTH, (int)HEIGHT);
        }
        /// <summary>
        /// Rotate a vector around a point.
        /// </summary>
        /// <param name="position">The vector to rotate.</param>
        /// <param name="origin">The origin of the rotation.</param>
        /// <param name="rotation">The amount of rotation in radians.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector2 RotateVector(Vector2 position, Vector2 origin, float rotation)
        {
            return new Vector2((float)(origin.X + (position.X - origin.X) * Math.Cos(rotation) - (position.Y - origin.Y) * Math.Sin(rotation)), (float)(origin.Y
            + (position.Y - origin.Y) * Math.Cos(rotation) + (position.X - origin.X) * Math.Sin(rotation)));
        }
    }
}
