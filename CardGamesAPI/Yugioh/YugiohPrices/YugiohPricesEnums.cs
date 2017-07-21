using System.ComponentModel;

namespace CardGamesAPI.Yugioh.YugiohPrices
{

    public enum ImageRequestType
    {
        [Description("card_image")]
        CardImage,
        [Description("set_image")]
        SetImage,
    }

    public enum CardDataType
    {
        [Description("card_data")]
        Details,
        [Description("card_versions")]
        Version,
        [Description("card_support")]
        Support,
    }

}
