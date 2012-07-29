﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tetris
{
    /// <summary>
    /// A block is a square piece capable of forming into more complex figures.
    /// </summary>
    public class Block
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Block()
        {
            Width = 32;
            Height = 32;
        }

        /// <summary>
        /// Whether two blocks intersect each other.
        /// </summary>
        /// <param name="block">The other block.</param>
        /// <returns>Whether they intersected each other.</returns>
        public bool Intersects(Block block)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height).Intersects(
                new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)block.Width, (int)block.Height));
        }
    }
}