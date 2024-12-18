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

            //if current player is in check
            //if current player doesn t have valid moves that stop the check
            //  return true
            //return false
        }
        public void SwitchPlayer()
        {
            Player temp = currentPlayer;
            currentPlayer = opponentPlayer;
            opponentPlayer = temp;
        }

    }
}