using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public static class Helper
    {
        /// <summary>
        /// See if a move is allowed by a figure.
        /// </summary>
        /// <param name="figure">The figure to move.</param>
        /// <param name="move">The desired move amount.</param>
        /// <param name="assist">The move assist.</param>
        /// <param name="allowNegativeY">Whether to allow the figure to find a position above the current one.</param>
        /// <returns>Whether the move is valid.</returns>
        public static bool IsMoveAllowed(IMovable entity, Vector2 move, List<Block> _blocks, out Vector2 assist, bool allowNegativeY)
        {
            //Set some startup variables.
            bool valid = false;
            assist = move;

            //Try to find an acceptable position by granting the figure some leeway.
            for (int x = 0; x <= Factory.WIDTH / 2; x++)
            {
                for (int y = 0; y <= Factory.HEIGHT / 2; y++)
                {
                    //Do four tests; (x, y), (x, -y), (-x, y), (-x, -y).
                    for (int n = 0; n < 4; n++)
                    {
                        //Decide upon the x, y configuration.
                        Vector2 config = Vector2.Zero;
                        switch (n)
                        {
                            case 0: { config = new Vector2(x, y); break; }
                            case 1: { config = allowNegativeY ? new Vector2(x, -y) : new Vector2(x, y); break; }
                            case 2: { config = new Vector2(-x, y); break; }
                            case 3: { config = allowNegativeY ? new Vector2(-x, -y) : new Vector2(-x, y); break; }
                        }

                        //Project the current figure to the new position and see whether the move was valid.
                        entity.Move(move + config);
                        valid = !_blocks.Exists(block => entity.Intersects(block));
                        entity.Move(-(move + config));

                        //If the move was valid, stop here.
                        if (valid) { assist = move + config; return valid; }
                    }
                }
            }

            //Return the result (Hint: fail).
            return valid;
        }
        
        
        /// <summary>
        /// See if a rotation is allowed by a figure.
        /// </summary>
        /// <returns>Whether the rotation is valid.</returns>
        public static bool IsRotationAllowed(Figure _currentFigure, List<Block> _blocks)
        {
            //Project the current figure to the new position.
            var proj = new Figure(_currentFigure) { Left = _currentFigure.Left, Bottom = _currentFigure.Bottom };
            proj.Rotate();

            //Return whether the movement is valid.
            return !_blocks.Exists(block => !_currentFigure.Blocks.Contains(block) && proj.Intersects(block));
        }
    }
}
