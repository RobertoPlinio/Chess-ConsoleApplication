using System;
using Chess;
using ChessBoard;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args) {
            try {
                Board board = new Board(8, 8);

                board.PositionPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.PositionPiece(new Queen(board, Color.Black), new Position(1, 3));
                board.PositionPiece(new King(board, Color.Black), new Position(2,4));

                Screen.PrintBoard(board);
            }
            catch(BoardException be) {
                Console.WriteLine(be.Message);
            }

            Console.WriteLine("\nTesting chess position");
            ChessPosition cp = new ChessPosition('c', 7);
            Console.WriteLine(cp);
            Console.WriteLine(cp.ToPosition());
        }
    }
}
