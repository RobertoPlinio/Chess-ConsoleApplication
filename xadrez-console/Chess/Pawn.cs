using ChessBoard;
using System.Text.RegularExpressions;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch _match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color) {
            Name = "Pawn";
            _match = match;
        }

        public override bool[,] PossibleMoves() {
            bool[,] moves = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            int sign = color == Color.White ? -1 : 1;

            pos.SetValues(position.Row + 1 * sign, position.Column);
            if (board.IsPositionValid(pos) && PositionIsFree(pos)) moves[pos.Row, pos.Column] = true;

            if(moveCount < 1) {
                pos.SetValues(position.Row + 2 * sign, position.Column);
                if (board.IsPositionValid(pos) && PositionIsFree(pos)) moves[pos.Row, pos.Column] = true;
            }

            pos.SetValues(position.Row + 1 * sign, position.Column + 1);
            if (board.IsPositionValid(pos) && EnemyExists(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row + 1 * sign, position.Column - 1);
            if (board.IsPositionValid(pos) && EnemyExists(pos)) moves[pos.Row, pos.Column] = true;

            //#SpecialMove EnPassant
            if(color == Color.White && position.Row == 3) {
                SetEnPassant(ref moves, sign);
            }

            if (color == Color.Black && position.Row == 4) {
                SetEnPassant(ref moves, sign);
            }

            return moves;
        }

        private void SetEnPassant(ref bool[,] moves, int sign) {
            Position left = new Position(position.Row, position.Column - 1);
            if (board.IsPositionValid(left) && EnemyExists(left) && board.GetPiece(left) == _match.enPassantVulnerable) {
                moves[left.Row + 1 * sign, left.Column] = true;
            }

            Position right = new Position(position.Row, position.Column + 1);
            if (board.IsPositionValid(right) && EnemyExists(right) && board.GetPiece(right) == _match.enPassantVulnerable) {
                moves[right.Row + 1 * sign, right.Column] = true;
            }
        }

        private bool EnemyExists(Position pos) {
            Piece p = board.GetPiece(pos);
            return p != null && p.color != color;
        }

        private bool PositionIsFree(Position pos) => board.GetPiece(pos) == null;

        public override string ToString() {
            return "P";
        }
    }
}
