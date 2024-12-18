using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Rook : Piece
    {
        public Rook(Colors color, Point position) :
           base()
        {
            SetPieceName(PieceNames.rook);
            SetPieceColor(color);
            SetPosition(position);
        }
        public override bool ValidDestination(Point destination)
        {
            if (this.GetPiecePosition().X == destination.X || this.GetPiecePosition().Y == destination.Y)
                return true;
            return false;
        }
        public override bool PieceToDestinationIsEmpty(Point destination)
        {
            if (GetPiecePosition().X == destination.X)
            {
                for (int i = GetPiecePosition().Y + 1; i < destination.Y; i++)
                {
                    if (matrix.MSquareIsEmpty(destination.X, i) == false)
                        return false;
                }
                for (int i = GetPiecePosition().Y - 1; i > destination.Y; i--)
                {
                    if (matrix.MSquareIsEmpty(destination.X, i) == false)
                        return false;
                }

            }
            if (GetPiecePosition().Y == destination.Y)
            {
                for (int i = GetPiecePosition().X + 1; i < destination.X; i++)
                {
                    if (matrix.MSquareIsEmpty(i, destination.Y) == false)
                        return false;
                }
                for (int i = GetPiecePosition().X - 1; i > destination.X; i--)
                {
                    if (matrix.MSquareIsEmpty(i, destination.Y) == false)
                        return false;
                }
            }
            return true;
        }


        internal override bool ValidMove(Point destination)
        {
            if (ValidDestination(destination))
            {
                if (PieceToDestinationIsEmpty(destination)&&matrix.MSquareIsEmpty(destination))
                    return true;
                if (PieceToDestinationIsEmpty(destination))
                    if (matrix.MSquareIsOppositePiece(destination, GetPieceColor())&&!matrix.MSquareIsOppositeKing(destination,GetPieceColor()))
                        return true;
            }
            return false;
        }
    }
}
