namespace Chaotik.AI.Graphs.Loaders
{
    public interface IGridNodeFactory
    {
        GraphGridNode CreateNode(int x, int y, string type);
    }
}