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
            SetPieceImage();
        }
        public bool EmptyRookToDestination(Point destination,int[,] allPieces)
        {
            if (GetPiecePosition().X == destination.X)
            {
                for (int i = GetPiecePosition().Y+1; i < destination.Y; i++)
                {
                    if (SquareIsEmpty(destination.X, i, allPieces) == false)
                        return false;
                }
                for (int i = GetPiecePosition().Y-1; i > destination.Y; i--)
                {
                    if (SquareIsEmpty(destination.X, i, allPieces)==false)
                        return false;
                }

            }
            if(GetPiecePosition().Y == destination.Y)
            {
                for (int i = GetPiecePosition().X+1; i < destination.X; i++)
                {
                    if (SquareIsEmpty(i, destination.Y, allPieces)==false)
                        return false;
                }
                for (int i = GetPiecePosition().X-1; i >destination.X; i--)
                {
                    if (SquareIsEmpty(i, destination.Y, allPieces)==false)
                        return false;
                }
            }
            return true;
        }
        public bool ValidSquare(Point destination)
        {
            if (this.GetPiecePosition().X == destination.X || this.GetPiecePosition().Y == destination.Y)
                return true;
            return false;
        }
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (ValidSquare(destination))
            {
                if (EmptyRookToDestination(destination, allPieces)&&SquareIsEmpty(destination,allPieces))
                    return true;
                if (EmptyRookToDestination(destination, allPieces))
                    if (SquareIsOpositePiece(destination, allPieces)&&!SquareIsOpositeKing(destination,allPieces))
                        return true;
            }
            return false;
        }
    }
}
