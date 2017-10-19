using System.Collections.Generic;

namespace Chaotik.AI.Pathfinding
{
    public interface INavigableNode
    {
        List<INavigableEdge> Edges { get; }
        void AddEdge(INavigableEdge edge);
    }
}