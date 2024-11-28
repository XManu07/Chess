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
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if ((GetPiecePosition().X==destination.X+2 && GetPiecePosition().Y==destination.Y+1)||
                (GetPiecePosition().X==destination.X+2 && GetPiecePosition().Y==destination.Y-1)||
                (GetPiecePosition().X == destination.X + 1 && GetPiecePosition().Y == destination.Y - 2)||
                (GetPiecePosition().X == destination.X -1 && GetPiecePosition().Y == destination.Y - 2) ||
                (GetPiecePosition().X == destination.X -2 && GetPiecePosition().Y == destination.Y - 1) ||
                (GetPiecePosition().X == destination.X -2 && GetPiecePosition().Y == destination.Y +1) ||
                (GetPiecePosition().X == destination.X -1 && GetPiecePosition().Y == destination.Y +2) ||
                (GetPiecePosition().X == destination.X + 1 && GetPiecePosition().Y == destination.Y + 2))
            {
                /*
                 * if destination =empty 
                 *      return true
                 *  else return false
                 *  if piece from destination color != piece color
                 *      return true
                    else return false
                 */ 

                Console.WriteLine("mutare valida");
                return true;
            }
            else
            {
                Console.WriteLine("mutare invalida");
                return false;             
            }
        }
    }
}
