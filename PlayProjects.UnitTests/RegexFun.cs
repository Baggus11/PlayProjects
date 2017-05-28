using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace PlayProjects.UnitTests
{
    [TestClass]
    public class RegexFun
    {
        [TestMethod]
        ///From: http://www.rexegg.com/regex-best-trick.html
        public void ExclusionTrickTest()
        {
            //string[] patterns = new string[] {
            //    "",
            //};
            //string[] inputs = new string[] { "", };
            //foreach (string rgx in patterns)
            //{
            //    Debug.WriteLine($"Pattern: {rgx}");
            //    foreach (string input in inputs)
            //    {
            //        Debug.WriteLine($"\tInput: {input}");
            //        Match match = Regex.Match(input, rgx);
            //        for (int i = 0; i < match.Groups.Count; i++)
            //        {
            //            Debug.WriteLine($"\t\tGroup {i}: {match.Groups[i].Value}");
            //        }
            //        Debug.WriteLine("");
            //    }
            //}
            string s1 = @"Jane"" ""Tarzan12"" Tarzan11@Tarzan22 {4 Tarzan34}";
            var myRegex = new Regex(@"{[^}]+}|""Tarzan\d+""|(Tarzan\d+)");
            var group1Caps = new List<string>();
            Match matchResult = myRegex.Match(s1);
            // put Group 1 captures in a list
            while (matchResult.Success)
            {
                if (matchResult.Groups[1].Value != "")
                {
                    Debug.WriteLine(matchResult.Groups[1].Value);
                    group1Caps.Add(matchResult.Groups[1].Value);
                }
                matchResult = matchResult.NextMatch();
            }
            ///////// The six main tasks we're likely to have ////////
            // Task 1: Is there a match?
            Debug.WriteLine("*** Is there a Match? ***");
            if (group1Caps.Any()) Debug.WriteLine("Yes");
            else Debug.WriteLine("No");
            // Task 2: How many matches are there?
            Debug.WriteLine("\n" + "*** Number of Matches ***");
            Debug.WriteLine(group1Caps.Count);
            // Task 3: What is the first match?
            Debug.WriteLine("\n" + "*** First Match ***");
            if (group1Caps.Any()) Debug.WriteLine(group1Caps[0]);
            // Task 4: What are all the matches?
            Debug.WriteLine("\n" + "*** Matches ***");
            if (group1Caps.Any())
            {
                foreach (string match in group1Caps) Debug.WriteLine(match);
            }
            // Task 5: Replace the matches
            string replaced = myRegex.Replace(s1, delegate (Match m)
            {
                // m.Value is the same as m.Groups[0].Value
                if (m.Groups[1].Value == "") return m.Value;
                else return "Superman";
            });
            Debug.WriteLine("\n" + "*** Replacements ***");
            Debug.WriteLine(replaced);
            // Task 6: Split
            // Start by replacing by something distinctive,
            // as in Step 5. Then split.
            string[] splits = Regex.Split(replaced, "Superman");
            Debug.WriteLine("\n" + "*** Splits ***");
            foreach (string split in splits) Debug.WriteLine(split);
        }
    }
}
