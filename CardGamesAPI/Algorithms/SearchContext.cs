using System.Collections.Generic;

namespace CardGamesAPI.Algorithms
{
    public class SearchContext<T>
        where T : class
    {
        private IAlgorithm<T> Algorithm { get; set; }
        public Graph<T> Graph { get; set; }

        public SearchContext(Graph<T> graph, IAlgorithm<T> algorithm)
        {
            Algorithm = algorithm;
            Graph = graph;
        }

        public IEnumerable<T> Search(T start)
        {
            return Algorithm.Search(start);
        }

        public void SwitchAlgorithm(IAlgorithm<T> nextAlgorithm)
        {
            Algorithm = nextAlgorithm;
        }

    }
}
