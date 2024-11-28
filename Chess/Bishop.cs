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

        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            
            if(Math.Abs(destination.X-this.GetPiecePosition().X)== Math.Abs(this.GetPiecePosition().Y-destination.Y))
            {
                //if all pieces from bishop to destination = empty(destination included)
                //    return true
                //else return false
                //if all pieces from bishop to destination - 1 = empty
                //    if color of piece from dest != piece color
                //        return true
                //    else return false
                //else return false
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
