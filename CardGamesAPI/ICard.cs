using System;
namespace CardGames
{
    /// <summary>
    /// Simple contract for all cards of all types
    /// </summary>
    public interface ICard : IDisposable
    {
        //double Height { get; set; }
        //double Width { get; set; }
        void Set();
        void Flip();
    }
}
