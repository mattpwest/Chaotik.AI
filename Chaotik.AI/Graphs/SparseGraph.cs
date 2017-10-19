using System;
using System.Collections.Generic;

namespace Chaotik.AI.Graphs
{
    public class SparseGraph<NodeType, EdgeType> : IGraph<NodeType, EdgeType>
        where NodeType : GraphNode
        where EdgeType : GraphEdge
    {
        public bool DiGraph { get; }
        public int NodeCount => nodes.Count;

        private int nextIndex;
        private List<NodeType> nodes;
        private List<List<EdgeType>> edges;

        public SparseGraph(bool diGraph)
        {
            DiGraph = diGraph;
            nextIndex = 0;
            
            nodes = new List<NodeType>();
            edges = new List<List<EdgeType>>();
        }

        public NodeType GetNode(int index)
        {
            if (index < 0 || index > nodes.Count)
            {
                return null;
            }

            var node = nodes[index];
            return node.Index != GraphConstants.INVALID_INDEX ? node : null;
        }

        public EdgeType GetEdge(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex > edges.Count)
            {
                return null;
            }

            return edges[fromIndex].Find(edge => edge.To == toIndex);
        }

        public List<EdgeType> GetEdgesFrom(int fromIndex)
        {
            return new List<EdgeType>(edges[fromIndex]);
        }

        public int AddNode(NodeType node)
        {
            node.Index = nextIndex++;
            nodes.Add(node);
            edges.Add(new List<EdgeType>());
            return node.Index;
        }

        public void RemoveNode(int index)
        {
            if (index < 0 || index > nodes.Count)
            {
                return;
            }

            nodes[index].Index = GraphConstants.INVALID_INDEX;
            
            // TODO: Should probably kill related edges?
        }

        public void AddEdge(EdgeType edge)
        {
            for (int i = edges.Count; i < Math.Max(edge.From, edge.To) + 1; i++)
            {
                edges.Add(new List<EdgeType>());
            }
            
            if (!edges[edge.From].Contains(edge))
            {
                edges[edge.From].Add(edge);
            }

//            TODO: Handle non-directional graphs somehow without requiring loaded data to input both directions
//            if (!DiGraph && !edges[edge.To].Contains(edge))
//            {
//                edges[edge.To].Add(edge);
//            }
        }

        public void RemoveEdge(EdgeType edge)
        {
            edges[edge.From].Remove(edge);

            if (!DiGraph)
            {
                edges[edge.To].Remove(edge);
            }
        }        
    }
}