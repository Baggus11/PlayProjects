namespace CardGamesAPI.Yugioh.YugiohPrices
{
    public class YugiohPricesDetails
    {
        public string status { get; set; }
        public CardData Data { get; set; }
    }

    public class CardData
    {
        public string name { get; set; }
        public string text { get; set; }
        public string card_type { get; set; }
        public string type { get; set; }
        public string family { get; set; }
        //public int? atk { get { return atk ?? 0; } set { atk = value; } }
        //public int? def { get { return atk ?? 0; } set { def = value; } }
        //public int? level { get { return level ?? 0; } set { level = value; } }
        //public int? rank { get { return rank ?? 0; } set { rank = value; } }
        public string property { get; set; }
        public int level { get; set; }
        public int rank { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
    }
}
