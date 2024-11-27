using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chess
{
    public enum Colors { white, black };
    internal class GameLogic
    {
        private TableLayoutPanel chessBoard;
        Player player1 = new Player(Colors.black);
        Player player2 = new Player(Colors.white);


        PictureBox selectedPieceImage;
        Piece pieceFromImage;
        Panel panelOfPieceImage;
        Control destinationSquare;
        public GameLogic(TableLayoutPanel game)
        {
            this.chessBoard = game;
            InitBoardBackground();
            InitPieces();
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
                        pieceFromImage.ValidMove(player1.getPointFromDestination(destinationSquare, chessBoard)))
                    {
                        panelOfPieceImage.Controls.Remove(selectedPieceImage);
                        destinationSquare.Controls.Add(selectedPieceImage);
                        Point destinationSquarePos = new Point();
                        destinationSquarePos.X = chessBoard.GetCellPosition(destinationSquare).Row;
                        destinationSquarePos.Y = chessBoard.GetCellPosition(destinationSquare).Column;
                        pieceFromImage.SetPosition(destinationSquarePos);
                    }
                }
            }
        }

        public bool Mate()
        {
            return true;
        }
        public void StartGame()
        {
            while (!Mate())
            {

            }
        }
    }
}