using System;
using System.Collections.Generic;

namespace CardGamesAPI.Algorithms
{
    public class DFSAlgorithm<T> : Algorithm<T>
    {
        private Graph<T> _graph;

        public DFSAlgorithm(Graph<T> graph)
        {
            _graph = graph;
        }

        public IEnumerable<T> Search(T start, Action<T> preVisitAction = null)
        {
            var visited = new HashSet<T>();

            if (!_graph.AdjacencyList.ContainsKey(start))
                return visited;

            var stack = new Stack<T>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var vertex = stack.Pop();

                if (visited.Contains(vertex))
                    continue;

                preVisitAction?.Invoke(vertex);

                visited.Add(vertex);
                PushNeighbors(visited, stack, vertex);
            }

            return visited;
        }

        public override IEnumerable<T> Search(T root)
        {
            return Search(root, null);
        }

        private void PushNeighbors(HashSet<T> visited, Stack<T> stack, T vertex)
        {
            foreach (var neighbor in _graph.AdjacencyList[vertex])
            {
                if (!visited.Contains(neighbor))
                    stack.Push(neighbor);
            }
        }

    }
}



