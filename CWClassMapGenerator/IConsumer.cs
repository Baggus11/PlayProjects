namespace CWClassMapGenerator
{
    public interface IConsumer
    {
        //Consume ["eat"] A(B)
        A Consume<A, B>();
        A Consume<A, B, C>();
    }
}
