namespace Chaotik.AI.Graphs
{
    public class GraphNode
    {
        public int Index { get; set; }

        public GraphNode(int index = GraphConstants.InvalidIndex)
        {
            Index = index;
        }
    }
}