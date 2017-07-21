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

        public bool Downloaded
        {
            get { return GetValue(() => Downloaded); }
            set { SetValue(() => Downloaded, value); }
        }

        public bool ImageOnDisk
        {
            get { return GetValue(() => ImageOnDisk); }
            set { SetValue(() => ImageOnDisk, value); }
        }

        private int _Level;
        public int Level
        {
            get { return _Level; }
            set { SetProperty(ref _Level, value); }
        }

        private string _CardText;
        public string CardText
        {
            get { return _CardText; }
            set { SetProperty(ref _CardText, value); }
        }

        private int _Rank;
        public int Rank
        {
            get { return _Rank; }
            set { SetProperty(ref _Rank, value); }
        }

        public string Type { get; internal set; }

        private string _CardType;
        public string CardType
        {
            get { return _CardType; }
            set { SetProperty(ref _CardType, value); }
        }

        private int _Attack;
        public int Attack
        {
            get { return _Attack; }
            set { SetProperty(ref _Attack, value); }
        }

        public string Attribute
        {
            get { return GetValue(() => Attribute); }
            set { SetValue(() => Attribute, value); }
        }

        private int _Defense;
        public int Defense
        {
            get { return _Defense; }
            set { SetProperty(ref _Defense, value); }
        }

    }
}
