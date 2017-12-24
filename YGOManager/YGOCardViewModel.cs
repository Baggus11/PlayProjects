using Common;

namespace YugiohCardDownloader
{
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

        public int Level
        {
            get { return GetValue(() => Level); }
            set { SetValue(() => Level, value); }
        }

        public string CardText
        {
            get { return GetValue(() => CardText); }
            set { SetValue(() => CardText, value); }
        }

        public int Rank
        {
            get { return GetValue(() => Rank); }
            set { SetValue(() => Rank, value); }
        }

        public string Type { get; internal set; }

        public string CardType
        {
            get { return GetValue(() => CardType); }
            set { SetValue(() => CardType, value); }
        }

        public int Attack
        {
            get { return GetValue(() => Attack); }
            set { SetValue(() => Attack, value); }
        }

        public string Attribute
        {
            get { return GetValue(() => Attribute); }
            set { SetValue(() => Attribute, value); }
        }

        public int Defense
        {
            get { return GetValue(() => Defense); }
            set { SetValue(() => Defense, value); }
        }

    }
}
