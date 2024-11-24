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
    internal class Pawn: Piece 
    {
      
        public Pawn(PieceNames name,PieceColors color,Position pos)
            :base(name,color,pos) 
        {
            SetPieceImage();
        }
    }
}
