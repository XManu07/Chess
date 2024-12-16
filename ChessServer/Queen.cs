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
    internal class Queen:Piece
    {
        public Queen(Colors color, Point position) :
              base()
        {
            SetPieceName(PieceNames.queen);
            SetPieceColor(color);
            SetPosition(position);

        }
        public override bool ValidDestination(Point destination)
        {
            if ((this.GetPiecePosition().X == destination.X || 
                this.GetPiecePosition().Y == destination.Y) ||
                Math.Abs(destination.X - this.GetPiecePosition().X) == Math.Abs(this.GetPiecePosition().Y - destination.Y))
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
            else if (GetPiecePosition().Y == destination.Y)
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
            else if (GetPiecePosition().X - destination.X < 0)
            {
                int j = destination.Y;
                for (int i = destination.X - 1; i > GetPiecePosition().X; i--)
                {
                    if (GetPiecePosition().Y - destination.Y > 0)
                        j++;
                    else j--;
                    if (!matrix.MSquareIsEmpty(i, j))
                        return false;
                }
            }
            else if (GetPiecePosition().X - destination.X > 0)
            {
                int j = destination.Y;
                for (int i = destination.X + 1; i < GetPiecePosition().X; i++)
                {
                    if (GetPiecePosition().Y - destination.Y > 0)
                        j++;
                    else j--;
                    if (!matrix.MSquareIsEmpty(i, j))
                        return false;
                }
            }
            return true;
        }

        internal override bool ValidMove(Point destination)
        {
            if(ValidDestination(destination))
            {
                if (PieceToDestinationIsEmpty(destination ) && matrix.MSquareIsEmpty(destination))
                    return true;
                if(PieceToDestinationIsEmpty(destination))
                    if(matrix.MSquareIsOppositePiece(destination,GetPieceColor())&&!matrix.MSquareIsOppositeKing(destination,GetPieceColor()))
                        return true; 
            }
            return false;
        }

    }
}
