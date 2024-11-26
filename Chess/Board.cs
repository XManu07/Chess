using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Board
    {
        private TableLayoutPanel chessBoard;
        
        public Board(TableLayoutPanel chessBoard)
        {
            InitBoardBackground();
        }
        public void InitBoardBackground()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    chessBoard.Controls.Add(square, column, row);
                }
            }
        }
    }
}
