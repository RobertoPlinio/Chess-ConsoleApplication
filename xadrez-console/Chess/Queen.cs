using ChessBoard;

namespace Chess
{
    class Queen : Piece
    {

        public Queen(Board board, Color color) : base(board, color) {
            Name = "Queen";
        }

        public override bool[,] PossibleMoves() {
            throw new System.NotImplementedException();
        }

        public override string ToString() {
            return "Q";
        }
    }
}
