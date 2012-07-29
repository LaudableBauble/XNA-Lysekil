using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris
{
    public class Figure
    {
        public int Rotate { get; set; }
        public bool IsSleeping { get; set; }
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Figure()
        {
            Blocks = new List<Block>();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="currentFigure"></param>
        public Figure(Figure currentFigure)
        {
            Blocks = new List<Block>();
            foreach (var block in currentFigure.Blocks)
            {
                Blocks.Add(new Block() {Height = block.Height, Position = Vector2.Zero, Width = block.Width});
            }      
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
            return Blocks.Exists(figure.Intersects);
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
}