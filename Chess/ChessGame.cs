using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class FChessGame : Form

    {
        public void InitBoardColors() { 
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.Brown : Color.White;
                    chessBoard.Controls.Add(square, column, row);
                    Console.WriteLine(row + " " + column);
                }
            }
        }
        public FChessGame()
        {
            InitializeComponent();
            InitBoardColors();
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FChessGame_Load(object sender, EventArgs e)
        {

        }


    }
}
