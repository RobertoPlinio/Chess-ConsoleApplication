using System;
using Chess;
using ChessBoard;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args) {
            try {
                ChessMatch match = new ChessMatch();

                while (!match.Finished) {
                    Screen.PrintBoard(match.board);

                    Console.Write("Origin: ");
                    ChessPosition origin = Screen.ReadPositionInput();

                    bool[,] moves = match.board.GetPiece(origin.ToPosition()).PossibleMoves();
                    Screen.PrintBoard(match.board, moves);

                    Console.Write("\nDestination: ");
                    ChessPosition destination = Screen.ReadPositionInput();

                    match.ExecuteMove(origin.ToPosition(), destination.ToPosition());
                }
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
