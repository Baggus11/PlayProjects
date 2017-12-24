using System.Collections.Generic;

namespace CardGamesAPI.Algorithms
{
    public class BFSAlgorithm<T> : Algorithm<T>
    {
        private Graph<T> _graph;
        public BFSAlgorithm(Graph<T> graph)
        {
            _graph = graph;
        }

        public override IEnumerable<T> Search(T start)
        {
            var visited = new HashSet<T>();

            if (!_graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);
                foreach (var neighbor in _graph.AdjacencyList[vertex])
                {
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
                }
            }

            return visited;
        }

    }
}