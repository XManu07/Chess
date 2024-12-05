using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Security.Policy;
using System.Windows.Forms;

namespace Chess
{
    public enum PieceNames { gol,pawn=1, rook, knight, bishop, king, queen };
    internal abstract class Piece
    {
        private PieceNames pieceName;
        private Colors pieceColor;
        private Point piecePosition;
        private PictureBox pieceImage;
        private List<Point> validMoves;
        public static BoardMatrix matrix;
        

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
        public void SetPosition(int x,int y)
        {
            piecePosition.X = x;
            piecePosition.Y = y;
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

        internal abstract bool ValidMove(Point destination);
        public virtual bool GenerateValidMoves(int[,] allPiecess)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j=0; j < 8; j++)
                {
                    if (allPiecess[i,j] == 0)
                    {

                    }
                }
            }
            return true;
        }

        public virtual bool KingPosIsValidMove(Point kingPos)
        {
            if (ValidDestination(kingPos))
            {
                if (PieceToDestinationIsEmpty(kingPos))
                    return true;              
            }
            return false;
        }
        public abstract bool ValidDestination(Point destination);
        public abstract bool PieceToDestinationIsEmpty(Point destination);

        public override string ToString()
        {
            return "[Piece=" + pieceName + ",color=" + pieceColor + ",position="
                + piecePosition.X + "," + piecePosition.Y + "]";
        }

    }
}
 