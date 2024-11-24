using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Chess
{
    internal abstract class Piece
    {
        private PieceNames pieceName;
        private Colors pieceColor;
        private Point piecePosition;
        private PictureBox pieceImage;
        public enum PieceNames { pawn, rook, knight, bishop, king, queen };

        public void SetPieceName(PieceNames name)
        {
            pieceName = name;
        }
        public PieceNames GetPieceName()
        {
            return pieceName;
        }
        public void SetPieceColor(Colors color)
        {
            pieceColor = color;
        }
        public Colors GetPieceColor()
        {
            return pieceColor;
        }
        public void SetPosition(Point pos)
        {
            piecePosition.X = pos.X;
            piecePosition.Y = pos.Y;
        }
        public Point GetPiecePosition()
        {
            return piecePosition;
        }
        public void SetPieceImage()
        {
            string nameOfImage = pieceName.ToString()+"_"+pieceColor.ToString();

            ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
            Image PieceImage = (Image)rm.GetObject(nameOfImage);
            pieceImage = new PictureBox();
            pieceImage.Image = PieceImage;
            pieceImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pieceImage.Dock = DockStyle.Fill;
            pieceImage.BackColor = Color.Transparent;
        }
        public PictureBox GetPieceImage()
        {
            return pieceImage;
        }


        public override string ToString()
        {
            return "[Piece=" + pieceName + ",color=" + pieceColor + ",position="
                + piecePosition.X + "," + piecePosition.Y + "]";
        }
    }
}
 