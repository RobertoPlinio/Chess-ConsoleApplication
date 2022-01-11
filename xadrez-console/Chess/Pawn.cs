using ChessBoard;

namespace Chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color) {
            Name = "Pawn";
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

            return moves;
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
