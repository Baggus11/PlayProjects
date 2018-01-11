namespace CWClassMapGenerator
{
    public interface ICloner
    {
        //Clone A->A'
        A Clone<A>(A source);
        A DeepClone<A>(A source);
    }
}
