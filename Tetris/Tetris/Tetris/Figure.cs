using Microsoft.Xna.Framework;

namespace Tetris
{
    class Figure
    {
        protected bool[,] Grid;

        public int Rotate { get; set; }
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public bool IsSleeping { get; set; }

        public Figure()
        {
            Grid = new bool[4, 4];
            Width = 32f;
            Height = 32f;
            Position = Vector2.Zero;
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