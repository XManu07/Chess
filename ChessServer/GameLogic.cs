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

            Piece.pieceMatrix = new BoardMatrix(player1,player2);
            boardMatrix = Piece.pieceMatrix;
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
            //GeneratePlayerLValidMoves(opponentPlayer);
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
                        if(GoodMove(player,piece,newPos))
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
        private bool GoodMove(Player player, Piece piece, Point newPos)
        {
            if (piece.ValidMove(newPos))
            {
                if (Check(player, piece,newPos))
                    return false;
                return true;
            }
            return false;
        }
        public bool Check(Player currentPlayer,Piece piece,Point destination)
        {
            Player opponentPlayer = GetOpponentPlayer(currentPlayer);
            SavePiecePos(piece);                  
            SaveCurrentBoardMatrix();

            if (DestIsOppositePiece(destination,currentPlayer))
            {
                Piece opponentPieceFromDestination = GetOpponentPieceFromDestination(destination,opponentPlayer);
                ErasePieceFromMatrix(destination);
                SetPiecePosOutOfBoard(opponentPieceFromDestination);
            }

            UpdatePiecePos(piece,destination); 
            UpdateMatrix(piece, destination);

            foreach(Piece opponentPiece in opponentPlayer.GetPieces())
            {
                if (opponentPiece.KingPosIsValidMove(currentPlayer.GetKingPoint())) 
                {
                    RestauratePiecePos(piece,opponentPlayer,destination);
                    RestaurateBoardMatrix();
                    return true;
                }
            }

            RestauratePiecePos(piece,opponentPlayer,destination);
            RestaurateBoardMatrix();
            return false;
        }
        private void UpdateMatrix(Piece piece, Point destination)
        {
            boardMatrix.allPieces[destination.X, destination.Y] = (int)piece.GetPieceColor() * (int)piece.GetPieceName();
        }
        private void UpdateMatrix()
        {
            if (boardMatrix.MSquareIsOppositePiece(currentPlayer.NewPiecePosition, currentPlayer.PlayerColorOfPieces))
            {
                Piece pieceToRemove = opponentPlayer.GetPieceFromPos(currentPlayer.NewPiecePosition);
                opponentPlayer.RemovePiece(pieceToRemove);
            }

            boardMatrix.MErasePieceFromDestination(currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition).GetPiecePosition());
            currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition).SetPosition(currentPlayer.NewPiecePosition);
            boardMatrix.MInitPieces(currentPlayer, opponentPlayer);
            boardMatrix.MShow();
        }
        private void RestaurateBoardMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardMatrix.allPieces[i, j] = oldMatrix[i, j];
                }
            }
        }
        private void RestauratePiecePos(Piece piece,Player opponentPlayer, Point destination)
        {
            RestaurateOpponentPiece(opponentPlayer,destination);
            piece.SetPosition(oldPosition);
        }
        private void RestaurateOpponentPiece(Player opponentPlayer, Point destination)
        {
            foreach(Piece piece in opponentPlayer.GetPieces())
            {
                if (piece.GetPiecePosition().X == -1)
                {
                    piece.SetPosition(destination);
                }
            }
        }
        private void UpdatePiecePos(Piece piece, Point destination)
        {
            piece.SetPosition(destination);
        }
        private void SetPiecePosOutOfBoard(Piece opponentPiece)
        {
            opponentPiece.SetPosition(new Point(-1,-1));
        }
        private void ErasePieceFromMatrix(Point destination)
        {
            boardMatrix.MErasePieceFromDestination(destination);
        }   
        private Piece GetOpponentPieceFromDestination(Point destination,Player opponentPlayer)
        {
            foreach(Piece piece in opponentPlayer.GetPieces())
            {
                if (destination == piece.GetPiecePosition())
                    return piece;
            }
            return null;
        }
        private bool DestIsOppositePiece(Point position,Player currentPlayer)
        {
            if (boardMatrix.MSquareIsOppositePiece(position,currentPlayer.GetPlayerColor())) 
                return true;
            return false;
        }
        private void SavePiecePos(Piece piece)
        {
            OldPosition = piece.GetPiecePosition();
        }
        private void SaveCurrentBoardMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    oldMatrix[i, j] = boardMatrix.allPieces[i, j];
                }
            }
        }
        private Player GetOpponentPlayer(Player player)
        {
            if (player == currentPlayer)
                return opponentPlayer;
            return currentPlayer;
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
            foreach(Point move in currentPlayer.GetPieceFromPos(currentPlayer.OldPiecePosition).GetLValidMoves())
            {
                if (move==currentPlayer.NewPiecePosition)
                    return true;
            }
            return false;
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