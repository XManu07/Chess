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
        Player Manu = new Player("Manu", PieceColors.black);
        
        public void InitPieces()
        {
            foreach (var item in Manu.pieces)
            {
                item.pieceImage.Click += PictureBox_Click;
                chessBoard.GetControlFromPosition(item.position.y, item.position.x).Controls.Add(item.pieceImage);
                
            }
        }
        public void InitBoardColors_Pieces()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    square.Click += Panel_Click;
                    chessBoard.Controls.Add(square, column, row);
                }
            }

        }

        public FChessGame()
        {
            InitializeComponent();
            InitBoardColors_Pieces();
            InitPieces();

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

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedSquare = sender as Panel;
            if (clickedSquare != null)
            {
                Console.WriteLine("You clicked on a square at position: ");
                Console.WriteLine(chessBoard.GetPositionFromControl(clickedSquare));
                if (pictureToBeMoved != null)
                {
                    Controls.Remove(pictureToBeMoved);
                    pictureToBeMoved.Location = new Point(clickedSquare.Location.X, clickedSquare.Location.Y);
                    clickedSquare.Controls.Add(pictureToBeMoved);
                }
            }
        }
        public void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPicture = sender as PictureBox;
            Panel parent = clickedPicture.Parent as Panel;
            if (clickedPicture != null)
            {
                Console.WriteLine("You clicked on an image at position: ");
                Console.WriteLine(chessBoard.GetCellPosition(parent));
                pictureToBeMoved = clickedPicture;

            }
        }

    }
}
