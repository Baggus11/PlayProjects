using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGamesAPI.Algorithms.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        private int[] vertices;
        private Tuple<int, int>[] edges;
        private Graph<int> graph;
        private IAlgorithm<int> algorithm;

        [TestInitialize]
        public void Init()
        {
            vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5), Tuple.Create(3,6),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(5,6), Tuple.Create(8,9), Tuple.Create(9,10)};

            graph = new Graph<int>(vertices, edges);
        }

        [TestMethod()]
        public void DFSSearchTest()
        {
            algorithm = new DFSAlgorithm<int>(graph);
            Debug.WriteLine(string.Join(", ", algorithm.Search(1)));
        }

        [TestMethod]
        public void DFSSearchAndActionTest()
        {
            var path = new List<int>();
            algorithm = new DFSAlgorithm<int>(graph);

            Debug.WriteLine(string.Join(", ", algorithm.Search(1)));
            Debug.WriteLine(string.Join(", ", path));
        }

        [TestMethod]
        public void BFSSearchTest()
        {
            algorithm = new BFSAlgorithm<int>(graph);
            Debug.WriteLine(string.Join(", ", algorithm.Search(1)));
        }

    }
}