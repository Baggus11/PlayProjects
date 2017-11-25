using Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CardGamesAPI.Yugioh.YugiohPrices
{
    public static class YugiohPricesService
    {
        private static Uri baseAddress = new Uri("http://yugiohprices.com/api/");

        public static async Task<string> CheckPriceForCard(string cardName)
        {
            string cardUriName = GetCardUriName(cardName);
            Uri requestUri = new Uri($"http://yugiohprices.com/api/get_card_prices/{cardUriName}");

            return await GetRequest(requestUri);
        }

        public static async Task<string> CheckPriceForCardPrintTag(string printTag)
        {
            Uri requestUri = new Uri($"http://yugiohprices.com/api/price_for_print_tag/{printTag}");
            return await GetRequest(requestUri);
        }

        public static async Task<string> CheckPriceHistory(string printTag, string rarity)
        {
            Uri requestUri = new Uri($"http://yugiohprices.com/api/price_history/{printTag}?rarity={rarity}");
            return await GetRequest(requestUri);
        }

        public static async Task<string> GetCardSetData()
        {
            Uri requestUri = new Uri("http://yugiohprices.com/api/card_sets");
            return await GetRequest(requestUri);
        }

        //public static async Task<IEnumerable> GetCardSetLists()
        //{
        //    string result = GetCardSetData().Result;

        //    Debug.WriteLine(result);

        //    return new List<object> { };
        //}

        public static async Task<string> GetRisingAndFallingList()
        {
            Uri requestUri = new Uri("http://yugiohprices.com/api/rising_and_falling");
            return await GetRequest(requestUri);
        }

        public static async Task<string> GetTop100ExpensiveCards(string rarity)
        {
            Uri requestUri = new Uri($"http://yugiohprices.com/api/top_100_cards?rarity=?rarity={rarity}");
            return await GetRequest(requestUri);
        }

        public static async Task<string> GetCardData(string cardName, CardDataType cardDataType)
        {
            string cardUriName = GetCardUriName(cardName);
            string methodName = cardDataType.GetDescription();

            Uri requestUri = new Uri($"http://yugiohprices.com/api/{methodName}/{cardUriName}");

            return await GetRequest(requestUri);
        }

        public static async Task<byte[]> GetCardImageByteData(string cardName, ImageRequestType imageType)
        {
            string cardUriName = GetCardUriName(cardName);
            string imageTypeName = imageType.GetDescription();

            Uri requestUri = new Uri($"http://yugiohprices.com/api/{imageTypeName}/{cardUriName}");
            return await GetRequestAsByteArray(requestUri);

        }

        public static async Task<bool> SaveCardToDirectory(string cardName, string cardDirectory, string extension = ".jpg")
        {
            try
            {

                if (!Directory.Exists(cardDirectory)) return false;

                string fullpath = Path.Combine(cardDirectory, cardName + extension);
                var data = await GetCardImageByteData(cardName, ImageRequestType.CardImage);

                using (MemoryStream stream = new MemoryStream(data))
                {
                    Image image = Image.FromStream(stream);
                    if (!File.Exists(fullpath))
                    {
                        image.Save(fullpath);
                        return true;
                    }

                    return false;

                }
            }
            catch (Exception ex) when (ex.Log($"Save Failed for card {cardName}!"))
            {
                return false;
            }

        }

        private static async Task<string> GetRequest(Uri requestUri)
        {
            Debug.WriteLine($"Request Uri: {requestUri.ToString()}");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            using (var response = await httpClient.GetAsync(requestUri))
            {
                var responseData = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(responseData);
                return responseData;
            }
        }

        private static async Task<Stream> GetRequestAsStream(Uri requestUri)
        {
            Debug.WriteLine($"Request Uri: {requestUri.ToString()}");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            using (var response = await httpClient.GetAsync(requestUri))
            {
                var responseData = await response.Content.ReadAsStreamAsync();
                return responseData;
            }
        }

        private static async Task<byte[]> GetRequestAsByteArray(Uri requestUri)
        {
            Debug.WriteLine($"Request Uri: {requestUri.ToString()}");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            using (var response = await httpClient.GetAsync(requestUri))
            {
                var responseData = await response.Content.ReadAsByteArrayAsync();
                return responseData;
            }
        }

        private static string GetCardUriName(string cardName)
        {
            return string.Join("+", cardName.Split(' ', '\t', '\r'));
        }

    }

}
