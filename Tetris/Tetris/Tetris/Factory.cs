using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public static class Factory
    {
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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 64) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 96) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 64) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 64) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 32) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(64, 0) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(64, 32) });

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
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(0, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(64, 0) });
            figure.AddBlock(new Block() { Width = 32, Height = 32, Position = new Vector2(32, 32) });

            //Return the figure.
            return figure;
        }
    }
}
