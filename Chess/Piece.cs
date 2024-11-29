using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Chess
{
    public enum PieceNames { pawn, rook, knight, bishop, king, queen };
    internal abstract class Piece
    {
        private PieceNames pieceName;
        private Colors pieceColor;
        private Point piecePosition;
        private PictureBox pieceImage;
        

        #region Set,Get
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
        #endregion

        #region Set,Get PieceImage
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
        #endregion

        public override string ToString()
        {
            return "[Piece=" + pieceName + ",color=" + pieceColor + ",position="
                + piecePosition.X + "," + piecePosition.Y + "]";
        }
        internal abstract bool ValidMove(Point destination,int[,] allPieces);
        public bool SquareIsEmpty(Point destination, int[,] allPieces)
        {
            return (allPieces[destination.X, destination.Y] == 0) ? true : false;
        }
        public bool SquareIsEmpty(int x,int y, int[,] allPieces)
        {
            return allPieces[x, y] == 0?true: false;
        }
        public bool SquareIsOpositePiece(Point destination, int[,] allPieces)
        {
            if(pieceColor==Colors.black)
                return (allPieces[destination.X,destination.Y]==2) ? true : false;
            if(pieceColor==Colors.white)
                return (allPieces[destination.X,destination.Y] == 1) ? true : false;
            return false;
        }
        public bool SquareIsOpositeKing(Point destination, int[,] allPieces)
        {
            if (SquareIsOpositePiece(destination, allPieces))
                if (SquareIsKing(destination, allPieces))
                    return true;
            return false;
        }
        public bool SquareIsKing(Point destination, int[,] allPieces)
        {
            if (allPieces[destination.X, destination.Y] == 2 || allPieces[destination.X, destination.Y] == 8)
                return true; 
            return false;
        }

    }
}
 