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

        int[,] oldMatrix;
        Point oldPosition;

        public Point OldPosition { get => oldPosition; set => oldPosition = value; }

        public GameLogic(Player player1, Player player2)
        {
            currentPlayer= player1;
            opponentPlayer= player2;

            Piece.matrix = new BoardMatrix(player1,player2);
            boardMatrix = Piece.matrix;
            oldMatrix = new int[8, 8];

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
                if (currentPlayer.Moved)
                {
                    if (VeryfiMove())
                    {
                        currentPlayer.WriteGoodMove();
                        string move = currentPlayer.OldPiecePosition.X.ToString() + currentPlayer.OldPiecePosition.Y.ToString() +
                            currentPlayer.NewPiecePosition.X.ToString() + currentPlayer.NewPiecePosition.Y.ToString();
                        opponentPlayer.WriteCurrentMove(move);

                        currentPlayer.Moved = false;
                        SwitchPlayer();
                        ClearLValidMoves();
                        GenerateValidMoves();
                    }
                }
            }
        }

        private void ClearLValidMoves()
        {
            currentPlayer.ClearLValidMoves();
            opponentPlayer.ClearLValidMoves();
        }

        public bool VeryfiMove()
        {
            Piece selectedPiece = currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition);
            if (selectedPiece != null &&
                selectedPiece.ValidMove(currentPlayer.NewPiecePosition))
            {
                if (Check(currentPlayer.GetKingPoint(), selectedPiece))
                {
                    return false;
                }
                if (boardMatrix.MSquareIsOppositePiece(currentPlayer.NewPiecePosition, currentPlayer.PlayerColorOfPieces))
                {
                    Piece pieceToRemove = currentPlayer.GetPieceFromPos(currentPlayer.NewPiecePosition);
                    opponentPlayer.RemovePiece(pieceToRemove);
                }

                boardMatrix.MUpdateOldPos(selectedPiece.GetPiecePosition());
                selectedPiece.SetPosition(currentPlayer.NewPiecePosition);
                boardMatrix.MInitPieces(currentPlayer, opponentPlayer);
                boardMatrix.MShow();
                return true;
            }
            return false;
        }
        public bool Check(Point kingPos, Piece pieceFromImage)
        {
            CopyBoardMatrix();
            OldPosition = pieceFromImage.GetPiecePosition();

            int check = 0;
            pieceFromImage.SetPosition(currentPlayer.NewPiecePosition);

            if (boardMatrix.allPieces[currentPlayer.NewPiecePosition.X, currentPlayer.NewPiecePosition.Y] != 0)
            {
                foreach (Piece piece in opponentPlayer.GetPieces())
                {
                    if (piece.GetPiecePosition() == currentPlayer.NewPiecePosition)
                        piece.SetPosition(default);
                }
            }
            boardMatrix.MUpdateOldPos(oldPosition);
            boardMatrix.MInitPieces(currentPlayer, opponentPlayer);

            if (pieceFromImage.GetPieceName() == PieceNames.king)
            {
                kingPos = currentPlayer.NewPiecePosition;
            }
            foreach (Piece piece in opponentPlayer.GetPieces())
            {
                if (piece.KingPosIsValidMove(kingPos))
                    check = 1;
            }

            pieceFromImage.SetPosition(OldPosition);
            CopyOldMatrix();
           
            return check == 1 ? true : false;
        }
        public void CopyBoardMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    oldMatrix[i, j] = boardMatrix.allPieces[i, j];
                }
            }
        }
        public void CopyOldMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardMatrix.allPieces[i, j] = oldMatrix[i, j];
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