using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class FHome : Form
    {
        FChessGame chessGame;
        public FHome()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            chessGame=new FChessGame();
            chessGame.ShowDialog();
        }

 
    }
}
