using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris
{
    public class Figure
    {
        protected bool[,] Grid;

        public int Rotate { get; set; }
        public bool IsSleeping { get; set; }
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Figure()
        {
            Grid = new bool[4, 4];
            Blocks = new List<Block>();
        }

        /// <summary>
        /// Add a block to the figure.
        /// </summary>
        /// <param name="block">The block to add.</param>
        public void AddBlock(Block block)
        {
            Blocks.Add(block);
        }
        /// <summary>
        /// Move the figure by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move.</param>
        public void Move(Vector2 amount)
        {
            Blocks.ForEach(item => item.Position += amount);
        }
        /// <summary>
        /// Whether a block intersects with this figure.
        /// </summary>
        /// <param name="block">The block to check for intersection.</param>
        /// <returns>Whether the block intersected with this figure.</returns>
        public bool Intersects(Block block)
        {
            return Blocks.Exists(item => item.Intersects(block));
        }
        /// <summary>
        /// Whether a figure intersected with this figure.
        /// </summary>
        /// <param name="figure">The other figure.</param>
        /// <returns>Whether the figures intersected.</returns>
        public bool Intersects(Figure figure)
        {
            return Blocks.Exists(item => figure.Intersects(item));
        }

        /// <summary>
        /// The leftmost x-coordinate of the figure.
        /// </summary>
        public float Left
        {
            get
            {
                float left = Blocks[0].Position.X;
                Blocks.ForEach(item => left = item.Position.X < left ? item.Position.X : left);
                return left;
            }
            set
            {
                Block left = Blocks[0];
                Blocks.ForEach(item => left = item.Position.X < left.Position.X ? item : left);
                Move(new Vector2(value - left.Position.X, 0));
            }
        }
        /// <summary>
        /// The rightmost x-coordinate of the figure.
        /// </summary>
        public float Right
        {
            get
            {
                float right = Blocks[0].Position.X + Blocks[0].Width;
                Blocks.ForEach(item => right = item.Position.X + item.Width > right ? item.Position.X + item.Width : right);
                return right;
            }
            set
            {
                Block right = Blocks[0];
                Blocks.ForEach(item => right = item.Position.X + item.Width > right.Position.X + right.Width ? item : right);
                Move(new Vector2(value - (right.Position.X + right.Width), 0));
            }
        }
        /// <summary>
        /// The bottom of the figure, ie. the greatest y-coordinate.
        /// </summary>
        public float Bottom
        {
            get
            {
                float bottom = Blocks[0].Position.Y;
                Blocks.ForEach(item => bottom = item.Position.Y + item.Height > bottom ? item.Position.Y + item.Height : bottom);
                return bottom;
            }
            set
            {
                Block bottom = Blocks[0];
                Blocks.ForEach(item => bottom = item.Position.Y + item.Height > bottom.Position.Y + bottom.Height ? item : bottom);
                Move(new Vector2(0, value - (bottom.Position.Y + bottom.Height)));
            }
        }
    }

    class Square : Figure
    {
        public Square()
        {
            Grid = new bool[4, 4]
            {
                { false, false, false, false },
                { false, true, true, false },
                { false, true, true, false },
                { false, false, false, false }
            };
        }
    }

    class Straight : Figure
    {
        public Straight()
        {
            Grid = new bool[4, 4]
            {
                { true, false, false, false },
                { true, false, false, false },
                { true, false, false, false },
                { true, false, false, false }
            };
        }
    }

    class HookRight : Figure
    {
        public HookRight()
        {
            Grid = new bool[4, 4]
            {
                { false, true, false, false },
                { false, true, false, false },
                { false, true, false, false },
                { false, true, true,  false }
            };
        }
    }

    class HookLeft : Figure
    {
        public HookLeft()
        {
            Grid = new bool[4, 4]
            {
                { false, false, true, false },
                { false, false, true, false },
                { false, false, true, false },
                { false,  true, true,  false }
            };
        }
    }

    class TwixRight : Figure
    {
        public TwixRight()
        {
            Grid = new bool[4, 4]
            {
                { false, false, true, false },
                { false, false, true, false },
                { false, true, false, false },
                { false,  true, true,  false }
            };
        }
    }

    class Twixleft : Figure
    {
        public Twixleft()
        {
            Grid = new bool[4, 4]
            {
                { false,  true, false, false },
                { false,  true, false, false },
                { false, false,  true, false },
                { false,  true,  true, false }
            };
        }
    }

    class Arrow : Figure
    {
        public Arrow()
        {
            Grid = new bool[4, 4]
            {
                { true,  true,  true,  false },
                { false, true,  false, false },
                { false, false, false, false },
                { false, false, false, false }
            };
        }
    }
}