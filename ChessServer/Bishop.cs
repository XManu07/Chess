using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Bishop:Piece
    {
        public Bishop(Colors color, Point position) :
            base()
        {
            SetPieceName(PieceNames.bishop);
            SetPieceColor(color);
            SetPosition(position);
        }
        public override bool ValidDestination (Point destination)
        {
            if (Math.Abs(destination.X - this.GetPiecePosition().X) == Math.Abs(this.GetPiecePosition().Y - destination.Y))
                return true;
            return false;
        }
        public override bool PieceToDestinationIsEmpty(Point destination)
        {
            if (GetPiecePosition().X - destination.X < 0)
            {
                int j = destination.Y;
                for (int i = destination.X - 1; i > GetPiecePosition().X; i--)
                {
                    if (GetPiecePosition().Y - destination.Y > 0)
                        j++;
                    else j--;
                    if (!pieceMatrix.MSquareIsEmpty(i, j))
                        return false;
                }
            }
            if (GetPiecePosition().X - destination.X > 0)
            {
                int j = destination.Y;
                for (int i = destination.X + 1; i < GetPiecePosition().X; i++)
                {
                    if (GetPiecePosition().Y - destination.Y > 0)
                        j++;
                    else j--;
                    if (!pieceMatrix.MSquareIsEmpty(i, j))
                        return false;
                }
            }
            return true;
        }
        internal override bool ValidMove(Point destination)
        {
            
            if(ValidDestination(destination))
            {
                if (PieceToDestinationIsEmpty(destination)&&pieceMatrix.MSquareIsEmpty(destination))
                    return true;
                if(PieceToDestinationIsEmpty(destination))
                {
                    if(pieceMatrix.MSquareIsOppositePiece(destination, GetPieceColor())&&!pieceMatrix.MSquareIsOppositeKing(destination,GetPieceColor())) 
                        return true;
                }
            }
            return false;
        }

    }
}
