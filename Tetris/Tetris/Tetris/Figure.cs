using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris
{
    public class Figure
    {
        protected bool[,] Grid;

        public int Rotate { get; set; }
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool IsSleeping { get; set; }
        public List<Block> Blocks { get; set; }

        public Figure()
        {
            Grid = new bool[4, 4];
            Width = 32f;
            Height = 32f;
            Position = Vector2.Zero;
            Blocks = new List<Block>();
        }

        public void AddBlock(Block block)
        {
            Blocks.Add(block);
        }
        public void Move(Vector2 amount)
        {
            //Move all blocks by the specified amount.
            Blocks.ForEach(item => item.Position += amount);
        }
        public bool Intersects(Block block)
        {
            return Blocks.Exists(item => item.Intersects(block));
        }
        public bool Intersects(Figure figure)
        {
            return Blocks.Exists(item => figure.Intersects(item));
        }

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