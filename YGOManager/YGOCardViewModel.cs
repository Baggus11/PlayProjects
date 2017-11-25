using Common;

namespace YGOManager
{
    //Wrapper & viewmodel
    public class YGOCardViewModel : ViewModelBase
    {
        public string CardName
        {
            get { return GetValue(() => CardName); }
            set { SetValue(() => CardName, value); }
        }

        public string LocalPath
        {
            get { return GetValue(() => LocalPath); }
            set { SetValue(() => LocalPath, value); }
        }

        public string Url
        {
            get { return GetValue(() => Url); }
            set { SetValue(() => Url, value); }
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
            set { SetValue(ref _Level, value); }
        }

        private string _CardText;
        public string CardText
        {
            get { return _CardText; }
            set { SetValue(ref _CardText, value); }
        }

        private int _Rank;
        public int Rank
        {
            get { return _Rank; }
            set { SetValue(ref _Rank, value); }
        }

        public string Type { get; internal set; }

        private string _CardType;
        public string CardType
        {
            get { return _CardType; }
            set { SetValue(ref _CardType, value); }
        }

        private int _Attack;
        public int Attack
        {
            get { return _Attack; }
            set { SetValue(ref _Attack, value); }
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
            set { SetValue(ref _Defense, value); }
        }

    }
}
