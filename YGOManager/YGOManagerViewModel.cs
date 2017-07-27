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

namespace YGOManager
{
    /// <summary>
    /// http://www.yugioh-card.com/uk/gameplay/detail.php?id=1155
    /// </summary>
    public class YGOManagerViewModel : ViewModelBase
    {
        private string _defaultSaveDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads");
        private string _CardSaveDirectory;
        private BaseViewableCollection<YGOCardViewModel> _CardList = new BaseViewableCollection<YGOCardViewModel>();
        public BaseViewableCollection<YGOCardViewModel> CardList { get { return _CardList; } }

        public string Text
        {
            get { return GetValue(() => Text); }
            set { SetValue(() => Text, value); }
        }

        private List<string> _downloadList
        {
            get
            {
                return Text?
                    .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Distinct().ToList();
            }
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

        internal async void UpdateListAsync()
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

        }

        internal async void SyncCards()
        {
            //Find any cards necessary to download, then download them.
            var cardsToDownload = CardList
                .Where(card => _downloadList.Contains(card.CardName) && !card.ImageOnDisk)
                .Select(c => c.CardName);

            cardsToDownload.Dump("cards to download");
            await DownloadCards(cardsToDownload, _defaultSaveDir);

            //Check Db tables and local files, make a collection of necessary downloads.
            //CheckLocalFiles();

            //Prompt user for sync if enabled (default).


            Text = null;
            //Process.Start(_defaultSaveDir, string.Format("/select, \"{0}\"", _defaultSaveDir));
        }

        private void CheckLocalFiles()
        {
            throw new NotImplementedException();
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

        public void LoadCardsFromDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory).ToList();
                files?.ForEach(path =>
                    CardList.Add(new YGOCardViewModel
                    {
                        LocalPath = path,
                        CardName = Path.GetFileNameWithoutExtension(path)
                    }));
            }
        }

        private void SaveCardToDb(YGOCardViewModel card)
        {
            //TODO: perform a mergeinsert on this card.
            throw new NotImplementedException();
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
