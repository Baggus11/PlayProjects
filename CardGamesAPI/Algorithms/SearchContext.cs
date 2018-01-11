using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<T> Search(T start) => start != null ? Algorithm.Search(start) : Enumerable.Empty<T>();

        public void SwitchAlgorithm(IAlgorithm<T> nextAlgorithm) => Algorithm = nextAlgorithm;

    }
}
