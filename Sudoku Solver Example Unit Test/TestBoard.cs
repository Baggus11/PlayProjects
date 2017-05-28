using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Common.Extensions;

namespace TestSudokuEngine
{
    [TestClass]
    public class TestBoard
    {
        [TestMethod]
        public void Test_Game_01()
        {
            Board board = new Board();
            board.SetSquareValue(1, 3, 4);
            board.SetSquareValue(1, 5, 5);
            board.SetSquareValue(1, 9, 2);
            board.SetSquareValue(2, 1, 1);
            board.SetSquareValue(2, 4, 2);
            board.SetSquareValue(3, 1, 7);
            board.SetSquareValue(3, 3, 5);
            board.SetSquareValue(3, 4, 1);
            board.SetSquareValue(3, 6, 8);
            board.SetSquareValue(3, 7, 9);
            board.SetSquareValue(4, 1, 3);
            board.SetSquareValue(4, 2, 5);
            board.SetSquareValue(4, 3, 2);
            board.SetSquareValue(4, 6, 1);
            board.SetSquareValue(4, 7, 7);
            board.SetSquareValue(4, 8, 8);
            board.SetSquareValue(5, 2, 6);
            board.SetSquareValue(5, 5, 7);
            board.SetSquareValue(5, 8, 5);
            board.SetSquareValue(6, 2, 8);
            board.SetSquareValue(6, 3, 7);
            board.SetSquareValue(6, 4, 6);
            board.SetSquareValue(6, 7, 4);
            board.SetSquareValue(6, 8, 3);
            board.SetSquareValue(6, 9, 1);
            board.SetSquareValue(7, 3, 6);
            board.SetSquareValue(7, 4, 3);
            board.SetSquareValue(7, 6, 7);
            board.SetSquareValue(7, 7, 5);
            board.SetSquareValue(7, 9, 8);
            board.SetSquareValue(8, 6, 2);
            board.SetSquareValue(8, 9, 4);
            board.SetSquareValue(9, 1, 8);
            board.SetSquareValue(9, 5, 1);
            board.SetSquareValue(9, 7, 3);

            //Debug.WriteLine(board.Validate());
            Assert.AreEqual(0, board.Squares.Count(x => !x.IsSolved));

            ////Shuffle the entire grid!
            //board.Squares.Shuffle(2).Dump();
            //board.Squares = board.Squares.Shuffle().ToList();
            Debug.WriteLine(board.ToString());
            //Debug.WriteLine(board.Validate());

        }
        public class Board
        {
            public List<Square> Squares { get; private set; }
            public Board()
            {
                Squares = new List<Square>();
                for (int row = 1; row < 10; row++)
                {
                    for (int column = 1; column < 10; column++)
                    {
                        Squares.Add(new Square(row, column));
                    }
                }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    sb.AppendLine("+----------+");
                    for (int r = 0; r < 9; r++)
                    {
                        sb.Append("||");
                        for (int c = 0; c < 9; c++)
                        {
                            sb.Append($"  {Squares[r + c].Value}  ");
                        }
                        sb.Append("||\n");
                    }
                    sb.AppendLine("+----------+");
                }
                catch (Exception ex)
                {
                    string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                    Debug.WriteLine(errMsg);
                    sb.ToString().Dump("contents");
                }
                return sb.ToString();
            }
            public void SetSquareValue(int row, int column, int value)
            {
                Square activeSquare = Squares.Single(x => (x.Row == row) && (x.Column == column));
                activeSquare.Value = value;
                // Remove value from other squares in the same row
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Row == row)))
                {
                    square.PotentialValues.Remove(value);
                }
                // Remove value from other squares in the same column
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Column == column)))
                {
                    square.PotentialValues.Remove(value);
                }
                // Remove value from other squares in the same quadrant
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Block == activeSquare.Block)))
                {
                    square.PotentialValues.Remove(value);
                }
                // Set the Value for any square that only have one remaining PotentialValue
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.PotentialValues.Count == 1)))
                {
                    SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);
                }
            }
        }
        public class Square
        {
            private readonly List<int> _potentialValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            internal enum Blocks
            {
                UpperLeft,
                UpperMiddle,
                UpperRight,
                MiddleLeft,
                Middle,
                MiddleRight,
                LowerLeft,
                LowerMiddle,
                LowerRight
            }
            public int Row { get; private set; }
            public int Column { get; private set; }
            internal Blocks Block
            {
                get
                {
                    if (Row < 4)
                    {
                        if (Column < 4)
                        {
                            return Blocks.UpperLeft;
                        }
                        return Column < 7 ? Blocks.UpperMiddle : Blocks.UpperRight;
                    }
                    if (Row < 7)
                    {
                        if (Column < 4)
                        {
                            return Blocks.MiddleLeft;
                        }
                        return Column < 7 ? Blocks.Middle : Blocks.MiddleRight;
                    }
                    if (Column < 4)
                    {
                        return Blocks.LowerLeft;
                    }
                    return Column < 7 ? Blocks.LowerMiddle : Blocks.LowerRight;
                }
            }
            public bool IsSolved { get { return Value != null; } }
            public int? Value { get; set; }
            internal List<int> PotentialValues { get; private set; }
            internal Square(int row, int column)
            {
                Row = row;
                Column = column;
                PotentialValues = _potentialValues;
            }
        }
    }
}
