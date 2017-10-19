using System.Collections.Generic;

namespace Chaotik.AI.Pathfinding
{
    public interface INavigableMap
    {
        List<INavigableNode> Nodes { get; }
        
        INavigableNode FindNodeById(string id);
    }
}