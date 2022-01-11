using ChessBoard;

namespace Chess
{
    class Knight : Piece
    {

        public Knight(Board board, Color color) : base(board, color) { }

        public override bool[,] PossibleMoves() {
            throw new System.NotImplementedException();
        }

        public override string ToString() {
            return "N";
        }
    }
}
