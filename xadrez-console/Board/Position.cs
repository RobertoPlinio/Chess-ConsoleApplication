using Chess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Position
    {
        public int Row{ get; set; }
        public int Column { get; set; }

        public Position(int row, int column) {
            SetValues(row, column);
        }

        public void SetValues(int row, int column) {
            Row = row;
            Column = column;
        }

        public ChessPosition ToChessPosition() {
            return new ChessPosition(Convert.ToChar(Column + 97), 8-Row);
        }

        public override string ToString() {
            return $"{Row}, {Column}";
        }
    }
}
