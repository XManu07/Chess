using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class FChessGame : Form

    {
        //here I is necessary to  have only the graphics(frontend),
        //and send to the server the location of the moved Piece(before and after move)
        //server validate the move and if is valid will wait for the other player to move,
        //and if it s not the player should move another piece
        GameLogic game;

        public FChessGame()
        {
            InitializeComponent();
            game = new GameLogic(this.chessBoard);
            
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FChessGame_Load(object sender, EventArgs e)
        {

        }

        #region resize
        private void FChessGame_Resize(object sender, EventArgs e) {
            AdjustChessBoardSize();
        } 
        private void AdjustChessBoardSize() 
        { 
            int size = Math.Min(this.ClientSize.Width, this.ClientSize.Height); 
            chessBoard.Size = new Size(size, size);
            chessBoard.Location = new Point( (this.ClientSize.Width - size) / 2, (this.ClientSize.Height - size) / 2 ); 
        }
        #endregion
    }
}
