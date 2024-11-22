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
        Player Manu = new Player("Manu", PieceColors.white);

        public void InitPieceImage(Panel square,String nameOfImage)
        {
            ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
            Image pieceImage = (Image)rm.GetObject(nameOfImage);
            PictureBox piece =new PictureBox();
            piece.Click += PictureBox_Click;
            piece.Image = pieceImage;
            piece.SizeMode = PictureBoxSizeMode.StretchImage;
            piece.Dock = DockStyle.Fill;
            piece.BackColor = Color.Transparent;
            square.Controls.Add(piece);
        }
        public void InitBoardColors_Pieces() { 
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    square.Click += Panel_Click;

                    //init pawns
                    if (row == 1)
                    {
                        InitPieceImage(square, "pawn_brown");
                    }

                    if (row == 6)
                    {
                        InitPieceImage(square, "pawn_black");
                    }

                    //init rooks
                    if(row==0 && (column==0 || column == 7))
                    {
                        InitPieceImage(square, "rook_brown");
                    }
                    if (row == 7 && (column == 0 || column == 7))
                    {
                        InitPieceImage(square, "rook_black");
                    }

                    //init knights
                    if (row == 0 && (column == 1 || column == 6))
                    {
                        InitPieceImage(square, "knight_brown");
                    }
                    if (row == 7 && (column == 1 || column == 6))
                    {
                        InitPieceImage(square, "knight_black");
                    }

                    //init bishops
                    if (row == 0 && (column == 2 || column == 5))
                    {
                        InitPieceImage(square, "bishop_brown");
                    }
                    if (row == 7 && (column == 2 || column == 5))
                    {
                        InitPieceImage(square, "bishop_black");
                    }

                    //init king
                    if (row == 0 && column == 4)
                    {
                        InitPieceImage(square, "king_brown");
                    }
                    if (row == 7 && column == 4)
                    {
                        InitPieceImage(square, "king_black");
                    }

                    //init queens
                    if (row == 0 && column == 3)
                    {
                        InitPieceImage(square, "queen_brown");
                    }
                    if (row == 7 && column == 3)
                    {
                        InitPieceImage(square, "queen_black");
                    }

                    chessBoard.Controls.Add(square, column, row);
                }
            }
            
        }
        
        public FChessGame()
        {
            InitializeComponent();
            InitBoardColors_Pieces();

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
            } 
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPicture= sender as PictureBox;
            Panel parent = clickedPicture.Parent as Panel;
            if(clickedPicture != null)
            {
                Console.WriteLine("You clicked on an image at position: ");
                Console.WriteLine(chessBoard.GetCellPosition(parent));
            }
        }
    }
}
