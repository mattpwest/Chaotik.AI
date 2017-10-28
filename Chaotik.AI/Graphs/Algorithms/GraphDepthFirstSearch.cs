using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Chaotik.AI.Graphs.Algorithms
{
    public class GraphDepthFirstSearch<NodeType, EdgeType>
        where NodeType : GraphNode
        where EdgeType : GraphEdge, new()
    {
        public bool Found { get; }
        public int NumNodesExplored => _visited.Count(state => state == NodeState.Visited);
        public List<NodeType> Path
        {
            get
            {
                var path = new List<NodeType>(_route.Count);
                
                if (!Found || _target == GraphConstants.InvalidIndex) return path;

                var nodeIndex = _target;
                path.Add(_graph.GetNode(nodeIndex));

                while (nodeIndex != _source)
                {
                    nodeIndex = _route[nodeIndex];
                    
                    path.Add(_graph.GetNode(nodeIndex));
                }
                
                path.Reverse();
                
                return path;
            }
        }

        private readonly IGraph<NodeType, EdgeType> _graph;
        private readonly List<int> _route;
        private readonly List<NodeState> _visited;
        private readonly int _source;
        private readonly int _target;

        public GraphDepthFirstSearch(
            IGraph<NodeType, EdgeType> graph,
            int source,
            int target = GraphConstants.InvalidIndex)
        {
            _graph = graph;
            _source = source;
            _target = target;
            
            _visited = new List<NodeState>(graph.NodeCount);
            for (var i = 0; i < graph.NodeCount; i++)
            {
                _visited.Add(NodeState.Unvisited);
            }
            
            _route = new List<int>(graph.NodeCount);
            for (var i = 0; i < graph.NodeCount; i++)
            {
                _route.Add(GraphConstants.InvalidIndex);
            }
            
            Found = Search();
        }
        
        [PublicAPI]
        public bool Search()
        {
            var edgeStack = new Stack<EdgeType>();
            _route[_source] = _source;
            _visited[_source] = NodeState.Visited;

            if (_source == _target)
            {
                return true;
            }

            _graph.GetEdgesFrom(_source).ForEach(edgeStack.Push);
            
            while (edgeStack.Count != 0)
            {
                var next = edgeStack.Pop();

                _route[next.To] = next.From;

                _visited[next.To] = NodeState.Visited;

                if (next.To == _target)
                {
                    return true;
                }
                
                _graph.GetEdgesFrom(next.To).ForEach(edge =>
                {
                    if (_visited[edge.To] == NodeState.Unvisited)
                    {
                        edgeStack.Push(edge);
                    }
                });
            }

            return false;
        }
    }
}