using CardGamesAPI.Yugioh;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace YGOManager
{
    /// <summary>
    /// http://www.yugioh-card.com/uk/gameplay/detail.php?id=1155
    /// </summary>
    public class YGOManagerViewModel : ViewModelBase
    {
        private YugiohWebCrawler _ygosearch;
        private string _CardSaveDirectory;
        private BaseViewableCollection<YGOCardViewModel> _CardList = new BaseViewableCollection<YGOCardViewModel>();
        public BaseObservableCollection<YGOCardViewModel> CardList
        {
            get { return _CardList; }
        }

        public string Text
        {
            get { return GetValue(() => Text); }
            set { SetValue(() => Text, value); }
        }

        private List<string> _downloadList
        {
            get
            {
                return Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct().ToList();
            }
        }

        public YGOManagerViewModel()
            : this(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads"))
        {
        }

        public YGOManagerViewModel(string defaultSaveDir)
        {
            _ygosearch = new YugiohWebCrawler();
            _CardSaveDirectory = defaultSaveDir;
            LoadCardsFromDirectory(_CardSaveDirectory);

        }

        internal void UpdateQueue()
        {
            var existing = CardList.Select(x => x.CardName);

            _downloadList?.FindAll(cardName => !existing.Contains(cardName))
                .ForEach(cardName => CardList.Add(new YGOCardViewModel { CardName = cardName }));

            RunDownloadQueue();//optional
        }

        internal void RunDownloadQueue()
        {
            _ygosearch.DownloadCards(_downloadList.Distinct(), _CardSaveDirectory);
            Text = null;
        }

        public void LoadCardsFromDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory).ToList();
                files?.ForEach(path => CardList.Add(new YGOCardViewModel { LocalPath = path, CardName = Path.GetFileNameWithoutExtension(path) }));
            }
        }

    }


}
