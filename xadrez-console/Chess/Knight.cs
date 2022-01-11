using ChessBoard;

namespace Chess
{
    class Knight : Piece
    {

        public Knight(Board board, Color color) : base(board, color) {
            Name = "Knight";
        }

        public override bool[,] PossibleMoves() {
            bool[,] moves = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            pos.SetValues(position.Row + 1, position.Column + 2);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row + 1, position.Column - 2);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;
            
            pos.SetValues(position.Row - 1, position.Column + 2);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row - 1, position.Column - 2);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row + 2, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row + 2, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row - 2, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            pos.SetValues(position.Row - 2, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            return moves;
        }

        public override string ToString() {
            return "N";
        }
    }
}
