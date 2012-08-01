using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    /// <summary>
    /// A block is a square piece capable of forming into more complex figures.
    /// </summary>
    public class Block
    {
        public Figure Parent { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool IsSleeping { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Block()
        {
            Parent = null;
            Position = Vector2.Zero;
            Color = Color.Crimson;
            Width = 32;
            Height = 32;
            IsSleeping = false;
        }
        /// <summary>
        /// Cloning constructor.
        /// </summary>
        public Block(Block block)
        {
            Parent = block.Parent;
            Position = block.Position;
            Color = block.Color;
            Width = block.Width;
            Height = block.Height;
            IsSleeping = block.IsSleeping;
        }

        /// <summary>
        /// Draw the block.
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
            spriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero,
                new Vector2(Width / texture.Bounds.Width, Height / texture.Bounds.Height), SpriteEffects.None, 0);
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
