using JetBrains.Annotations;

namespace Chaotik.AI.Graphs
{
    public class GraphGridNode : GraphNode
    {
        [PublicAPI] public int X { get; }
        [PublicAPI] public int Y { get; }
        [PublicAPI] public bool Passable { get; protected set; }

        public GraphGridNode(int x, int y) : this(GraphConstants.InvalidIndex, x, y, true)
        {
        }
        
        public GraphGridNode(int x, int y, bool passable) : this(GraphConstants.InvalidIndex, x, y, passable)
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