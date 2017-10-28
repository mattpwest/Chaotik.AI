using System.Collections.Generic;

namespace Chaotik.AI.Graphs
{
    public interface IGraph<TNodeType, TEdgeType> where TNodeType : GraphNode where TEdgeType : GraphEdge
    {
        bool DiGraph { get; }
        int NodeCount { get; }
        
        TNodeType GetNode(int index);
        TEdgeType GetEdge(int fromIndex, int toIndex);
        List<TEdgeType> GetEdgesFrom(int fromIndex);
        
        int AddNode(TNodeType node);
        void RemoveNode(int index);
        void AddEdge(TEdgeType edge);
        void RemoveEdge(TEdgeType edge);
    }
}