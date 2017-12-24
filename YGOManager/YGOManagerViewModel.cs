using CardGamesAPI.Yugioh.YugiohPrices;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using static CardGamesAPI.Yugioh.YugiohPrices.YugiohPricesService;

namespace YugiohCardDownloader
{
    /// <summary>
    /// http://www.yugioh-card.com/uk/gameplay/detail.php?id=1155
    /// </summary>
    public class YGOManagerViewModel : ViewModelBase
    {
        private string _defaultSaveDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads");
        private string _CardSaveDirectory;
        private ViewableCollection<YGOCardViewModel> _CardList = new ViewableCollection<YGOCardViewModel>();
        public ViewableCollection<YGOCardViewModel> CardList { get { return _CardList; } }

        private List<string> _downloadList
        {
            get
            {
                return RequestedCards?
                    .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)?
                    .Distinct().ToList() ?? Enumerable.Empty<string>().ToList();
            }
        }

        public string RequestedCards
        {
            get { return GetValue(() => RequestedCards); }
            set { SetValue(() => RequestedCards, value); }
        }

        public YGOManagerViewModel()
            : this(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads"))
        {
        }

        public YGOManagerViewModel(string defaultSaveDir)
        {
            _CardSaveDirectory = defaultSaveDir;
            LoadCardsFromDirectory(_CardSaveDirectory);
        }

        internal async void SynchronizeCardsList()
        {
            if (_downloadList?.Count == 0)
                return;

            var cardsToDownload = CardList
                .Where(card => _downloadList.Contains(card.CardName) && !card.ImageOnDisk)
                .Select(c => c.CardName);

            await DownloadCards(cardsToDownload, _defaultSaveDir);

            //Check Db tables and local files, make a collection of necessary downloads.
            //CheckLocalFiles();

            //Prompt user for sync if enabled (default).

            RequestedCards = null;
            //Process.Start(_defaultSaveDir, string.Format("/select, \"{0}\"", _defaultSaveDir));
        }

        public async Task DownloadCards(IEnumerable<string> cardNames, string saveDirectory)
        {
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            await Task.Run(() => Parallel.ForEach(cardNames, cardName =>
            {
                try
                {
                    bool success;
                    //use api to get card info, image, etc.
                    string data = GetCardData(cardName, CardDataType.Details).Result;

                    //convert the card json data to a Card
                    //YugiohCardData rawCardData = JsonConvert.DeserializeObject<YugiohCardData>(data,
                    //      new JsonSerializerSettings
                    //      {
                    //          Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() },
                    //          NullValueHandling = NullValueHandling.Ignore,
                    //          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    //      }
                    //    );
                    //MapDataToCard(cardName, rawCardData);

                    //save card, mark as downloaded
                    success = SaveCardToDirectory(cardName, _CardSaveDirectory, ".jpg").Result;

                    //cardName.Dump("Downloaded card - json");

                }
                catch (Exception ex) when (ex.Log())
                {

                    string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                    Debug.WriteLine(errMsg);
                    MessageBox.Show(errMsg);

                }
                //catch (Exception ex) when (ex.Log("Download Failed!")) { }

            }));

        }

        internal Task UpdateList()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    IEnumerable<string> existingCardNames = CardList
                        .Where(card => File.Exists(card.LocalPath))
                        .Select(card => card.CardName);

                    _downloadList?.FindAll(cardName =>
                          !existingCardNames.Contains(cardName))
                              .ForEach(cardName =>
                                  CardList.Add(new YGOCardViewModel
                                  { CardName = cardName, }));

                }
                catch (Exception ex) /*when (ex.Log())*/
                {
                    string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                    Debug.WriteLine(errMsg);
                    MessageBox.Show(errMsg);
                    errMsg.NLog();
                }
            });
        }

        public void LoadCardsFromDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory).ToList();
                files?.ForEach(path => CardList.Add(new YGOCardViewModel
                {
                    LocalPath = path,
                    CardName = Path.GetFileNameWithoutExtension(path)
                }));
            }
        }

        private void MapDataToCard(YGOCardViewModel model, YugiohPricesDetails yugiohCardData)
        {
            yugiohCardData.Data?.With(d =>
            {
                model.CardName = d?.name;
                model.CardText = d?.text;
                model.Type = d?.type;
                model.CardType = d?.card_type;
                model.Attribute = d?.family;
                model.Level = d.level;
                model.Rank = d.rank;
                model.Attack = d.atk;
                model.Defense = d.def;
            });
        }
    }
}
