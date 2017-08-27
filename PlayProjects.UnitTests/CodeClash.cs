using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlayProjects.UnitTests
{
    [TestClass]
    public class CodeClash
    {
        /// <summary>
        /// Old, no notes
        /// </summary>
        [TestMethod]
        public void AsciiAvg()
        {
            string myString = "ToavX";
            int[] asciiArray = myString.Select(r => (int)r).ToArray();
            //int total = 0;
            //foreach (char letter in myString.Split())
            //{
            //    letter = toupper(letter);
            //    total += (int)letter;
            //}
            Debug.WriteLine((char)(asciiArray.Sum() / asciiArray.Length));
        }
        /// <summary>
        /// A male ostrich weighs 20% more than a female one.
        ///Given the gender of an ostrich, and its weight, display the weight of its counterpart.
        ///If the gender is unknown, you should display "UNKNOWN".
        /// </summary>
        [TestMethod]
        public void Ostrich()
        {
            string G = "M";
            int W = 120;
            double cW = W;
            switch (G.ToUpper())
            {
                case "F":
                    cW = 1.2 * W;
                    Debug.WriteLine(cW);
                    break;
                case "M":
                    cW = W / 1.2;
                    Debug.WriteLine(cW);
                    break;
                default:
                    Debug.WriteLine("UNKNOWN");
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        //[TestMethod]
        //public void Letters()
        //{
        //    string W = "wrdsW wW123!";
        //    //int letters = W.ToCharArray().Distinct().Dump().Count(char.IsLetter);
        //    int letters = W.ToCharArray().Where(char.IsLetter).Distinct().Dump().ToList().Count;
        //    Debug.WriteLine(letters);
        //}
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TurtleGum()
        {
            //copy 'inputs' here
            //string source = "sayonara";
            //string target = "serenita";
            string source = "aren't you worried?";
            string target = "prepare our troops?";
            char[] s = source.ToCharArray();
            char[] t = target.ToCharArray();
            Debug.WriteLine(new string(s));
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] != t[i])
                {
                    s[i] = t[i];
                    Debug.WriteLine(new string(s));
                }
            }
        }
        [TestMethod]
        public void ReverseTest2()
        {
            List<int> list = new List<int> {
                1,1,3,1,3,2,3
            };
            foreach (var item in list.Distinct())
            {
                Debug.WriteLine(item);
            }
            //for (int i = 0; i < N; i++)
            //{
            //    int X = int.Parse(Console.ReadLine());
            //    list.Add(X);
            //}
        }
        [TestMethod]
        public void DropBall()
        {
            int h = 5;
            int I = h;
            for (int i = 0; i < 6; i++)
            {
                I *= 2;
            }
            Debug.WriteLine(I);
            //Debug.WriteLine(Math.Pow(2, 5));
        }
        [TestMethod]
        public void Diamond()
        {
            int N = 4;
            for (int i = 0; i < N; i++)
                Debug.Write("#");
            Debug.WriteLine("");
            for (int i = N; i >= 1; i -= 2)
            {
                string spaces = new String(' ', (N + 1 - i) / 2);
                Console.WriteLine(spaces + new String('#', i) + spaces);
            }
        }
        [TestMethod]
        public void SumNatural()
        {
            int N = 4;
            int sum = Enumerable.Range(1, N).Dump().Sum(x => 2 * x);
            Debug.WriteLine(sum);
        }
        [TestMethod]
        public void TempsClosestToZero()
        {
            string temps = "-12 -5 -137";
            //string temps = "-10 10";
            int[] array = temps.Split(' ').Select(int.Parse).ToArray();
            int min_abs_value = array.Min(Math.Abs);
            int[] mins = array.Where(x => Math.Abs(x) == min_abs_value).ToArray().Dump("minimums");
            int min;
            if (mins.Distinct().ToList().Count > 1)
                min = Math.Abs(mins.First());
            else min = mins.First();
            Debug.WriteLine(min);
            //top rated answer:
            //
            //var result = temps.Split(' ').Select(int.Parse)
            //    .OrderBy(Math.Abs)
            //    .ThenByDescending(x => x)
            //    .FirstOrDefault();
            //result.Dump("top rated result");
            ////
        }
        [TestMethod]
        public void ThereIsNoSpoon()
        {
            ////Init your grid from input
            //for (int i = 0; i < height; i++)
            //{
            //    string line = Console.ReadLine(); // width characters, each either 0 or .
            //    Console.Error.WriteLine($"line {i}: {line}");
            //    int col = 0;
            //    foreach (char ch in line)
            //    {
            //        grid[row, col] = ch;
            //        col++;
            //    }
            //}
            int width = 2, height = 2;
            char[,] grid = new char[width, height];
            string input = "00\n0.";
            var lines = input.Split(new[] { '\n' });
            int row = 0;
            //Init:
            foreach (var line in lines)
            {
                int col = 0;
                foreach (char character in line)
                {
                    grid[row, col] = character;
                    col++;
                }
                row++;
            }
            grid.Dump("grid");
            char xyVal = '.';
            //int xRight, yRight, xBottom, yBottom;
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    xyVal = grid[r, c];
                    //If blank, continue
                    if (xyVal == '.')
                    {
                        Debug.WriteLine("blank node");
                        continue;
                    }
                    //else find neighbors:
                    else if (grid[r, c + 1] == '0')
                        Debug.WriteLine($"{r} {c}");
                }
            }
        }
        [TestMethod]
        public void Test3()
        {
            var value = "";
            Debug.WriteLine(value);
        }
        [TestMethod]
        public void Test4()
        {
            var value = "";
            Debug.WriteLine(value);
        }
        [TestMethod]
        public void WordsAndLines()
        {
            string[] lines = new string[]
            {
                "I pledge allegiance to the Flag of the United States of America,",
                "   and to the Republic for which it stands, one Nation under God,",
                "indivisible, with liberty and justice for all, should be rendered",
                "by standing at attention facing the flag with the right hand over",
                "the heart.When not in uniform men should remove any non - religious",
                "headdress with their right hand and hold it at the left shoulder,",
                "the hand being over the heart.Persons in uniform should remain",
                "silent, face the flag, and render the military salute.",
            };
            //int every_X_words = 3;
            //int min_Y_vowels = 2;//or more
            //int every_Z_lines = 2;
            //Finish this in Linq, then port over to python, js, something:
        }
        [TestMethod]
        public void GitHubRegex()
        {
            string input_str = "    Michael Preston         28 2.1 blah junk"; //from a plain text file, xml, json, whatever you can Regex your way out of!
            string regex_pattern = @"\s*(?<Name>[a-zA-Z]+)\s*(?<Age>\d+)";
            Person person_1 = input_str.ExtractObject<Person>(regex_pattern, false);
            person_1.Dump("person object extracted");
        }
    }
    public abstract class Guitar
    {
        public int NumberOfStrings { get; set; }
        private string notepattern = @"[a-fA-F]{1}(#|##|b|bb){0,2}"; //may not be final, but good start.
        public List<string> Frets = new List<string>(); //assumes each fret can be represented by a string.
        public void PlayChord(params string[] notes)
        {
            if (notes.Count() < 2) return; //not a chord
            //play notes;
        }
        public void PlayScale(params string[] notes)
        {
            if (notes.Count(s => s.Length <= 3 && Regex.IsMatch(s, notepattern)) != 8) return; //omit bad characters.
            //play notes in scale;
        }
        public void PlayRoutine1()
        {
            PlayChord("A#", "Cb"); //generic chord
            PlayScale("C", "D", "E", "F", "G", "A", "B", "C"); //Trumpet...not a guitarist
        }
        public bool CompareFrets(Guitar g1, Guitar g2)
        {
            bool result = false;
            //Logic for comparing the sets of frets from guitars g1 and g2
            //compares a set or map of the g1 frets to those of g2 in a pass/fail comparison            
            return result;
        }
    }
    public class SixString : Guitar
    {
        public SixString()
        {
            NumberOfStrings = 6;
        }
    }
    public class FourStringBass : Guitar
    {
        public FourStringBass()
        {
            NumberOfStrings = 4;
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
