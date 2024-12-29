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
        }

        public override bool ValidDestination(Point destination)
        {
            if ((Math.Abs(GetPiecePosition().X - destination.X) == 2 && GetPiecePosition().Y == destination.Y && 
                    (GetPiecePosition().X==1||GetPiecePosition().X==6))||
                (Math.Abs(GetPiecePosition().X - destination.X) == 1 && GetPiecePosition().Y==destination.Y) ||
                (Math.Abs(GetPiecePosition().X - destination.X) == 1 && Math.Abs(GetPiecePosition().Y - destination.Y) == 1))
            return true;
            return false;
        }
        public override bool PieceToDestinationIsEmpty(Point destination)
        {
            if(CanTakeFrontPiece(destination))
            {
                return true;
            }
            return false;
        }

        public bool OnePositionIsValid(Point destination)
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
        public bool CanTakeFrontPiece(Point destination)
        {
            if(GetPieceColor() == Colors.black)
            {
                if (destination.X + 1 == GetPiecePosition().X &&
                    (GetPiecePosition().Y == destination.Y-1||GetPiecePosition().Y==destination.Y+1))
                    return true;
                else return false;
            }
            if (GetPieceColor() == Colors.white)
            {
                if (destination.X - 1 == GetPiecePosition().X &&
               (GetPiecePosition().Y == destination.Y - 1 || GetPiecePosition().Y == destination.Y + 1))
                    return true;
                else return false;
            }
            return false;
        }
        public bool TwoPositionIsValid(Point destination)
        {   
            if(GetPieceColor()==Colors.black)
            {
                if (GetPiecePosition().X == 6 &&
                    destination.X + 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    pieceMatrix.MSquareIsEmpty(destination.X+1, destination.Y))
                    return true;
                else return false;
            }
            if (GetPieceColor() == Colors.white)
            {
                if (GetPiecePosition().X == 1 &&
                    destination.X - 2 == GetPiecePosition().X &&
                    GetPiecePosition().Y == destination.Y &&
                    pieceMatrix.MSquareIsEmpty(destination.X - 1, destination.Y))
                    return true;
                else return false;
            }
            return false;
        }
        internal override bool ValidMove(Point destination)
        {

            if (OnePositionIsValid(destination))
            {
                if (pieceMatrix.MSquareIsEmpty(destination))
                    return true;
            }
            if (CanTakeFrontPiece(destination))
            {
                if (pieceMatrix.MSquareIsOppositePiece(destination,GetPieceColor()) && !pieceMatrix.MSquareIsOppositeKing(destination, GetPieceColor()))
                    return true;
            }
            if (TwoPositionIsValid(destination))
                return true;
            else return false;
        }
    }
}
