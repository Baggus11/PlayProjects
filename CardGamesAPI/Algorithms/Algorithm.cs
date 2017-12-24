using System.Collections.Generic;

namespace CardGamesAPI.Algorithms
{
    public abstract class Algorithm<T> : IAlgorithm<T>
    {
        public abstract IEnumerable<T> Search(T root);
    }
}
