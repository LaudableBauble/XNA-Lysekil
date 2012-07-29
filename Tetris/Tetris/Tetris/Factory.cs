using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public static class Factory
    {
        public const float WIDTH = 32;
        public const float HEIGHT = 32;

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT * 2) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT * 3) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT * 2) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT * 2) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH * 2, 0) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = Vector2.Zero });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH * 2, HEIGHT) });

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
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(0, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH * 2, 0) });
            figure.AddBlock(new Block() { Width = WIDTH, Height = HEIGHT, Position = new Vector2(WIDTH, HEIGHT) });

            figure.CenterBlock = figure.Blocks[1];

            //Return the figure.
            return figure;
        }

       
    }
}
