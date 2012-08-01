﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Figure
    {
        public bool IsSleeping
        {
            get { return !Blocks.Exists(b => !b.IsSleeping); }
            set { Blocks.ForEach(b => b.IsSleeping = value); }
        }
        public bool IsEmpty { get; set; }
        private Color _color;
        public Color Color
        {
            get { return _color; }
            set { _color = value; if (Blocks != null) { Blocks.ForEach(b => b.Color = value); } }
        }
        public List<Block> Blocks { get; set; }
        public Block CenterBlock { get; set; }

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
            block.Color = Color;
            IsEmpty = false;
        }
        /// <summary>
        /// Remove a block from the figure.
        /// </summary>
        /// <param name="block">The block to remove.</param>
        public void RemoveBlock(Block block)
        {
            Blocks.Remove(block);
            block.Parent = null;
            IsEmpty = Blocks.Count > 0 ? false : true;
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
        /// Whether this figure contains a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>Whether this figure contains a vector.</returns>
        public bool Contains(Vector2 v)
        {
            return Blocks.Exists(item => item.Contains(v));
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
        /// Rotate a vector around a point.
        /// </summary>
        /// <param name="position">The vector to rotate.</param>
        /// <param name="origin">The origin of the rotation.</param>
        /// <param name="rotation">The amount of rotation in radians.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector2 RotateVector(Vector2 position, Vector2 origin, float rotation)
        {
            return new Vector2((float)(origin.X + (position.X - origin.X) * Math.Cos(rotation) - (position.Y - origin.Y) * Math.Sin(rotation)), (float)(origin.Y
            + (position.Y - origin.Y) * Math.Cos(rotation) + (position.X - origin.X) * Math.Sin(rotation)));
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
                    block.Position = RotateVector(block.Position, CenterBlock.Position, (float)Math.PI / 2);
                }
            }
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
                return (float)Math.Round(left);
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
                return (float)Math.Round(right);
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
                return (float)Math.Round(bottom);
            }
            set
            {
                Block bottom = Blocks[0];
                Blocks.ForEach(item => bottom = item.Position.Y + item.Height > bottom.Position.Y + bottom.Height ? item : bottom);
                Move(new Vector2(0, value - (bottom.Position.Y + bottom.Height)));
            }
        }
        /// <summary>
        /// The top of the figure, ie. the smallest y-coordinate.
        /// </summary>
        public float Top
        {
            get
            {
                float top = Blocks[0].Position.Y;
                Blocks.ForEach(item => top = item.Position.Y < top ? item.Position.Y : top);
                return (float)Math.Round(top);
            }
            set
            {
                Block top = Blocks[0];
                Blocks.ForEach(item => top = item.Position.Y < top.Position.Y ? item : top);
                Move(new Vector2(0, value - top.Position.Y));
            }
        }
    }
}