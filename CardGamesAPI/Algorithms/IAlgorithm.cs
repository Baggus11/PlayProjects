using System.Collections.Generic;

namespace CardGamesAPI.Algorithms
{
    public interface IAlgorithm<T>
    {
        IEnumerable<T> Search(T root);
    }
}
