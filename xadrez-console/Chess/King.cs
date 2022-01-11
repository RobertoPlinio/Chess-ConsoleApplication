using ChessBoard;

namespace Chess
{
    class King : Piece
    {

        public King(Board board, Color color) : base(board, color) { }

        public override bool[,] PossibleMoves() {
            bool[,] moves = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            // N
            pos.SetValues(position.Row - 1, position.Column);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // NE
            pos.SetValues(position.Row - 1, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // E
            pos.SetValues(position.Row, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // SE
            pos.SetValues(position.Row + 1, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // S
            pos.SetValues(position.Row + 1, position.Column);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // SW
            pos.SetValues(position.Row + 1, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // W
            pos.SetValues(position.Row, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            // NW
            pos.SetValues(position.Row - 1, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMove(pos)) moves[pos.Row, pos.Column] = true;

            return moves;
        }

        public override string ToString() {
            return "K";
        }
    }
}
