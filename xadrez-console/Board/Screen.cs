using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Screen
    {
        public static void PrintBoard(Board board) {
            for (int i = 0; i < board.Rows; i++) {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++) {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine(" a b c d e f g h");
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
