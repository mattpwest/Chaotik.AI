using System.Collections.Generic;

namespace Chaotik.AI.Graphs
{
    public class GraphEdge
    {
        public int From { get; }
        public int To { get; }
        
        // This should use less memory for graphs with uniform edge costs - override if stored costs are needed
        public float Cost => 1.0f;

        public GraphEdge() : this(GraphConstants.INVALID_INDEX, GraphConstants.INVALID_INDEX)
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