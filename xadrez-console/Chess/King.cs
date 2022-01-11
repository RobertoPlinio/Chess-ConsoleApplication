using ChessBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch _match;

        public King(Board board, Color color, ChessMatch match) : base(board, color) {
            Name = "King";
            _match = match;
        }

        public override bool[,] PossibleMoves() {
            bool[,] moves = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            // N
            pos.SetValues(position.Row - 1, position.Column);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // NE
            pos.SetValues(position.Row - 1, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // E
            pos.SetValues(position.Row, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // SE
            pos.SetValues(position.Row + 1, position.Column + 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // S
            pos.SetValues(position.Row + 1, position.Column);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // SW
            pos.SetValues(position.Row + 1, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // W
            pos.SetValues(position.Row, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            // NW
            pos.SetValues(position.Row - 1, position.Column - 1);
            if (board.IsPositionValid(pos) && CanMoveToPos(pos)) moves[pos.Row, pos.Column] = true;

            //#SpecialMove Castle
            if(moveCount < 1 && !_match.check) {
                //King side
                Position R1 = new Position(position.Row, position.Column + 3);
                if(RookCastleCheck(R1)) {
                    Position p1 = new Position(position.Row, position.Column + 1);
                    Position p2 = new Position(position.Row, position.Column + 2);
                    if(board.GetPiece(p1) == null && board.GetPiece(p2) == null) {
                        moves[position.Row, position.Column + 2] = true;
                    }
                }

                //Queen side
                Position R2 = new Position(position.Row, position.Column - 4);
                if (RookCastleCheck(R1)) {
                    Position p1 = new Position(position.Row, position.Column - 1);
                    Position p2 = new Position(position.Row, position.Column - 2);
                    Position p3 = new Position(position.Row, position.Column - 3);
                    if (board.GetPiece(p1) == null && board.GetPiece(p2) == null && board.GetPiece(p3) == null) {
                        moves[position.Row, position.Column - 2]= true;
                    }
                }
            }

            return moves;
        }

        private bool RookCastleCheck(Position pos) {
            Piece p = board.GetPiece(pos);
            return p != null && p.color == color && p is Rook && p.moveCount < 1;
        }

        public override string ToString() {
            return "K";
        }
    }
}
