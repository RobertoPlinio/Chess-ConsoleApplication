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

        HashSet<Piece> pieces;
        HashSet<Piece> capturedPieces;

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PlaceInitialPieces();
        }

        void PlacePiece(Piece piece, char column, int row) {
            board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        void PlaceInitialPieces() {
            PlacePiece(new Rook(board, Color.White), 'c', 1);
            PlacePiece(new Rook(board, Color.White), 'c', 2);
            PlacePiece(new King(board, Color.White), 'd', 1);
            PlacePiece(new Rook(board, Color.White), 'd', 2);
            PlacePiece(new Rook(board, Color.White), 'e', 1);
            PlacePiece(new Rook(board, Color.White), 'e', 2);

            PlacePiece(new Rook(board, Color.Black), 'c', 7);
            PlacePiece(new Rook(board, Color.Black), 'c', 8);
            PlacePiece(new Rook(board, Color.Black), 'd', 7);
            PlacePiece(new King(board, Color.Black), 'd', 8);
            PlacePiece(new Rook(board, Color.Black), 'e', 7);
            PlacePiece(new Rook(board, Color.Black), 'e', 8);
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

        public HashSet<Piece> GetCapturedPiecesByColor(Color color) {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach (Piece p in capturedPieces) if (p.color == color) temp.Add(p);
            return temp;
        }

        public HashSet<Piece> GetAlivePiecesByColor(Color color) {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach (Piece p in capturedPieces) if (p.color == color) temp.Add(p);
            temp.ExceptWith(GetCapturedPiecesByColor(color));
            return temp;
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
            if (capturedPiece != null) capturedPieces.Add(capturedPiece);
        }
    }
}
