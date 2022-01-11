using ChessBoard;

namespace Chess
{
    class Bishop : Piece
    {

        public Bishop(Board board, Color color) : base(board, color) { }

        public override bool[,] PossibleMoves() {
            throw new System.NotImplementedException();
        }

        public override string ToString() {
            return "B";
        }
    }
}
