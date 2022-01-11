using System;
using System.Collections.Generic;
using Chess;

namespace ChessBoard
{
    class Screen
    {
        public static void PrintBoard(Board board) {
            Console.Clear();
            for (int i = 0; i < board.Rows; i++) {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++) {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,]possibleMoves) {
            Console.Clear();
            for (int i = 0; i < board.Rows; i++) {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++) {
                    Console.BackgroundColor = possibleMoves[i, j] == true ? ConsoleColor.DarkGray : ConsoleColor.Black;

                    PrintPiece(board.GetPiece(i, j));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadPositionInput() {
            string input = Console.ReadLine();
            return new ChessPosition(input[0], int.Parse($"{input[1]} ")); //Workaround since int.Parse(input[1]) is invalid
        }

        public static void PrintPiece(Piece piece) {
            if (piece == null) {
                Console.Write($"- ");
                return;
            }

            ConsoleColor foreColor = Console.ForegroundColor;
            ConsoleColor backColor = Console.BackgroundColor;

            switch (piece.color) {
                case Color.Black:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Pink:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }

            Console.Write($"{piece}");

            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.Write($" ");
        }
    }
}
