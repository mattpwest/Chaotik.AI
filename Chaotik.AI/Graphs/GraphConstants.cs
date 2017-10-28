namespace Chaotik.AI.Graphs
{
    public static class GraphConstants
    {
        public const int InvalidIndex = -1;
        public const float DefaultEdgeCost = 1.0f;
    }

    public enum NodeState
    {
        Visited,
        Unvisited
    }
}