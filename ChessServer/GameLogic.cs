using System;
using System.Drawing;
using System.Runtime.InteropServices;

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

            GenerateLValidMoves();
            StartGame();
        }

        private void StartGame()
        {
            while (!CheckMate())
            {
                if (currentPlayer.Moved)
                {
                    if (MoveOfCurrentPlayerIsValid())
                    {
                        SendMessagesToPlayers();
                        UpdateMatrix();
                        SwitchPlayer();
                        ClearLValidMoves();
                        GenerateLValidMoves();
                    }
                }
            }
        }
        #region ValidMoves
        private void GenerateLValidMoves()
        {
            GeneratePlayerLValidMoves(currentPlayer);
            GeneratePlayerLValidMoves(opponentPlayer);
        }
        public void GeneratePlayerLValidMoves(Player player)
        {
            foreach (Piece piece in player.GetPieces())
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Point newPos = new Point(i, j);
                        if(VeryfiMove(player,piece,newPos))//dest invalid
                        {
                            piece.GetLValidMoves().Add(new Point(i, j));
                        }
                    }
                }
            }
        }
        private void ClearLValidMoves()
        {
            currentPlayer.ClearLValidMoves();
            opponentPlayer.ClearLValidMoves();
        }
        #endregion 
        private bool VeryfiMove(Player player, Piece piece, Point newPos)
        {
            if (piece.ValidMove(newPos))
            {
                if (Check(player.GetKingPoint(), piece,player,GetOpponentPlayer(player)))
                    return false;
                return true;
            }
            return false;
        }

        private Player GetOpponentPlayer(Player player)
        {
            if (player == currentPlayer)
                return opponentPlayer;
            return currentPlayer;
        }

        private void UpdateMatrix()
        {
            if (boardMatrix.MSquareIsOppositePiece(currentPlayer.NewPiecePosition, currentPlayer.PlayerColorOfPieces))
            {
                Piece pieceToRemove = currentPlayer.GetPieceFromPos(currentPlayer.NewPiecePosition);
                opponentPlayer.RemovePiece(pieceToRemove);
            }

            boardMatrix.MUpdateOldPos(currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition).GetPiecePosition());
            currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition).SetPosition(currentPlayer.NewPiecePosition);
            boardMatrix.MInitPieces(currentPlayer, opponentPlayer);
            boardMatrix.MShow();
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
        public void SendMessagesToPlayers()
        {
            currentPlayer.WriteGoodMove();
            string move = GetCurrentPlayerMove();
            opponentPlayer.WriteCurrentPlayerMove(move);
            currentPlayer.Moved = false;
        }
        private bool MoveOfCurrentPlayerIsValid()
        {
            foreach(Piece piece in currentPlayer.GetPieces())
            {
                foreach(Point move in piece.GetLValidMoves())
                {
                    if (move==currentPlayer.NewPiecePosition)
                        return true;
                }
            }
            return false;
        }
        public bool Check(Point kingPos, Piece pieceFromImage,Player currentPlayer,Player opponentPlayer)
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
                    {
                        piece.SetPosition(new Point(-1, -1));
                    }
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
                if (piece.GetPiecePosition().X == -1)
                    continue;
                if (piece.KingPosIsValidMove(kingPos))
                    check = 1;
            }

            pieceFromImage.SetPosition(OldPosition);
            CopyOldMatrix();

            foreach (Piece opiece in opponentPlayer.GetPieces())
            {
                if (opiece.GetPiecePosition().X == -1)
                    opiece.SetPosition(currentPlayer.NewPiecePosition);
            }
            return check == 1 ? true : false;
        }
        private bool CheckMate()
        {
            if (currentPlayer.HasValidMoves())
            {
                return false;
            }
            return true;
        }
        public void SwitchPlayer()
        {
            Player temp = currentPlayer;
            currentPlayer = opponentPlayer;
            opponentPlayer = temp;
        }
        public string GetCurrentPlayerMove()
        {
            return  currentPlayer.OldPiecePosition.X.ToString() + 
                    currentPlayer.OldPiecePosition.Y.ToString() +
                    currentPlayer.NewPiecePosition.X.ToString() + 
                    currentPlayer.NewPiecePosition.Y.ToString();
        }

    }
}