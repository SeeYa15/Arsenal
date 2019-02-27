using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsenal
{
    class Board
    {
        private Tile[,] GameBoard { get; set; }
        private SpriteBatch Spritebatch { get; set; }
        private Texture2D Texture { get; set; }
        private int Rows, Columns;
        private Board ThisBoard { get; set; }

        public Board(Texture2D texture, int rows, int columns, SpriteBatch spritebatch)
        {           
            Rows = rows;
            Columns = columns;
            Texture = texture;
            GameBoard = new Tile[columns,rows];
            Spritebatch = spritebatch;
            ThisBoard = this;
        }
        #region Functions
        public void Generateboard()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Vector2 tileposition = new Vector2(x * Texture.Width, y * Texture.Height);
                    GameBoard[x, y] = new Tile(tileposition ,Texture, Spritebatch, false);
                }
            }
        }

        public void GenerateRandomBoard()
        {
            for (int x = 0; x <= Columns-1; x++)
            {
                for (int y = 0; y <= Rows-1; y++)
                {
                    Vector2 tileposition = new Vector2(x * Texture.Width, y * Texture.Height);
                    if ((x == 0 && y >= 0) || (x >= 0 && y == 0) || (y == Rows - 1 && x >= 0) || (x == Columns-1 && y >= 0))
                    {
                        GameBoard[x, y] = new Tile(tileposition, Texture, Spritebatch, true );
                    } else GameBoard[x, y] = new Tile(tileposition, Texture, Spritebatch, false);
                }
            }
        }

        public void Draw()
        {
            foreach (Tile tile in GameBoard)
            {
                tile.Draw();
            }
        }
        #endregion
        #region Getters and Setters
        #endregion
    }
}
