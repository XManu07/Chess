using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using static Chess.FChessGame;

namespace Chess
{
    internal class Board
    {
        TableLayoutPanel chessBoard;

        Point oldPiecePosition;
        Point newPiecePosition;

        PictureBox selectedImage;
        Panel destination;

        public StreamWriter writer;
        public StreamReader reader;

        public Colors playerColor;
        public Board(TableLayoutPanel chessBoard,Colors playerCol)
        {
            this.chessBoard = chessBoard;
            SetPlayerColor(playerCol);
            InitBoardBackground();
            InitPieceImages();
        }
        public void SetPlayerColor(Colors playerColor)
        {
            this.playerColor = playerColor;
        }

        #region Init Board
        public void InitBoardBackground()
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
        public void InitPieceImages()
        {
            InitWhitePieces();
            InitBlackPieces();
        }
        public void SetImageAtPos(string nameOfImage, int x, int y,Colors pieceColor)
        {
            ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
            Image PieceImage = (Image)rm.GetObject(nameOfImage);
            PictureBox pieceImage = new PictureBox();
            pieceImage.Image = PieceImage;
            pieceImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pieceImage.Dock = DockStyle.Fill;
            pieceImage.BackColor = Color.Transparent;
            pieceImage.Click += PictureBox_Click;
            if (playerColor == pieceColor)
                pieceImage.Enabled = true;
            else pieceImage.Enabled = false;

            chessBoard.GetControlFromPosition(y, x).Controls.Add(pieceImage);
        }
        public void InitWhitePieces()
        {
            SetImageAtPos("rook_white", 0, 0,Colors.white);
            SetImageAtPos("knight_white", 0, 1,Colors.white);
            SetImageAtPos("bishop_white", 0, 2, Colors.white);
            SetImageAtPos("queen_white", 0, 3, Colors.white);
            SetImageAtPos("king_white", 0, 4, Colors.white);
            SetImageAtPos("bishop_white", 0, 5, Colors.white);
            SetImageAtPos("knight_white", 0, 6, Colors.white);
            SetImageAtPos("rook_white", 0, 7, Colors.white);
            for (int i = 0; i < 8; i++)
            {
                SetImageAtPos("pawn_white", 1, i, Colors.white);
            }
        }
        private void InitBlackPieces()
        {
            SetImageAtPos("rook_black", 7, 0,Colors.black);
            SetImageAtPos("knight_black", 7, 1, Colors.black);
            SetImageAtPos("bishop_black", 7, 2, Colors.black);
            SetImageAtPos("queen_black", 7, 3, Colors.black);
            SetImageAtPos("king_black", 7, 4, Colors.black);
            SetImageAtPos("bishop_black", 7, 5, Colors.black);
            SetImageAtPos("knight_black", 7, 6, Colors.black);
            SetImageAtPos("rook_black", 7, 7, Colors.black);
            for (int i = 0; i < 8; i++)
            {
                SetImageAtPos("pawn_black", 6, i, Colors.black);
            }

        }
        #endregion
        public void PictureBox_Click(object sender,EventArgs e)
        {
            selectedImage = sender as PictureBox;
            Panel imageParent=selectedImage.Parent as Panel;
            oldPiecePosition.X = chessBoard.GetCellPosition(imageParent).Row;
            oldPiecePosition.Y = chessBoard.GetCellPosition(imageParent).Column;
            Console.WriteLine("Click on PictureBox at pos " + chessBoard.GetCellPosition(imageParent));
        }
        public void Panel_Click(object sender,EventArgs e)
        {
            destination = sender as Panel;
            newPiecePosition.X = chessBoard.GetCellPosition(destination).Row;
            newPiecePosition.Y = chessBoard.GetCellPosition(destination).Column;
            Console.WriteLine("Click on Panel at pos " + chessBoard.GetCellPosition(destination));
            
            if(oldPiecePosition!=default)
            {
                writer.WriteLine(oldPiecePosition.X.ToString()+oldPiecePosition.Y.ToString()+
                    newPiecePosition.X.ToString()+newPiecePosition.Y.ToString());
            }
            oldPiecePosition = default;
        }
        public void UpdatePieceImage()
        {
            if (PanelHasImage(destination))
            {
                destination.Controls.RemoveAt(0);
            }
            destination.Controls.Add(selectedImage);
        }
        private bool PanelHasImage(Panel destination)
        {
            if (destination.HasChildren == true)
            {
                return true;
            }
            return false;
        }
        public void UpdateOponnentPieceImage(Point O_OldPiecePos, Point O_NewPiecePos)
        {
            Panel parent =(Panel)chessBoard.GetControlFromPosition(O_OldPiecePos.Y,O_OldPiecePos.X);
            PictureBox OImage = (PictureBox)parent.Controls[0];
            Console.WriteLine("parent "+chessBoard.GetPositionFromControl(parent).Row+" "+ chessBoard.GetPositionFromControl(parent).Column);
            parent.Controls.Remove(OImage);

            Panel ODestination= (Panel)chessBoard.GetControlFromPosition(O_NewPiecePos.Y, O_NewPiecePos.X);
            Console.WriteLine("dest "+chessBoard.GetPositionFromControl(ODestination).Row + " " + chessBoard.GetPositionFromControl(ODestination).Column);
            ODestination.Controls.Add(OImage);
            OImage.Dock=DockStyle.Fill;
        }
    }
}
