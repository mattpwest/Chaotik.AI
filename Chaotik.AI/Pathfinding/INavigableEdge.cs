namespace Chaotik.AI.Pathfinding
{
    public interface INavigableEdge
    {
        INavigableNode NodeA { get; }
        INavigableNode NodeB { get; }
        
        bool Contains(INavigableNode node);
    }
}