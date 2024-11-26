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
        PictureBox pictureToBeMoved=null;
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
        private void FChessGame_Resize(object sender, EventArgs e) {
            AdjustChessBoardSize();
        } 
        private void AdjustChessBoardSize() 
        { 
            int size = Math.Min(this.ClientSize.Width, this.ClientSize.Height); 
            chessBoard.Size = new Size(size, size);
            chessBoard.Location = new Point( (this.ClientSize.Width - size) / 2, (this.ClientSize.Height - size) / 2 ); 
        }

    }
}
