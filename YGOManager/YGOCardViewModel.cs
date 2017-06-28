using CardGamesAPI.Yugioh;
using Common;

namespace YGOManager
{
    //Wrapper & viewmodel
    public class YGOCardViewModel : ViewModelBase
    {
        private YugiohCard _YugiohCard = new YugiohCard();

        public string CardName
        {
            get { return GetValue(() => _YugiohCard.CardName); }
            set { SetValue(() => _YugiohCard.CardName, value); }
        }

        public string LocalPath
        {
            get { return GetValue(() => _YugiohCard.LocalPath); }
            set { SetValue(() => _YugiohCard.LocalPath, value); }
        }

        public string Url
        {
            get { return GetValue(() => _YugiohCard.Url); }
            set { SetValue(() => _YugiohCard.Url, value); }
        }

    }

}
