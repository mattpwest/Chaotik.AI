using System.Numerics;

namespace Chaotik.AI.Graphs
{
    public class GraphGridNode : GraphNode
    {
        public Vector2 Position { get; }
        public bool Passable { get; }

        public GraphGridNode(int x, int y) : this(GraphConstants.INVALID_INDEX, x, y, true)
        {
        }
        
        public GraphGridNode(int x, int y, bool passable) : this(GraphConstants.INVALID_INDEX, x, y, passable)
        {
        }
        
        public GraphGridNode(int index, int x, int y, bool passable) : base(index)
        {
            Position = new Vector2(x, y);
            Passable = passable;
        }
    }
}