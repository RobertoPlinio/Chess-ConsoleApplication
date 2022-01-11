using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moveCount { get; protected set; }
        public Board board { get; protected set; }
        public string Name { get; set; }

        public Piece(Board board, Color color) {
            this.position = null;
            this.board = board;
            this.color = color;
            moveCount = 0;
        }

        public void IncrementMove() => moveCount++;

        public bool MoveAvailable() {
            bool[,] possibleMoves = PossibleMoves();
            for (int i = 0; i < board.Columns; i++) {
                for (int j = 0; j < board.Rows; j++) {
                    if (possibleMoves[j, i]) return true;
                }
            }
            return false;
        }

        public bool MoveToAvailable(Position destination) {
            return PossibleMoves()[destination.Row, destination.Column];
        }

        public abstract bool[,] PossibleMoves();

        protected bool CanMove(Position pos) {
            Piece p = board.GetPiece(pos);
            return p == null || p.color != color;
        }
    }
}
