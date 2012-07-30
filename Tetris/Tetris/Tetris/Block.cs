using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
    /// <summary>
    /// A block is a square piece capable of forming into more complex figures.
    /// </summary>
    public class Block
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = new Vector2((float)Math.Round(value.X), value.Y); }
        }
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
        /// <summary>
        /// Whether this block contains a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>Whether this block contains a vector.</returns>
        public bool Contains(Vector2 v)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height).Contains((int)v.X, (int)v.Y);
        }
    }
}
