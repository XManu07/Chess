using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chess
{
    public enum Colors { white=1, black=9 };
    public enum Pieces { blackKing=8,whiteKing=2}
    internal class GameLogic
    {
        private TableLayoutPanel chessBoard;
        Player player1 = new Player(Colors.black);
        Player player2 = new Player(Colors.white);

        int[,] allPieces=new int[8,8];

        PictureBox selectedPieceImage;
        Piece pieceFromImage;
        Panel panelOfPieceImage;
        Control destinationSquare;
        public GameLogic(TableLayoutPanel game)
        {
            this.chessBoard = game;
            InitBoardBackground();
            InitPieces();
            InitPieceMatrix(player1,player2);
            ShowMatrix(allPieces);
            StartGame();
        }
        #region InitBoard
        public void InitBoardBackground()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    square.Click += Control_Click;
                    chessBoard.Controls.Add(square, column, row);
                }
            }
        }

        public void InitPieces()
        {
            foreach (var item in player1.GetPieces())
            {
                item.GetPieceImage().Click += Control_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
            }
            foreach (var item in player2.GetPieces())
            {
                item.GetPieceImage().Click += Control_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
            }
        }
        #endregion
        /*
  //public void Panel_Click(object sender, EventArgs e)
        //{
        //    clickedSquare = sender as Control;
        //    if (clickedSquare != null)
        //    {
        //        Console.WriteLine("You clicked on a square at position: "+ 
        //            chessBoard.GetPositionFromControl(clickedSquare));
        //        if (selectedPieceImage != null  )
        //        {
        //            selectedPieceImage.Location = new Point(clickedSquare.Location.X, clickedSquare.Location.Y);
        //            clickedSquare.Controls.Add(selectedPieceImage);
        //        }
        //    }
        //}
        //public void PictureBox_Click(object sender, EventArgs e)
        //{
        //    PictureBox clickedPicture = sender as PictureBox;
        //    Panel parent = clickedPicture.Parent as Panel;
        //    if (clickedPicture != null)
        //    {
        //        Console.WriteLine("You clicked on an image at position: "+
        //            chessBoard.GetCellPosition(parent));
        //        selectedPieceImage = clickedPicture;
        //    }
        //}
 */
        public void Control_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(PictureBox))
            {
                selectedPieceImage = sender as PictureBox;
                panelOfPieceImage=selectedPieceImage.Parent as Panel;
                pieceFromImage = player1.GetPieceFromImage(selectedPieceImage, chessBoard);
                Console.WriteLine("clicked on picture box " + pieceFromImage.ToString());
            }
            else
            {
                destinationSquare = sender as Panel;
                Console.WriteLine("click on panel"+chessBoard.GetCellPosition(destinationSquare));
                
                if (pieceFromImage != null)
                {
                    if (selectedPieceImage != null &&
                        pieceFromImage.ValidMove(player1.getPointFromDestination(destinationSquare, chessBoard),allPieces))
                    {
                        panelOfPieceImage.Controls.Remove(selectedPieceImage);
                        destinationSquare.Controls.Add(selectedPieceImage);
                        Point destinationSquarePos = new Point();
                        destinationSquarePos.X = chessBoard.GetCellPosition(destinationSquare).Row;
                        destinationSquarePos.Y = chessBoard.GetCellPosition(destinationSquare).Column;
                        UpdateMatrix(pieceFromImage.GetPiecePosition());
                        pieceFromImage.SetPosition(destinationSquarePos);
                        InitPieceMatrix(player1, player2);
                        ShowMatrix(allPieces);
                    }
                }
            }
        }
        public void StartGame()
        {
        }
        public void UpdateMatrix(Point pos)
        {
            allPieces[pos.X, pos.Y] = 0;
            allPieces[5, 5] = 2;
        }
        public void InitPieceMatrix(Player p1, Player p2)
        {
            foreach(var piece in p1.GetPieces())
            {
                allPieces[piece.GetPiecePosition().X,piece.GetPiecePosition().Y] = (int)p1.GetPlayerColor();
                //init king
                if (piece.GetPieceName() == PieceNames.king)
                { 
                    if (p1.GetPlayerColor() == Colors.black)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.blackKing;
                    if (p1.GetPlayerColor() == Colors.white)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.whiteKing;
                }
            }

            foreach (var piece in p2.GetPieces())
            {
                allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)p2.GetPlayerColor();
                //init king 
                if (piece.GetPieceName() == PieceNames.king)
                   {
                    if (p2.GetPlayerColor() == Colors.black)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.blackKing;
                    if (p2.GetPlayerColor() == Colors.white)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.whiteKing;
                }
            }
        }
        public void ShowMatrix(int[,] pieces)
        {
            for (int line=0;line<8;line++)
            {
                for(int column = 0; column < 8; column++)
                {
                    Console.Write(allPieces[line, column] + " ");
                }
                Console.WriteLine();
            }
        }
        
    }
}