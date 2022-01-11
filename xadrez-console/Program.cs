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
                        Screen.PrintMatch(match);

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

                Screen.PrintMatch(match);
            }
            catch(BoardException be) {
                Console.WriteLine(be.Message);
            }
        }
    }
}
