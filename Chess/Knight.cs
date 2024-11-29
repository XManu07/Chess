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
            SetPieceImage();
        }
        public bool ValidSquare(Point destination)
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
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (ValidSquare(destination))
            {
                if (SquareIsEmpty(destination, allPieces))
                    return true;
                if (SquareIsOpositePiece(destination, allPieces) && !SquareIsOpositeKing(destination, allPieces))
                    return true;
            }
           return false;
        }
    }
}
