using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// todo: Get this - http://html-agility-pack.net/
    /// Also try: http://yugiohtopdecks.com/card/Dark+Hole
    /// 
    /// </summary>
    public class YugiohWebCrawler
    {
        private string WebImageSrcTagRegex = @"<img[^>]*?src\s*=\s*[""']?([^'"">]+?)['""][^>]*?>";

        private ConcurrentQueue<string> _downloadQueue = new ConcurrentQueue<string>();
        private string yugiohPricesBaseSearchURL = @"http://yugiohprices.com/card_price?name={0}";
        private string BaseSearchUrl;


        public YugiohWebCrawler()
        {
            BaseSearchUrl = yugiohPricesBaseSearchURL;
        }

        public async void DownloadCards(IEnumerable<string> cardNames, string saveDirectory) //todo: make yield return for concurrent updating
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            Dictionary<string, string> cardsDictionary = GetYGOSearchURLs(cardNames, saveDirectory);
            List<Task> tasks = new List<Task>();

            await Task.Run(() => Parallel.ForEach(cardsDictionary, ygo_paths =>
            {
                string full_searchUrl = ygo_paths.Key;
                string card_savePath = ygo_paths.Value;

                if (!File.Exists(card_savePath)) //local path
                {
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(full_searchUrl);
                        request.Credentials = CredentialCache.DefaultCredentials;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (WebClient client = new WebClient())
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                List<Uri> links = FetchLinksFromSource(reader.ReadToEnd());
                                DownloadCardContentFromUri(card_savePath, client, links);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                    }
                }

            }));


            await Task.WhenAll(tasks);
        }

        private void DownloadCardContentFromUri(string savePath, WebClient client, List<Uri> links)
        {
            foreach (var link in links.Where(lnk => lnk.AbsoluteUri.Contains("card_images")))
            {
                try
                {
                    client.DownloadFile(link, savePath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    continue;
                }
            }

        }

        private Dictionary<string, string> GetYGOSearchURLs(IEnumerable<string> cardNames, string save_directory)
        {
            string search_name;
            string full_searchUrl;
            string file_Name;
            string card_SavePath;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var cardname in cardNames)
            {
                search_name = string.Join("+", cardname.Split(' '));
                full_searchUrl = string.Format(BaseSearchUrl, search_name);
                file_Name = $"{cardname}.jpg";
                card_SavePath = Path.Combine(save_directory, file_Name);
                if (!dictionary.Keys.Contains(file_Name))
                    dictionary.Add(full_searchUrl, card_SavePath);

            }

            return dictionary;
        }

        private List<Uri> FetchLinksFromSource(string htmlSource)
        {
            List<Uri> links = new List<Uri>();
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, WebImageSrcTagRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            string href = "";

            foreach (Match m in matchesImgSrc)
            {
                try
                {
                    href = m.Groups[1].Value;
                    if (href.StartsWith(@"http://static"))
                    {
                        links.Add(new Uri(href));
                    }

                }
                catch (Exception)
                {
                    Debug.WriteLine($"Uri '{href}' invalid.");
                    continue;
                }
            }

            return links;
        }

    }
}
