using Chaotik.AI.Graphs;
using Chaotik.AI.Graphs.Loaders;

namespace Tests.Stubs
{
    public class SimpleGridNodeFactory : IGridNodeFactory
    {
        public GraphGridNode CreateNode(int x, int y, string type)
        {
            if (type.Equals("."))
            {
                return new GraphGridNode(x, y, true);
            }
            
            if (type.Equals("#"))
            {
                return new GraphGridNode(x, y, false);
            }

            return new GraphGridNode(x, y, true);
        }
    }
}