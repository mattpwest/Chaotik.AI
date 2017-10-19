namespace Chaotik.AI.Graphs
{
    public class GraphNode
    {
        public int Index { get; set; }

        public GraphNode() : this(GraphConstants.INVALID_INDEX)
        {
        }

        public GraphNode(int index)
        {
            Index = index;
        }
    }
}