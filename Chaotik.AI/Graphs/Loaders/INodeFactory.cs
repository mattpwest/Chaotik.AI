namespace Chaotik.AI.Graphs.Loaders
{
    public interface INodeFactory
    {
        GraphGridNode CreateNode(string type);
    }
}