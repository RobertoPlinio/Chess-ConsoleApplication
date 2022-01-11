using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class ChessPosition
    {
        public char Column{ get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row) {
            Column = column;
            Row = row;
        }

        public Position ToPosition() => new Position(8 - Row, Column - 'a');

        public override string ToString() {
            return $"{Column}{Row}";
        }
    }
}
