using ChessBoard;
using System;
using System.Collections.Generic;

namespace Chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public bool Finished { get; private set; }
        int turn;
        Color currentPlayer;

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

        public void ExecuteMove(Position origin, Position destination) {
            Piece movedPiece = board.RemovePiece(origin);
            movedPiece.IncrementMove();
            Piece capturedPiece = board.RemovePiece(destination);
            board.PlacePiece(movedPiece, destination);
        }
    }
}
