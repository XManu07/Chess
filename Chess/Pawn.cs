using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Pawn: Piece 
    {

        public Pawn(Colors color,Point position):
            base()
        {
            SetPieceName(PieceNames.pawn);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }
        public bool OnePositionIsValid(Point destination,int[,] allPieces)
        {
            if(this.GetPieceColor() == Colors.black)
            {
                if (this.GetPiecePosition().X == destination.X + 1 && this.GetPiecePosition().Y == destination.Y)
                    return true;
            }
            if (this.GetPieceColor() == Colors.white)
            {
                if (this.GetPiecePosition().X == destination.X - 1 && this.GetPiecePosition().Y == destination.Y)
                    return true;
            }
            return false;
        }
        public bool CanTakeFrontPiece(Point destination, int[,] allPieces)
        {
            if(GetPieceColor() == Colors.black)
            {
                if (GetPiecePosition().X == 6 &&
                    destination.X + 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    allPieces[destination.X - 1, destination.Y] == 0)
                    return true;
                else return false;
            }
            if (GetPieceColor() == Colors.white)
            {
                if (this.GetPiecePosition().X == 1 &&
                    destination.X - 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    allPieces[destination.X + 1, destination.Y] == 0)
                    return true;
                else return false;
            }
            return false;
        }
        public bool TwoPositionIsValid(Point destination, int[,] allPieces)
        {   
            if(GetPieceColor()==Colors.black)
            {
                if (GetPiecePosition().X == 6 &&
                    destination.X + 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    SquareIsEmpty(destination.X+1, destination.Y, allPieces))
                    return true;
                else return false;
            }
            if (GetPieceColor() == Colors.white)
            {
                if (GetPiecePosition().X == 1 &&
                    destination.X - 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    SquareIsEmpty(destination.X - 1, destination.Y, allPieces))
                    return true;
                else return false;
            }
            return false;
        }
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (OnePositionIsValid(destination,allPieces))
            {
                if( SquareIsEmpty(destination, allPieces) )
                    return true;
            }
            if (CanTakeFrontPiece(destination, allPieces))
            {
                if (SquareIsOpositePiece(destination, allPieces)&& !SquareIsOpositeKing(destination,allPieces))
                    return true;           
            }
            if (TwoPositionIsValid(destination, allPieces))
                return true;
            else return false;
        }
    }
}
