using System;
using System.Drawing;

namespace Chess
{
    public enum Colors { white=1, black=-1 };
    
    internal class GameLogic
    {
        private Player currentPlayer;
        private Player opponentPlayer;

        BoardMatrix boardMatrix;

        private Piece pieceToRemove;
        private Piece pieceFromImage;

        Point currentKingPosition;

        public GameLogic(Player player1, Player player2)
        {
            currentPlayer= player1;
            opponentPlayer= player2;
            Piece.matrix = new BoardMatrix(player1,player2);
            boardMatrix = Piece.matrix;
            GenerateValidMoves();
            StartGame();
        }

        private void GenerateValidMoves()
        {
            currentPlayer.GeneratePiecesValidMoves();
            opponentPlayer.GeneratePiecesValidMoves();
        }

        private void StartGame()
        {
            while (!CheckMate())
            {
                if(currentPlayer.Moved)
                { 
                    if (currentPlayer.VeryfiMove(opponentPlayer,boardMatrix))
                    {
                        currentPlayer.WriteGoodMove();
                        string move = currentPlayer.GetOldPiecePos().X.ToString() + currentPlayer.GetOldPiecePos().Y.ToString() +
                            currentPlayer.GetNewPiecePos().X.ToString() + currentPlayer.GetNewPiecePos().Y.ToString();
                        opponentPlayer.WriteCurrentMove(move);

                        currentPlayer.Moved= false;
                        SwitchPlayer();
                        GenerateValidMoves();
                    }
                }
            }
        }

        private bool CheckMate()
        {
            if (currentPlayer.HasValidMoves())
            {
                return false;
            }
            return true;
        }

        public void PieceChangePos()
        {
            if (pieceFromImage != null &&
                pieceFromImage.ValidMove(currentPlayer.GetNewPiecePos()))
            {
                currentKingPosition = currentPlayer.GetKingPoint();
                if (currentPlayer.Check(currentKingPosition, opponentPlayer, boardMatrix, pieceFromImage,currentPlayer.GetNewPiecePos()))
                {
                    Console.WriteLine("Check");
                    return;
                }
                if (boardMatrix.MSquareIsOppositePiece(currentPlayer.GetNewPiecePos(),currentPlayer.GetPlayerColor()))
                {
                    pieceToRemove = GetPieceToRemove(currentPlayer);
                    opponentPlayer.RemovePiece(pieceToRemove);
                }
                UpdateSelectedPiece();
                SwitchPlayer();
                pieceFromImage = null;
            }
        }
        public void UpdateSelectedPiece()
        {
            boardMatrix.MUpdateOldPos(pieceFromImage.GetPiecePosition());
            pieceFromImage.SetPosition(currentPlayer.GetNewPiecePos());
            boardMatrix.MInitPieces(currentPlayer, opponentPlayer);
            boardMatrix.MShow();
        }
        public Piece GetPieceToRemove(Player currentPlayer)
        {
            Piece pieceToRemove;
            foreach(var piece in opponentPlayer.GetPieces())
            {
                if (piece.GetPiecePosition()==currentPlayer.GetNewPiecePos())
                {
                    pieceToRemove = piece;
                    return pieceToRemove;
                }
            }
            

            return null;
        }

        public void SwitchPlayer()
        {
            Player temp = currentPlayer;
            currentPlayer = opponentPlayer;
            opponentPlayer = temp;
        }

    }
}