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

                board.PositionPiece(new Bishop(board, Color.White), new Position(7, 7));
                board.PositionPiece(new Knight(board, Color.Blue), new Position(5, 3));
                board.PositionPiece(new Pawn(board, Color.Red), new Position(1, 6));
                board.PositionPiece(new Pawn(board, Color.Pink), new Position(3, 3));
                board.PositionPiece(new Pawn(board, Color.Yellow), new Position(4, 6));

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
