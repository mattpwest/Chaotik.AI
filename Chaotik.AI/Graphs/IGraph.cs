using System.Collections.Generic;

namespace Chaotik.AI.Graphs
{
    public interface IGraph<NodeType, EdgeType> where NodeType : GraphNode where EdgeType : GraphEdge
    {
        bool DiGraph { get; }
        int NodeCount { get; }
        
        NodeType GetNode(int index);
        EdgeType GetEdge(int fromIndex, int toIndex);
        List<EdgeType> GetEdgesFrom(int fromIndex);
        
        int AddNode(NodeType node);
        void RemoveNode(int index);
        void AddEdge(EdgeType edge);
        void RemoveEdge(EdgeType edge);
    }
}