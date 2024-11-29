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
            SetPieceImage();
        }
        public bool ValidSquare (Point destination)
        {
            if (Math.Abs(destination.X - this.GetPiecePosition().X) == Math.Abs(this.GetPiecePosition().Y - destination.Y))
                return true;
            return false;
        }
        private bool BishopToDestinationIsEmpty(Point destination, int[,] allPieces)
        {
            if(GetPiecePosition().X-destination.X< 0)
            {
                int j = destination.Y;
                for (int i = destination.X - 1; i > GetPiecePosition().X; i--)
                {   
                    if (GetPiecePosition().Y - destination.Y > 0)
                        j++;
                    else j--;
                    if (!SquareIsEmpty(i, j, allPieces))
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
                    if (!SquareIsEmpty(i, j, allPieces))
                        return false;
                }
            }
            return true;
        }

        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            
            if(ValidSquare(destination))
            {
                if (BishopToDestinationIsEmpty(destination, allPieces)&&SquareIsEmpty(destination,allPieces))
                    return true;
                if(BishopToDestinationIsEmpty(destination, allPieces))
                {
                    if(SquareIsOpositePiece(destination, allPieces)&&!SquareIsOpositeKing(destination,allPieces)) 
                        return true;
                }
            }
            return false;
        }
    }
}
