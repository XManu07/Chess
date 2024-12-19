using System;
using System.Drawing;


namespace Chess
{
    internal class BoardMatrix
    {
        public int[,] allPieces = new int[8, 8];
        public BoardMatrix(Player p1,Player p2) 
        {
            MInitPieces(p1, p2);
            MShow();
        }
        public void MInitPieces(Player p1, Player p2)
        {
            foreach (var piece in p1.GetPieces())
            {
                if (piece.GetPiecePosition().X == -1) continue;
                allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)piece.GetPieceName()*(int)piece.GetPieceColor();   
            }

            foreach (var piece in p2.GetPieces())
            {
                if (piece.GetPiecePosition().X == -1) continue;
                allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)piece.GetPieceName() * (int)piece.GetPieceColor();
            }
        }
        public void MShow()
        {
            for (int line = 0; line < 8; line++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Console.Write("{0,3}",allPieces[line, column]);
                }
                Console.WriteLine();
            }
        }
        public void MUpdateOldPos(Point oldPos)
        {
            allPieces[oldPos.X, oldPos.Y] = 0;
        }

        public bool MSquareIsEmpty(Point destination)
        {
            return (allPieces[destination.X, destination.Y] == 0) ? true : false;
        }
        public bool MSquareIsEmpty(int x, int y)
        {
            return allPieces[x, y] == (int)PieceNames.gol ? true : false;
        }

        public bool MSquareIsOppositePiece(Point destination,Colors pieceColor)
        {
            if (pieceColor == Colors.black)
                return (allPieces[destination.X, destination.Y]>0) ? true : false;
            if (pieceColor == Colors.white)
                return (allPieces[destination.X, destination.Y]<0) ? true : false;
            return false;
        }
        public bool MSquareIsKing(Point destination)
        {
            if (allPieces[destination.X, destination.Y] == (int)PieceNames.king*(int)Colors.white || 
                allPieces[destination.X, destination.Y] == (int)PieceNames.king*(int)Colors.white)
                return true;
            return false;
        }
        public bool MSquareIsOppositeKing(Point destination,Colors pieceColor)
        {
            if (MSquareIsOppositePiece(destination,pieceColor))
                if (MSquareIsKing(destination))
                    return true;
            return false;
        }


    }
}
