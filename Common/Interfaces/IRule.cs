namespace Common
{
    public interface IRule
    {
        string PreConditions { get; set; }
        string PostConditions { get; set; }
        string Name { get; set; }
        string Type { get; set; }

    }

}
