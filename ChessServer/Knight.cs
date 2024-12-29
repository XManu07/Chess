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
    internal class Knight:Piece
    {
        public Knight(Colors color, Point position) :
           base()
        {
            SetPieceName(PieceNames.knight);
            SetPieceColor(color);
            SetPosition(position);
        }

        public override bool ValidDestination(Point destination)
        {
            if ((GetPiecePosition().X == destination.X + 2 && GetPiecePosition().Y == destination.Y + 1) ||
                (GetPiecePosition().X == destination.X + 2 && GetPiecePosition().Y == destination.Y - 1) ||
                (GetPiecePosition().X == destination.X + 1 && GetPiecePosition().Y == destination.Y - 2) ||
                (GetPiecePosition().X == destination.X - 1 && GetPiecePosition().Y == destination.Y - 2) ||
                (GetPiecePosition().X == destination.X - 2 && GetPiecePosition().Y == destination.Y - 1) ||
                (GetPiecePosition().X == destination.X - 2 && GetPiecePosition().Y == destination.Y + 1) ||
                (GetPiecePosition().X == destination.X - 1 && GetPiecePosition().Y == destination.Y + 2) ||
                (GetPiecePosition().X == destination.X + 1 && GetPiecePosition().Y == destination.Y + 2))
                return true;
            return false;
        }
        public override bool PieceToDestinationIsEmpty(Point destination)
        {
            return true;
        }

        internal override bool ValidMove(Point destination)
        {
            if (ValidDestination(destination))
            {
                if (pieceMatrix.MSquareIsEmpty(destination))
                    return true;
                if (pieceMatrix.MSquareIsOppositePiece(destination, GetPieceColor()) && !pieceMatrix.MSquareIsOppositeKing(destination, GetPieceColor()))
                    return true;
            }
           return false;
        }
    }
}
