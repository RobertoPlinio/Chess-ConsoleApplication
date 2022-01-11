using ChessBoard;

namespace Chess
{
    class Rook : Piece
    {

        public Rook(Board board, Color color) : base(board, color) {
            Name = "Rook";
        }

        public override bool[,] PossibleMoves() {
            bool[,] moves = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            // N
            pos.SetValues(position.Row - 1, position.Column);
            while(board.IsPositionValid(pos) && CanMoveToPos(pos)) {
                moves[pos.Row, pos.Column] = true;
                Piece p = board.GetPiece(pos);
                if (p != null && p.color != color) break;
                pos.Row -= 1;
            }

            // E
            pos.SetValues(position.Row, position.Column + 1);
            while (board.IsPositionValid(pos) && CanMoveToPos(pos)) {
                moves[pos.Row, pos.Column] = true;
                Piece p = board.GetPiece(pos);
                if (p != null && p.color != color) break;
                pos.Column += 1;
            }

            // S
            pos.SetValues(position.Row + 1, position.Column);
            while (board.IsPositionValid(pos) && CanMoveToPos(pos)) {
                moves[pos.Row, pos.Column] = true;
                Piece p = board.GetPiece(pos);
                if (p != null && p.color != color) break;
                pos.Row += 1;
            }

            // W
            pos.SetValues(position.Row, position.Column - 1);
            while (board.IsPositionValid(pos) && CanMoveToPos(pos)) {
                moves[pos.Row, pos.Column] = true;
                Piece p = board.GetPiece(pos);
                if (p != null && p.color != color) break;
                pos.Column -= 1;
            }

            return moves;
        }

        public override string ToString() {
            return "R";
        }
    }
}
