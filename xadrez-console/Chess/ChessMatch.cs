using ChessBoard;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public bool Finished { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            PlaceInitialPieces();
        }

        void PlaceInitialPieces() {
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('c', 1).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('c', 2).ToPosition());
            board.PlacePiece(new King(board, Color.White), new ChessPosition('d', 1).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('d', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('e', 1).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('e', 2).ToPosition());

            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('c', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('c', 8).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('d', 7).ToPosition());
            board.PlacePiece(new King(board, Color.Black), new ChessPosition('d', 8).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('e', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('e', 8).ToPosition());
        }

        public void ValidateOriginPosition(ChessPosition pos) => ValidateOriginPosition(pos.ToPosition());
        public void ValidateOriginPosition(Position pos) {
            Piece piece = board.GetPiece(pos);
            if (piece == null) throw new BoardException($"There is no piece at desired position! ({pos.ToChessPosition()})");
            if (piece.color != currentPlayer) throw new BoardException($"Invalid piece! ({piece.Name} at {piece.position.ToChessPosition()})");
            if (!piece.MoveAvailable()) throw new BoardException($"No possible moves for this piece! ({piece.Name} at {piece.position.ToChessPosition()})");
        }

        public void ValidateDestinationPosition(ChessPosition origin, ChessPosition destination) => ValidateDestinationPosition(origin.ToPosition(), destination.ToPosition());
        public void ValidateDestinationPosition(Position origin, Position destination) {
            if (!board.GetPiece(origin).MoveToAvailable(destination)) throw new BoardException($"Invalid destination position!" +
                $" ({board.GetPiece(origin).Name} to {destination.ToChessPosition()})");
        }

        public void Move(Position origin, Position destination) {
            ExecuteMove(origin, destination);
            turn++;
            ChangeCurrentPlayer();
        }

        private void ChangeCurrentPlayer() {
            if (currentPlayer == Color.White) currentPlayer = Color.Black;
            else currentPlayer = Color.White;
        }

        private void ExecuteMove(Position origin, Position destination) {
            Piece movedPiece = board.RemovePiece(origin);
            movedPiece.IncrementMove();
            Piece capturedPiece = board.RemovePiece(destination);
            board.PlacePiece(movedPiece, destination);
        }
    }
}
