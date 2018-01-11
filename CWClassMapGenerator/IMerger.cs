namespace CWClassMapGenerator
{
    public interface IMerger
    {
        //Merge A,B -> C
        C Merge<A, B, C>(A source1, B source2);
    }
}
