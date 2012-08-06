using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    /// <summary>
    /// This interface defines game movement and collision for an entity.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Find out whether this IMovable entity currently intersects a specified other. Cannot intersect itself.
        /// </summary>
        /// <param name="entity">The other IMovable entity.</param>
        /// <returns>Whether the two entities are currently intersecting.</returns>
        bool Intersects(IMovable entity);
        /// <summary>
        /// Find out whether this IMovable entity contains a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>Whether this IMovable entity contains a vector.</returns>
        bool Contains(Vector2 v);
        /// <summary>
        /// Move the entity a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move.</param>
        void Move(Vector2 amount);
        /// <summary>
        /// Define this IMovable entity's as a rectangle.
        /// </summary>
        /// <returns></returns>
        Rectangle ToRectangle();
        /// <summary>
        /// The IMovable entity's figure parent.
        /// </summary>
        Figure Parent { get; set; }
        /// <summary>
        /// If the IMovable entity is sleeping and not moving.
        /// </summary>
        bool IsSleeping { get; set; }
    }
}
