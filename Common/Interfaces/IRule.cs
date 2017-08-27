namespace Common
{
    public interface IRule
    {
        string Conditions { get; set; }
        string Name { get; set; }
        string Type { get; set; }

    }

}
