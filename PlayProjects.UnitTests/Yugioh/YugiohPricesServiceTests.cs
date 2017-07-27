using CardGamesAPI.Yugioh.YugiohPrices;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using static CardGamesAPI.Yugioh.YugiohPrices.YugiohPricesService;

namespace CardGamesAPI.Yugioh.Tests
{
    [TestClass()]
    public class YugiohPricesServiceTests
    {
        //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        private string _defaultSaveDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads");
        string printTag = "LOB-EN001";
        string rarity = "Ultra";
        string cardName = "Stardust Dragon";

        //Price Checks
        [TestMethod()]
        public void CheckPriceForCardPrintTagTest()
        {
            string printTag = "LOB-EN001";

            var task = CheckPriceForCardPrintTag(printTag);
            Debug.WriteLine(task.Result);

            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void CheckPriceForCardNameTest()
        {
            var task = CheckPriceForCard(cardName);
            Debug.WriteLine(task.Result);

            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void CheckPriceHistoryTest()
        {
            var task = CheckPriceHistory(printTag, rarity);
            Debug.WriteLine(task.Result);

            Assert.IsNotNull(task.Result);
        }


        //Data Tests
        [TestMethod()]
        public void GetCardDetailsTest()
        {
            string details = GetCardData(cardName, CardDataType.Details).Result;
            Debug.WriteLine(details);

            YugiohPricesDetails card = JsonConvert.DeserializeObject<YugiohPricesDetails>(details);
            card.Dump();

            Assert.IsNotNull(details);
        }

        [TestMethod()]
        public void GetCardVersionTest()
        {
            var task = GetCardData(cardName, CardDataType.Version);
            Debug.WriteLine(task.Result);

            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void GetCardSupportTest()
        {
            var task = GetCardData(cardName, CardDataType.Support);
            Debug.WriteLine(task.Result);

            Assert.IsNotNull(task.Result);
        }


        [TestMethod()]
        public void GetCardSetListsTest()
        {
            string result = GetCardSetData().Result;
            Debug.WriteLine(result);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetRisingAndFallingListTest()
        {
            string result = GetRisingAndFallingList().Result;
            Debug.WriteLine(result);

            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void GetTop100ExpensiveCardsTest()
        {
            string result = GetTop100ExpensiveCards(rarity).Result;
            Debug.WriteLine(result);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetCardImageStaticUrlTest()
        {
            string fullpath = Path.Combine(_defaultSaveDir, cardName + ".jpg");
            var task = GetCardImageByteData(cardName, ImageRequestType.CardImage);

            using (MemoryStream stream = new MemoryStream(task.Result))
            {
                Image image = Image.FromStream(stream);
                if (!File.Exists(fullpath))
                    image.Save(fullpath);
            }

            Process.Start(_defaultSaveDir, string.Format("/select, \"{0}\"", _defaultSaveDir));
        }




        public void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}