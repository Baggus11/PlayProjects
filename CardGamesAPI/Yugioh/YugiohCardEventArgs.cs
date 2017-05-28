using System;
namespace CardGamesAPI.Yugioh
{
    public class YugiohCardEventArgs : EventArgs
    {
        protected string Data { get; private set; } //dummy implementation
        YugiohCardEventArgs(string data)
        {
            Data = data;
        }
    }
}
