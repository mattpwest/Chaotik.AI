using System;
using System.Collections.Generic;

namespace Chaotik.AI.Graphs
{
    public class SparseGraph<TNodeType, TEdgeType> : IGraph<TNodeType, TEdgeType>
        where TNodeType : GraphNode
        where TEdgeType : GraphEdge
    {
        public bool DiGraph { get; }
        public int NodeCount => _nodes.Count;

        private int _nextIndex;
        private readonly List<TNodeType> _nodes;
        private readonly List<List<TEdgeType>> _edges;

        public SparseGraph(bool diGraph)
        {
            DiGraph = diGraph;
            _nextIndex = 0;
            
            _nodes = new List<TNodeType>();
            _edges = new List<List<TEdgeType>>();
        }

        public TNodeType GetNode(int index)
        {
            if (index < 0 || index > _nodes.Count)
            {
                return null;
            }

            var node = _nodes[index];
            return node.Index != GraphConstants.InvalidIndex ? node : null;
        }

        public TEdgeType GetEdge(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex > _edges.Count)
            {
                return null;
            }

            return _edges[fromIndex].Find(edge => edge.To == toIndex);
        }

        public List<TEdgeType> GetEdgesFrom(int fromIndex)
        {
            return new List<TEdgeType>(_edges[fromIndex]);
        }

        public int AddNode(TNodeType node)
        {
            if (node == null) return GraphConstants.InvalidIndex;
            
            node.Index = _nextIndex++;
            _nodes.Add(node);
            _edges.Add(new List<TEdgeType>());
            return node.Index;
        }

        public void RemoveNode(int index)
        {
            if (index < 0 || index > _nodes.Count)
            {
                return;
            }

            _nodes[index].Index = GraphConstants.InvalidIndex;
            
            // TODO: Should probably kill related edges?
        }

        public void AddEdge(TEdgeType edge)
        {
            for (var i = _edges.Count; i < Math.Max(edge.From, edge.To) + 1; i++)
            {
                _edges.Add(new List<TEdgeType>());
            }
            
            if (!_edges[edge.From].Contains(edge))
            {
                _edges[edge.From].Add(edge);
            }

//            TODO: Handle non-directional graphs somehow without requiring loaded data to input both directions
//            if (!DiGraph && !edges[edge.To].Contains(edge))
//            {
//                edges[edge.To].Add(edge);
//            }
        }

        public void RemoveEdge(TEdgeType edge)
        {
            _edges[edge.From].Remove(edge);

            if (!DiGraph)
            {
                _edges[edge.To].Remove(edge);
            }
        }        
    }
}