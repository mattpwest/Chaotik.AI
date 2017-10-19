using System.Linq;
using System.Collections.Generic;

namespace Chaotik.AI.Graphs.Algorithms
{
    public class GraphDepthFirstSearch<NodeType, EdgeType>
        where NodeType : GraphNode
        where EdgeType : GraphEdge, new()
    {
        public bool Found { get; }
        public int NumNodesExplored => visited.Count(state => state == NodeState.VISITED);
        public List<NodeType> Path
        {
            get
            {
                List<NodeType> path = new List<NodeType>(route.Count);
                
                if (!Found || target == GraphConstants.INVALID_INDEX) return path;

                int nodeIndex = target;
                path.Add(graph.GetNode(nodeIndex));

                while (nodeIndex != source)
                {
                    nodeIndex = route[nodeIndex];
                    
                    path.Add(graph.GetNode(nodeIndex));
                }
                
                path.Reverse();
                
                return path;
            }
        }

        private IGraph<NodeType, EdgeType> graph;
        private List<int> route;
        private List<NodeState> visited;
        private int source;
        private int target;

        public GraphDepthFirstSearch(IGraph<NodeType, EdgeType> graph, int source) : this(graph, source,
            GraphConstants.INVALID_INDEX)
        {
        }
        
        public GraphDepthFirstSearch(IGraph<NodeType, EdgeType> graph, int source, int target)
        {
            this.graph = graph;
            this.source = source;
            this.target = target;
            
            visited = new List<NodeState>(graph.NodeCount);
            for (var i = 0; i < graph.NodeCount; i++)
            {
                visited.Add(NodeState.UNVISITED);
            }
            
            route = new List<int>(graph.NodeCount);
            for (var i = 0; i < graph.NodeCount; i++)
            {
                route.Add(GraphConstants.INVALID_INDEX);
            }
            
            Found = Search();
        }
        
        public bool Search()
        {
            Stack<EdgeType> edgeStack = new Stack<EdgeType>();
            route[source] = source;
            visited[source] = NodeState.VISITED;

            if (source == target)
            {
                return true;
            }

            graph.GetEdgesFrom(source).ForEach(edgeStack.Push);
            
            while (edgeStack.Count != 0)
            {
                var next = edgeStack.Pop();

                route[next.To] = next.From;

                visited[next.To] = NodeState.VISITED;

                if (next.To == target)
                {
                    return true;
                }
                
                graph.GetEdgesFrom(next.To).ForEach(edge =>
                {
                    if (visited[edge.To] == NodeState.UNVISITED)
                    {
                        edgeStack.Push(edge);
                    }
                });
            }

            return false;
        }
    }
}