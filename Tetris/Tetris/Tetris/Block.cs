using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Block
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Block()
        {
            Width = 32;
            Height = 32;
        }

        public bool Intersects(Block block)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height).Intersects(
                new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)block.Width, (int)block.Height));
        }
    }
}
