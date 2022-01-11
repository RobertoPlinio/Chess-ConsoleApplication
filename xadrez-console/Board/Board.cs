using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private Piece[,] pieces;

        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece GetPiece(int row, int column) => pieces[row, column];
        public Piece GetPiece(Position pos) => pieces[pos.Row, pos.Column];

        public bool PieceExists(Position pos) {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void PlacePiece (Piece piece, Position pos) {
            if (PieceExists(pos)) throw new BoardException($"Position occupied by another piece! ({pos})");
            pieces[pos.Row, pos.Column] = piece;
            piece.position = pos;
        }

        public Piece RemovePiece(Position pos) {
            if (!PieceExists(pos)) return null;

            Piece temp = GetPiece(pos);
            pieces[pos.Row, pos.Column] = null;
            return temp;
        }

        public void ValidatePosition(Position pos) {
            if (!IsPositionValid(pos)) throw new BoardException($"Invalid board position! ({pos})");
        }

        public bool IsPositionValid(Position pos) {
            if (pos.Row < 0 || pos.Column < 0 || pos.Row >= Rows || pos.Column >= Columns) return false;
            return true;
        }
    }
}
