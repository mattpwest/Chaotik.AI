namespace Chaotik.AI.Graphs
{
    public class GraphEdge
    {
        public int From { get; }
        public int To { get; }
        public float Cost => 1.0f; // Use less memory for graphs with uniform edge costs - override for varying costs

        public GraphEdge() : this(GraphConstants.InvalidIndex, GraphConstants.InvalidIndex)
        {
        }
        
        public GraphEdge(int from, int to)
        {
            From = from;
            To = to;
        }

        public override bool Equals(object obj)
        {
            if (typeof(GraphEdge) != obj.GetType()) return false;
            
            var other = (GraphEdge) obj;
            return From == other.From && To == other.To;
        }
    }
}