using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
/// <summary>
/// From - http://www.codeproject.com/Articles/11983/Sudoku-Solver-and-Generator
/// </summary>
namespace PlayProjects.UnitTests
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void Test_Game_01()
        {
            try
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
                board.SetSquareValue(9, 7, 3, true);
                Debug.WriteLine(board.Validate());
                Assert.AreEqual(0, board.Squares.Count(x => !x.IsSolved));
                //bugs for fun! Change a random square:
                //var squareA = board.Squares.TakeFirstRandom();
                //Debug.WriteLine(board.ToString());
                //Debug.WriteLine(board.Validate());
                ////Shuffle the entire grid!
                //board.Squares.Shuffle(2).Dump();
                board.Squares = board.Squares.Shuffle().ToList();
                Debug.WriteLine(board.ToString());
                Debug.WriteLine(board.Validate());
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Assert.Fail(errMsg);
            }
        }
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        Sudoku sudoku = new Sudoku();
        [TestMethod()]
        public void IsSudokuUniqueTest()
        {
            sudoku.Data = new byte[,]{
                {1,7,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0},
                {0,0,0,0,0,0,6,8,0},
                {8,0,0,0,0,5,4,0,0},
                {0,0,0,0,0,0,0,6,0},
                {3,5,0,6,0,7,1,0,0},
                {0,0,2,1,0,0,3,0,4},
                {5,0,3,0,0,0,0,0,8},
                {0,0,0,0,9,4,0,0,0}
            };
            var actual = sudoku.IsSudokuUnique();
            Assert.AreEqual(true, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void IsSudokuNotUniqueTest()
        {
            sudoku.Data = new byte[,]{
                {0,0,0,0,0,0,0,9,0},
                {0,0,0,0,9,0,4,6,7},
                {9,0,4,1,0,0,0,2,5},
                {0,0,9,0,0,0,0,0,8},
                {8,7,0,0,6,0,0,1,4},
                {6,0,0,6,0,0,3,0,0},
                {7,4,0,0,0,2,9,0,1},
                {2,9,8,0,3,0,0,0,0},
                {0,1,0,0,0,0,0,0,0}
            };
            var actual = sudoku.IsSudokuUnique();
            Assert.AreEqual(false, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void SolveSuccessTest()
        {
            sudoku.Data = new byte[,]{
                {1,7,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0},
                {0,0,0,0,0,0,6,8,0},
                {8,0,0,0,0,5,4,0,0},
                {0,0,0,0,0,0,0,6,0},
                {3,5,0,6,0,7,1,0,0},
                {0,0,2,1,0,0,3,0,4},
                {5,0,3,0,0,0,0,0,8},
                {0,0,0,0,9,4,0,0,0}
            };
            Debug.WriteLine(sudoku.ToString());
            var actual = sudoku.Solve();
            Assert.AreEqual(true, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void SolveNoSuccessTest()
        {
            sudoku.Data = new byte[,]{
                {1,7,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0},
                {0,0,0,0,0,0,6,8,0},
                {8,0,0,0,0,5,4,0,0},
                {0,0,0,0,0,0,0,6,0},
                {3,5,0,6,0,7,1,0,0},
                {0,0,2,1,0,0,3,0,4},
                {5,0,3,0,0,0,0,0,8},
                {0,0,0,0,9,4,0,0,7}
            };
            Debug.WriteLine(sudoku.ToString());
            var actual = sudoku.Solve();
            Assert.AreEqual(false, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void IsSudokuFeasibleTest()
        {
            sudoku.Data = new byte[,]{
                {1,7,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0},
                {0,0,0,0,0,0,6,8,0},
                {8,0,0,0,0,5,4,0,0},
                {0,0,0,0,0,0,0,6,0},
                {3,5,0,6,0,7,1,0,0},
                {0,0,2,1,0,0,3,0,4},
                {5,0,3,0,0,0,0,0,8},
                {0,0,0,0,9,4,0,0,7}
            };
            var actual = sudoku.IsSudokuFeasible();
            Assert.AreEqual(true, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void IsSudokuNotFeasibleTest()
        {
            sudoku.Data = new byte[,]{
                {1,7,0,0,0,0,0,0,0},
                {0,0,0,2,0,0,0,0,0},
                {0,0,0,0,0,0,6,8,0},
                {8,0,0,0,0,5,4,0,0},
                {0,0,0,0,0,0,0,6,0},
                {3,5,0,6,0,7,1,0,0},
                {0,0,2,1,0,0,3,0,4},
                {5,0,3,0,0,0,0,0,8},
                {0,0,0,0,9,4,4,0,7}
            };
            var actual = sudoku.IsSudokuFeasible();
            Assert.AreEqual(false, actual);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void GenerateTest()
        {
            sudoku.Data = new byte[,]{
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0}
            };
            bool actual = sudoku.Generate(30).Item2;
            Debug.WriteLine(sudoku.ToString());
            bool feasible = sudoku.IsSudokuFeasible();
            bool unique = sudoku.IsSudokuUnique();
            bool solve = sudoku.Solve();
            Assert.AreEqual(true, actual && feasible && unique && solve);
            Debug.WriteLine(sudoku.ToString());
        }
        [TestMethod()]
        public void GenerateTestFromTemplate()
        {
            sudoku.Data = new byte[,]{
                {1,0,0,0,0,0,0,0,2},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,9,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {2,0,0,0,0,0,0,0,1}
            };
            sudoku.Randomizer = new Deterministic(new int[]
                {5,7,6,0,3,6,4,3,2,2,0,4,2,6,1,5,4,3,0,1,9,3,2,3,6,3,1,2,1,2,4,5,5,6,6,7,2,7,7,1,3,4,3,0,5,7,8,5,2,8,8,1,0,3,7,7,4,7,1,6,6,8,9,0,7,3,3,8,4,8,7,8,4,7,1}
            );
            var actual = sudoku.Generate(30);
            Debug.WriteLine(sudoku.ToString());
            bool chkTemplateConserved = sudoku.Data[0, 0] == 1 && sudoku.Data[8, 0] == 2 && sudoku.Data[0, 8] == 2 && sudoku.Data[8, 8] == 1 && sudoku.Data[4, 4] == 9;
            Assert.IsTrue(chkTemplateConserved, "Template not conserved.");
        }
    }
    internal class Deterministic : IRandomizer
    {
        int[] DeterministicArray
        {
            get;
            set;
        }
        int current = 0;
        public Deterministic(int[] deterministicArray)
        {
            DeterministicArray = deterministicArray;
        }
        public int GetInt(int max)
        {
            return GetNext();
        }
        public int GetInt(int min, int max)
        {
            return GetNext();
        }
        private int GetNext()
        {
            return DeterministicArray[current++];
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
    public class Board
    {
        public List<Square> Squares { get; set; }
        private readonly char[,] board = new char[9, 9];
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
            Debug.WriteLine($"squares: {Squares.Count}");
        }
        /// <summary>
        /// Basically the Solve method, setting square values by excluding all potentials
        /// until only 1 remains.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetSquareValue(int row, int column, int value, bool verbose = false)
        {
            Square active_square = Squares.Single(s => (s.Row == row) && (s.Column == column)); //get the only Square instance at given row/column coordinate.
            active_square.Value = value; //this is a removeable value from the list of potential values.
                                         //if (active_square?.Value != null)
                                         //{
            if (verbose) Debug.WriteLine(ToString());
            try
            {
                //Remove all potential values from other squares within the same row number:
                foreach (Square square in Squares.Where(s => !s.IsSolved && s.Row == row))
                    square.PotentialValues.Remove(value);
                //Remove all potential values from other squares within the same column number:
                foreach (Square square in Squares.Where(s => !s.IsSolved && s.Column == column))
                    square.PotentialValues.Remove(value);
                //Remove all potential values from other squares within the same quadrant:
                foreach (Square square in Squares.Where(s => !s.IsSolved && s.Block == active_square.Block))
                    square.PotentialValues.Remove(value);
                //Set the Value for any square that only has ONE remaining PotentialValue:
                foreach (Square square in Squares.Where(s => !s.IsSolved && s.PotentialValues.Count == 1))
                {
                    SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);
                }
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Debug.WriteLine(errMsg);
            }
            //}
        }
        public bool Validate()
        {
            var rows = Enumerable.Range(1, 9).Select(i => new HashSet<char>()).ToArray();
            var columns = Enumerable.Range(1, 9).Select(i => new HashSet<char>()).ToArray();
            var cubes = Enumerable.Range(1, 9).Select(i => new HashSet<char>()).ToArray();
            //Extra step: convert the Squares to a board array:
            char val;
            for (int row = 0; row < 9; ++row)
            {
                for (int column = 0; column < 9; ++column)
                {
                    val = (char)Squares[row + column].Value;
                    board[row, column] = val;
                    Debug.Write($"{(int)val} ");
                }
                Debug.WriteLine("");
            }
            //board.Dump();
            // process each cell only once
            for (int row = 0; row < 9; ++row)
            {
                for (int column = 0; column < 9; ++column)
                {
                    var current = board[row, column];
                    if (char.IsDigit(current))
                    {
                        // determine which of the "cubes" the row/col fall in
                        var cube = 3 * (row / 3) + (column / 3);
                        // if add to any set returns false, it was already there.
                        if (!rows[row].Add(current) || !columns[column].Add(current) || !cubes[cube].Add(current))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine($"+{new string('-', 36)}+");
                for (int r = 0; r < 9; r++)
                {
                    sb.Append("||");
                    for (int c = 0; c < 9; c++)
                    {
                        sb.Append($"  {Squares[r + c].Value}  ");
                    }
                    sb.Append("||\n");
                }
                sb.AppendLine($"+{new string('-', 36)}+");
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Debug.WriteLine(errMsg);
                sb.ToString().Dump("contents");
            }
            return sb.ToString();
        }
    }
    public static class Exts
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list/*, int size*/)
        {
            var rand = new Random();
            var shuffledList =
                list.
                    Select(x => new { Number = rand.Next(), Item = x }).
                    OrderBy(x => x.Number).
                    Select(x => x.Item);
            //Take(size); // Assume first @size items is fine
            //list = shuffledList;
            return shuffledList;
        }
    }
}
