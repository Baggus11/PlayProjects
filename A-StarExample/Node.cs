using System;

namespace A_StarExample
{
    public class Node
    {
        private Node parent;
        public Node Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                G = parent.G + GetTraversalCost(Location, parent.Location);
            }
        }

        public Point Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float G { get; private set; } //cost of start to here
        public float F { get { return G + H; } } //estm. total cost
        public float H { get; private set; } //cost from here to end
        public NodeState State { get; set; }

        public Node(int x, int y, bool isWalkable, Point endLocation)
        {
            Location = new Point(x, y);
            State = NodeState.Untested;
            IsWalkable = isWalkable;
            H = GetTraversalCost(Location, endLocation);
            G = 0;
        }
        public override string ToString()
        {
            return $"{Location.X},{Location.Y},{State}";
        }
        internal static float GetTraversalCost(Point location, Point targetLocation)
        {
            float deltaX = targetLocation.X - location.X;
            float deltaY = targetLocation.Y - location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
