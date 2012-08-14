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
        #region Properties
        public Figure Parent { get; set; }
        public Vector2 Position { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Block()
        {
            Parent = null;
            Position = Vector2.Zero;
        }
        /// <summary>
        /// Cloning constructor.
        /// </summary>
        public Block(Block block)
        {
            Parent = block.Parent;
            Position = block.Position;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draw the block.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use.</param>
        /// <param name="texture">The texture to overlay a block with.</param>
        /// <param name="effect">The color tint effect to use.</param>
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Effect effect)
        {
            //Set the color tint.
            effect.Parameters["TintColor"].SetValue(Parent.Color.ToVector4());
            effect.CurrentTechnique.Passes[0].Apply();

            //Draw all blocks.
            spriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero,
                new Vector2(Helper.WIDTH / texture.Bounds.Width, Helper.HEIGHT / texture.Bounds.Height), SpriteEffects.None, 0);
        }

        public bool Contains(Vector2 v)
        {
            return Helper.ToRectangle(this).Contains((int)v.X, (int)v.Y);
        }
        public bool Intersects(Block block)
        {
            return this != block && block.Parent != Parent && Helper.ToRectangle(this).Intersects(Helper.ToRectangle(block));
        }
        #endregion
    }
}
