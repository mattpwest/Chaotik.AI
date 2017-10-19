using System.Numerics;

namespace Chaotik.AI.Graphs.Loaders
{
    public interface IGridNodeFactory
    {
        GraphGridNode CreateNode(Vector2 position, string type);
    }
}