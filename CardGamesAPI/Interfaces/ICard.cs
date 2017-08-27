using System;

namespace CardGames
{

    public interface ICard : IDisposable
    {
        //double Height { get; set; }
        //double Width { get; set; }

        void Set();

        void Flip();

    }
}
