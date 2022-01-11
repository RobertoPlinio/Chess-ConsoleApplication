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

        public Piece(Board board, Color color) {
            this.position = null;
            this.board = board;
            this.color = color;
            moveCount = 0;
        }

        public void IncrementMove() => moveCount++;

        public abstract bool[,] PossibleMoves();

        protected bool CanMove(Position pos) {
            Piece p = board.GetPiece(pos);
            return p == null || p.color != color;
        }
    }
}
