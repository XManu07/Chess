using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Player
    {
        public String name;
        PieceColors piece_colors;
        public List<Piece> pieces;

        void InitPieces()
        {
            Position pos = new Position();
            if (piece_colors == PieceColors.white)
            {
                //init white 
                //pawns
                pos.x = 1;
                for (int i = 0; i < 8; i++)
                {
                    pos.y = i;
                    pieces.Add(new Pawn(PieceNames.pawn, PieceColors.white, pos));
                }

                pos.x = 0;

                pos.y = 0;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.white, pos));
                pos.y = 7;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.white, pos));

                pos.y = 1;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.white, pos));
                pos.y = 6;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.white, pos));

                pos.y = 2;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.white, pos));
                pos.y = 5;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.white, pos));

                pos.y = 3;
                pieces.Add(new Queen(PieceNames.queen, PieceColors.white, pos));
                pos.y = 4;
                pieces.Add(new King(PieceNames.king, PieceColors.white, pos));
            }

            if (piece_colors == PieceColors.black)
            {
                
                //init black 
                //pawns
                pos.x = 6;
                for (int i = 0; i < 8; i++)
                {
                    pos.y = i;
                    pieces.Add(new Pawn(PieceNames.pawn, PieceColors.black, pos));
                }

                pos.x = 7;

                pos.y = 0;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.black, pos));
                pos.y = 7;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.black, pos));

                pos.y = 1;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.black, pos));
                pos.y = 6;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.black, pos));

                pos.y = 2;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.black, pos));
                pos.y = 5;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.black, pos));

                pos.y = 3;
                pieces.Add(new Queen(PieceNames.queen, PieceColors.black, pos));
                pos.y = 4;
                pieces.Add(new King(PieceNames.king, PieceColors.black, pos));

            }
        }
    }
}
