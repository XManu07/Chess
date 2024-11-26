﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum PieceColors { white, black };
    public enum PieceNames { pawn, rook, knight, bishop, king, queen };

    struct Position
    {
        int x;
        int y;
    }
    internal interface Rules
    {
        bool AvailableMove();

    }
}
