using System.Numerics;
using Chaotik.AI.Graphs;
using Chaotik.AI.Graphs.Loaders;

namespace Tests.Stubs
{
    public class SimpleGridNodeFactory : IGridNodeFactory
    {
        public GraphGridNode CreateNode(Vector2 position, string type)
        {
            if (type.Equals("."))
            {
                return new GraphGridNode((int) position.X, (int) position.Y, true);
            }
            
            if (type.Equals("#"))
            {
                return new GraphGridNode((int) position.X, (int) position.Y, false);
            }

            return new GraphGridNode((int) position.X, (int) position.Y, true);
        }
    }
}