namespace Chaotik.AI.Graphs
{
    public class GraphGridNode : GraphNode
    {
        public int X { get; }
        public int Y { get; }
        public bool Passable { get; protected set; }

        public GraphGridNode(int x, int y) : this(GraphConstants.INVALID_INDEX, x, y, true)
        {
        }
        
        public GraphGridNode(int x, int y, bool passable) : this(GraphConstants.INVALID_INDEX, x, y, passable)
        {
        }
        
        public GraphGridNode(int index, int x, int y, bool passable) : base(index)
        {
            X = x;
            Y = y;
            Passable = passable;
        }
    }
}