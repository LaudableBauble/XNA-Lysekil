using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace Tetris.Test
{
    [TestFixture]
    public class GameLogicTest
    {
        IMovable entity;
        Vector2 move;
        List<Block> _blocks;
        Vector2 assist;
        bool allowNegativeY;


        [SetUp]
        public void Setup()
        {
            entity = new Block();
            move = Vector2.Zero;
            _blocks = new List<Block>();
            _blocks.Add(entity as Block);
            allowNegativeY = false;
        }

        [Test]
        public void IsMoveAllowedTest()
        {
            Helper.IsMoveAllowed(entity, move, _blocks, out assist, allowNegativeY);

            
        }

        [Test]
        public void IsRotationAllowedTest()
        {

        }
    }
}
