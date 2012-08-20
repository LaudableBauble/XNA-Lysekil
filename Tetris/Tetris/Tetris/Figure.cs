using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    /// <summary>
    /// A figure is a shape built upon a multitude of blocks.
    /// </summary>
    public class Figure
    {
        #region Properties
        public bool IsSleeping { get; set; }
        public Color Color { get; set; }
        public List<Block> Blocks { get; set; }
        public Block CenterBlock { get; set; }

        /// <summary>
        /// The leftmost x-coordinate of the figure.
        /// </summary>
        public float Left
        {
            get
            {
                if (Blocks.Count == 0) { return 0; }
                float left = Blocks[0].Position.X;
                Blocks.ForEach(item => left = item.Position.X < left ? item.Position.X : left);
                return (float)Math.Round(left);
            }
            set
            {
                if (Blocks.Count == 0) { return; }
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
                if (Blocks.Count == 0) { return 0; }
                float right = Blocks[0].Position.X + Helper.WIDTH;
                Blocks.ForEach(item => right = item.Position.X + Helper.WIDTH > right ? item.Position.X + Helper.WIDTH : right);
                return (float)Math.Round(right);
            }
            set
            {
                if (Blocks.Count == 0) { return; }
                Block right = Blocks[0];
                Blocks.ForEach(item => right = item.Position.X + Helper.WIDTH > right.Position.X + Helper.WIDTH ? item : right);
                Move(new Vector2(value - (right.Position.X + Helper.WIDTH), 0));
            }
        }
        /// <summary>
        /// The bottom of the figure, ie. the greatest y-coordinate.
        /// </summary>
        public float Bottom
        {
            get
            {
                if (Blocks.Count == 0) { return 0; }
                float bottom = Blocks[0].Position.Y;
                Blocks.ForEach(item => bottom = item.Position.Y + Helper.HEIGHT > bottom ? item.Position.Y + Helper.HEIGHT : bottom);
                return (float)Math.Round(bottom);
            }
            set
            {
                if (Blocks.Count == 0) { return; }
                Block bottom = Blocks[0];
                Blocks.ForEach(item => bottom = item.Position.Y + Helper.HEIGHT > bottom.Position.Y + Helper.HEIGHT ? item : bottom);
                Move(new Vector2(0, value - (bottom.Position.Y + Helper.HEIGHT)));
            }
        }
        /// <summary>
        /// The top of the figure, ie. the smallest y-coordinate.
        /// </summary>
        public float Top
        {
            get
            {
                if (Blocks.Count == 0) { return 0; }
                float top = Blocks[0].Position.Y;
                Blocks.ForEach(item => top = item.Position.Y < top ? item.Position.Y : top);
                return (float)Math.Round(top);
            }
            set
            {
                if (Blocks.Count == 0) { return; }
                Block top = Blocks[0];
                Blocks.ForEach(item => top = item.Position.Y < top.Position.Y ? item : top);
                Move(new Vector2(0, value - top.Position.Y));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Figure()
        {
            Blocks = new List<Block>();
            Color = Color.Crimson;
        }
        /// <summary>
        /// Constructor for a figure.
        /// </summary>
        /// <param name="figure">The figure to clone.</param>
        public Figure(Figure figure)
        {
            Color = figure.Color;
            Blocks = new List<Block>();
            foreach (var block in figure.Blocks) { Blocks.Add(new Block(block)); }
            CenterBlock = figure.CenterBlock == null ? null : Blocks[figure.Blocks.IndexOf(figure.CenterBlock)];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draw the figure.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use.</param>
        /// <param name="texture">The texture to overlay a block with.</param>
        /// <param name="effect">The color tint effect to use.</param>
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Effect effect)
        {
            //Set the color tint.
            effect.Parameters["TintColor"].SetValue(Color.ToVector4());
            effect.CurrentTechnique.Passes[0].Apply();

            //Draw all blocks.
            Blocks.ForEach(block => block.Draw(spriteBatch, texture, effect));
        }

        /// <summary>
        /// Add a block to the figure.
        /// </summary>
        /// <param name="block">The block to add.</param>
        public void AddBlock(Block block)
        {
            Blocks.Add(block);
            block.Parent = this;
        }
        /// <summary>
        /// Remove a block from the figure.
        /// </summary>
        /// <param name="block">The block to remove.</param>
        public void RemoveBlock(Block block)
        {
            Blocks.Remove(block);
            block.Parent = null;
        }
        /// <summary>
        /// Get the blocks that contain a specific vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>The blocks that contain the vector.</returns>
        public List<Block> GetBlocks(Vector2 v)
        {
            return Blocks.FindAll(item => item.Contains(v));
        }
        /// <summary>
        /// Rotate the figure 90 degrees.
        /// </summary>
        public void Rotate()
        {
            //If the figure has no center block, stop here.
            if (CenterBlock == null) { return; }

            //Rotate each block around the center position.
            foreach (var block in Blocks)
            {
                if (block != CenterBlock)
                {
                    block.Position = Helper.RotateVector(block.Position, CenterBlock.Position, (float)Math.PI / 2);
                }
            }
        }
        public void Move(Vector2 amount)
        {
            Blocks.ForEach(item => item.Position += amount);
        }
        public bool Intersects(Block block)
        {
            return block.Parent != this && Blocks.Exists(item => block.Intersects(item));
        }
        public bool Contains(Vector2 v)
        {
            return Blocks.Exists(item => item.Contains(v));
        }
        #endregion
    }
}