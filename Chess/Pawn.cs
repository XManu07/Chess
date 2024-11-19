using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Pawn: Piece 
    {
        public Pawn(PieceNames name,PieceColors color,Position pos)
            :base(name,color,pos) 
        { 
            
        }
    }
}
