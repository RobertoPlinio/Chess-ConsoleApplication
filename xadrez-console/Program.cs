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
                    try {
                        Screen.PrintBoard(match.board);

                        Console.WriteLine($"\nTurn: {match.turn}");
                        Console.Write($"Waiting for player ");
                        Screen.TextAccordingToPlayerColor(match.currentPlayer, $"{match.currentPlayer}");
                        Console.WriteLine();

                        Console.Write("\nOrigin: ");
                        ChessPosition origin = Screen.ReadPositionInput();
                        match.ValidateOriginPosition(origin);

                        bool[,] moves = match.board.GetPiece(origin.ToPosition()).PossibleMoves();
                        Screen.PrintBoard(match.board, moves);

                        Console.Write("\nDestination: ");
                        ChessPosition destination = Screen.ReadPositionInput();
                        match.ValidateDestinationPosition(origin, destination);

                        match.Move(origin.ToPosition(), destination.ToPosition());
                    }
                    catch(BoardException be) {
                        Console.WriteLine($"{be.Message}\n\nPress enter to continue");
                        Console.ReadLine();
                    }
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
