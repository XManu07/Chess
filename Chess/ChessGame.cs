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
        //Player Banu = new Player("Banu", PieceColors.black);
        
        public void InitBoardColors() { 
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Peru;

                    //init pawns
                    ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
                    if (row == 1)
                    {
                        Image pawnImage = (Image)rm.GetObject("pawn_brown");
                        PictureBox pawn= new PictureBox();
                        pawn.Image = pawnImage;
                        pawn.SizeMode = PictureBoxSizeMode.StretchImage;
                        pawn.Dock = DockStyle.Fill;
                        pawn.BackColor = Color.Transparent;
                        square.Controls.Add (pawn);
                    }

                    if (row == 6)
                    {
                        Image pawnImage = (Image)rm.GetObject("pawn_black");
                        PictureBox pawn = new PictureBox();
                        pawn.Image = pawnImage;
                        pawn.SizeMode = PictureBoxSizeMode.StretchImage;
                        pawn.Dock = DockStyle.Fill;
                        pawn.BackColor = Color.Transparent;
                        square.Controls.Add(pawn);
                    }

                    //init rooks
                    if(row==0 && (column==0 || column == 7))
                    {
                        Image rookImage = (Image)rm.GetObject("rook_brown");
                        PictureBox rook = new PictureBox();
                        rook.Image = rookImage;
                        rook.SizeMode = PictureBoxSizeMode.StretchImage;
                        rook.Dock = DockStyle.Fill;
                        rook.BackColor = Color.Transparent;
                        square.Controls.Add(rook);
                    }
                    if (row == 7 && (column == 0 || column == 7))
                    {
                        Image rookImage = (Image)rm.GetObject("rook_black");
                        PictureBox rook = new PictureBox();
                        rook.Image = rookImage;
                        rook.SizeMode = PictureBoxSizeMode.StretchImage;
                        rook.Dock = DockStyle.Fill;
                        rook.BackColor = Color.Transparent;
                        square.Controls.Add(rook);
                    }

                    //init knights
                    if (row == 0 && (column == 1 || column == 6))
                    {
                        Image KnightImage = (Image)rm.GetObject("knight_brown");
                        PictureBox Knight = new PictureBox();
                        Knight.Image = KnightImage;
                        Knight.SizeMode = PictureBoxSizeMode.StretchImage;
                        Knight.Dock = DockStyle.Fill;
                        Knight.BackColor = Color.Transparent;
                        square.Controls.Add(Knight);
                    }
                    if (row == 7 && (column == 1 || column == 6))
                    {
                        Image KnightImage = (Image)rm.GetObject("knight_black");
                        PictureBox Knight = new PictureBox();
                        Knight.Image = KnightImage;
                        Knight.SizeMode = PictureBoxSizeMode.StretchImage;
                        Knight.Dock = DockStyle.Fill;
                        Knight.BackColor = Color.Transparent;
                        square.Controls.Add(Knight);
                    }

                    //init bishops
                    if (row == 0 && (column == 2 || column == 5))
                    {
                        Image bishopImage = (Image)rm.GetObject("bishop_brown");
                        PictureBox bishop = new PictureBox();
                        bishop.Image = bishopImage;
                        bishop.SizeMode = PictureBoxSizeMode.StretchImage;
                        bishop.Dock = DockStyle.Fill;
                        bishop.BackColor = Color.Transparent;
                        square.Controls.Add(bishop);
                    }
                    if (row == 7 && (column == 2 || column == 5))
                    {
                        Image bishopImage = (Image)rm.GetObject("bishop_black");
                        PictureBox bishop = new PictureBox();
                        bishop.Image = bishopImage;
                        bishop.SizeMode = PictureBoxSizeMode.StretchImage;
                        bishop.Dock = DockStyle.Fill;
                        bishop.BackColor = Color.Transparent;
                        square.Controls.Add(bishop);
                    }

                    //init knights
                    if (row == 0 && column == 4)
                    {
                        Image kingImage = (Image)rm.GetObject("king_brown");
                        PictureBox king = new PictureBox();
                        king.Image = kingImage;
                        king.SizeMode = PictureBoxSizeMode.StretchImage;
                        king.Dock = DockStyle.Fill;
                        king.BackColor = Color.Transparent;
                        square.Controls.Add(king);
                    }
                    if (row == 7 && column == 4)
                    {
                        Image kingImage = (Image)rm.GetObject("king_black");
                        PictureBox king = new PictureBox();
                        king.Image = kingImage;
                        king.SizeMode = PictureBoxSizeMode.StretchImage;
                        king.Dock = DockStyle.Fill;
                        king.BackColor = Color.Transparent;
                        square.Controls.Add(king);
                    }

                    //init queens
                    if (row == 0 && column == 3)
                    {
                        Image queenImage = (Image)rm.GetObject("queen_brown");
                        PictureBox queen = new PictureBox();
                        queen.Image = queenImage;
                        queen.SizeMode = PictureBoxSizeMode.StretchImage;
                        queen.Dock = DockStyle.Fill;
                        queen.BackColor = Color.Transparent;
                        square.Controls.Add(queen);
                    }
                    if (row == 7 && column == 3)
                    {
                        Image queenImage = (Image)rm.GetObject("queen_black");
                        PictureBox queen = new PictureBox();
                        queen.Image = queenImage;
                        queen.SizeMode = PictureBoxSizeMode.StretchImage;
                        queen.Dock = DockStyle.Fill;
                        queen.BackColor = Color.Transparent;
                        square.Controls.Add(queen);
                    }



                    chessBoard.Controls.Add(square, column, row);
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
            Application.Exit();
        }

        private void FChessGame_Load(object sender, EventArgs e)
        {

        }


    }
}
