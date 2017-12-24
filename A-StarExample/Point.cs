namespace A_StarExample
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; internal set; }
        public int X { get; internal set; }
    }
}
