using System;

namespace CardGamesAPI
{
    public interface ICard : IDisposable
    {
        void Set();
        void Flip();
    }
}
