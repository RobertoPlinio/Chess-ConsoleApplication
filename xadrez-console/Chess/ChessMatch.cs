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
        public bool check { get; private set; }

        HashSet<Piece> pieces;
        HashSet<Piece> capturedPieces;

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PlaceInitialPieces();
        }

        void PlacePiece(Piece piece, char column, int row) {
            board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        void PlaceInitialPieces() {
            PlacePiece(new Rook(board, Color.Black), 'a', 8);
            PlacePiece(new Knight(board, Color.Black), 'b', 8);
            PlacePiece(new Bishop(board, Color.Black), 'c', 8);
            PlacePiece(new Queen(board, Color.Black), 'd', 8);
            PlacePiece(new King(board, Color.Black, this), 'e', 8);
            PlacePiece(new Bishop(board, Color.Black), 'f', 8);
            PlacePiece(new Knight(board, Color.Black), 'g', 8);
            PlacePiece(new Rook(board, Color.Black), 'h', 8);
            PlacePiece(new Pawn(board, Color.Black), 'a', 7);
            PlacePiece(new Pawn(board, Color.Black), 'b', 7);
            PlacePiece(new Pawn(board, Color.Black), 'c', 7);
            PlacePiece(new Pawn(board, Color.Black), 'd', 7);
            PlacePiece(new Pawn(board, Color.Black), 'e', 7);
            PlacePiece(new Pawn(board, Color.Black), 'f', 7);
            PlacePiece(new Pawn(board, Color.Black), 'g', 7);
            PlacePiece(new Pawn(board, Color.Black), 'h', 7);

            PlacePiece(new Rook(board, Color.White), 'a', 1);
            PlacePiece(new Knight(board, Color.White), 'b', 1);
            PlacePiece(new Bishop(board, Color.White), 'c', 1);
            PlacePiece(new Queen(board, Color.White), 'd', 1);
            PlacePiece(new King(board, Color.White, this), 'e', 1);
            PlacePiece(new Bishop(board, Color.White), 'f', 1);
            PlacePiece(new Knight(board, Color.White), 'g', 1);
            PlacePiece(new Rook(board, Color.White), 'h', 1);
            PlacePiece(new Pawn(board, Color.White), 'a', 2);
            PlacePiece(new Pawn(board, Color.White), 'b', 2);
            PlacePiece(new Pawn(board, Color.White), 'c', 2);
            PlacePiece(new Pawn(board, Color.White), 'd', 2);
            PlacePiece(new Pawn(board, Color.White), 'e', 2);
            PlacePiece(new Pawn(board, Color.White), 'f', 2);
            PlacePiece(new Pawn(board, Color.White), 'g', 2);
            PlacePiece(new Pawn(board, Color.White), 'h', 2);
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
            if (!board.GetPiece(origin).IsMovePossible(destination)) throw new BoardException($"Invalid destination position!" +
                $" ({board.GetPiece(origin).Name} to {destination.ToChessPosition()})");
        }

        public void Move(Position origin, Position destination) {
            Piece captured = ExecuteMove(origin, destination);

            if (IsInCheck(currentPlayer)) {
                UndoMove(origin, destination, captured);
                throw new BoardException("You can't put yourself in Check!");
            }

            check = IsInCheck(Adversary(currentPlayer));

            if (IsInCheckMate(Adversary(currentPlayer))) {
                Finished = true;
            } else {
                turn++;
                ChangeCurrentPlayer();
            }
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color) {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach (Piece p in capturedPieces) if (p.color == color) temp.Add(p);
            return temp;
        }

        public HashSet<Piece> GetAlivePiecesByColor(Color color) {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach (Piece p in pieces) if (p.color == color) temp.Add(p);
            temp.ExceptWith(GetCapturedPiecesByColor(color));
            return temp;
        }

        public bool IsInCheck(Color color) {
            Piece king = GetKing(color);
            if (king == null) throw new BoardException($"There is no King for player {color}!");

            foreach (Piece p in GetAlivePiecesByColor(Adversary(color))) {
                bool[,] possibleMoves = p.PossibleMoves();
                if (possibleMoves[king.position.Row, king.position.Column]) return true;
            }

            return false;
        }

        public bool IsInCheckMate(Color color) {
            if (!IsInCheck(color)) return false;

            //Will simulate every possible move for every piece alive to see if check can be negated
            foreach (Piece p in GetAlivePiecesByColor(color)) {
                bool[,] possibleMoves = p.PossibleMoves();
                for (int i = 0; i < board.Rows; i++) {
                    for (int j = 0; j < board.Columns; j++) {
                        if (possibleMoves[i, j]) {
                            Position origin = p.position;
                            Position destination = new Position(i, j);
                            Piece cachedPiece = ExecuteMove(origin, destination);
                            bool isInCheck = IsInCheck(color);
                            UndoMove(origin, destination, cachedPiece);
                            if (!isInCheck) return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ChangeCurrentPlayer() {
            if (currentPlayer == Color.White) currentPlayer = Color.Black;
            else currentPlayer = Color.White;
        }

        private Piece ExecuteMove(Position origin, Position destination) {
            Piece movedPiece = board.RemovePiece(origin);
            movedPiece.IncrementMove();
            Piece capturedPiece = board.RemovePiece(destination);
            board.PlacePiece(movedPiece, destination);
            if (capturedPiece != null) capturedPieces.Add(capturedPiece);

            //#SpecialMove Castle
            if (movedPiece is King && MathF.Abs(origin.Column - destination.Column) > 1) {
                Piece rook;
                Position originRook, destinationRook;

                if (destination.Column == 6) {
                    //King Side
                    originRook = new Position(origin.Row, 7);
                    destinationRook = new Position(origin.Row, 5);

                    rook = board.RemovePiece(originRook);
                    board.PlacePiece(rook, destinationRook);
                    rook.IncrementMove();
                } else if (destination.Column == 2) {
                    //Queen Side
                    originRook = new Position(origin.Row, 0);
                    destinationRook = new Position(origin.Row, 3);

                    rook = board.RemovePiece(originRook);
                    board.PlacePiece(rook, destinationRook);
                    rook.IncrementMove();
                }
            }

            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destination, Piece undoPiece) {
            Piece p = board.RemovePiece(destination);
            p.DecrementMove();

            if (undoPiece != null) {
                board.PlacePiece(undoPiece, destination);
                capturedPieces.Remove(undoPiece);
            }

            board.PlacePiece(p, origin);

            //#SpecialMove Castle
            if (p is King && MathF.Abs(origin.Column - destination.Column) > 1) {
                Piece rook;
                Position originRook, destinationRook;

                if (destination.Column == 6) {
                    //King Side
                    originRook = new Position(origin.Row, 5);
                    destinationRook = new Position(origin.Row, 7);

                    rook = board.RemovePiece(originRook);
                    board.PlacePiece(rook, destinationRook);
                    rook.DecrementMove();
                } else if (destination.Column == 2) {
                    //Queen Side
                    originRook = new Position(origin.Row, 3);
                    destinationRook = new Position(origin.Row, 0);

                    rook = board.RemovePiece(originRook);
                    board.PlacePiece(rook, destinationRook);
                    rook.DecrementMove();
                }
            }
        }

        private Color Adversary(Color color) {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece GetKing(Color color) {
            foreach (Piece p in GetAlivePiecesByColor(color)) {
                if (p is King) return p;
            }
            return null;
        }
    }
}
