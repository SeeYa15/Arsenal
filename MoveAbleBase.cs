using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsenal
{
    /// <summary>
    /// This class is for handling movement-based action. General gravity, Speed. Maybe change class name
    /// </summary>
    /// 
    class MoveAbleBase
    {
        public Vector2 Movement;
        private Board _gameboard;

        public MoveAbleBase(Vector2 position, Texture2D texture, SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// Start position for player, enemy and related objects
        /// </summary>
        /// <returns></returns>
        public Vector2 FindStartPosition()
        {
            //_gameboard = _gameboard ?? _gameboard.GetGameBoard();
            return new Vector2(0);
        }
    }
}
