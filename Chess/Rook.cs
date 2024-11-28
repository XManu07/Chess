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
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (this.GetPiecePosition().X==destination.X || this.GetPiecePosition().Y==destination.Y)
            {
                //if all position from rook to destination = empty(destination included)
                //    return true
                //else return false
                //if all position from rook to destination - 1 = empty
                //    if piece color from destination != piece color
                //        return true
                //    else return false
                //else return false
                Console.WriteLine("mutare valida ");
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
