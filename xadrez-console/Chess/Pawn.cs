using ChessBoard;

namespace Chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color) {
            Name = "Pawn";
        }

        public override bool[,] PossibleMoves() {
            throw new System.NotImplementedException();
        }

        public override string ToString() {
            return "P";
        }
    }
}
