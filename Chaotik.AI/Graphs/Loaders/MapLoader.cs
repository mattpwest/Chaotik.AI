using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace Chaotik.AI.Graphs.Loaders
{
    public class MapLoader
    {
        public SparseGraph<GraphGridNode, GraphEdge> Graph => _graph;
        public GraphGridNode[,] Map => _map;
        public GraphGridNode Source { get; private set; }
        public GraphGridNode Destination { get; private set; }

        private Stream _fileStream;
        private bool _orthogonal;
        private IGridNodeFactory _nodeFactory;
        
        private int _width;
        private int _height;
        
        private SparseGraph<GraphGridNode, GraphEdge> _graph;
        private GraphGridNode[,] _map;

        public MapLoader(Stream fileStream, bool orthogonal, IGridNodeFactory nodeFactory)
        {
            _fileStream = fileStream;
            _orthogonal = orthogonal;
            _nodeFactory = nodeFactory;
            
            LoadGraph(LoadLines(_fileStream));
        }
        
        private List<string> LoadLines(Stream stream)
        {
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);

                    line = reader.ReadLine();
                }
            }
            return lines;
        }
        
        private void LoadGraph(List<string> lines)
        {
            _width = CalculateLongestLine(lines);
            _height = lines.Count;
            
            _graph = new SparseGraph<GraphGridNode, GraphEdge>(false);
            _map = new GraphGridNode[_width, _height];
            
            int x = 0;
            int y = 0;
            foreach (var line in lines)
            {
                x = 0;
                foreach (var tileChar in line)
                {
                    Vector2 position = new Vector2(x, y);
                    GraphGridNode node = _nodeFactory.CreateNode(position, tileChar.ToString());
                    _graph.AddNode(node);
                    _map[x, y] = node;

                    if (tileChar == '@')
                    {
                        Source = node;
                    }

                    if (tileChar == 'X')
                    {
                        Destination = node;
                    }

                    x++;
                }

                for (; x < _width; x++)
                {
                    Vector2 position = new Vector2(x, y);
                    GraphGridNode node = _nodeFactory.CreateNode(position, '.'.ToString());
                    _graph.AddNode(node);
                    _map[x, y] = node;
                }

                y++;
            }
            
            CreateEdges();
        }

        private void CreateEdges()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    GraphGridNode node = _map[x, y];

                    AddEdgeIfNeeded(node, x + 1, y + 0);
                    AddEdgeIfNeeded(node, x - 1, y + 0);
                    AddEdgeIfNeeded(node, x + 0, y + 1);
                    AddEdgeIfNeeded(node, x + 0, y - 1);
                    
                    if (!_orthogonal) // Also add diagonal edges 
                    {
                        AddEdgeIfNeeded(node, x - 1, y - 1);
                        AddEdgeIfNeeded(node, x + 1, y - 1);
                        AddEdgeIfNeeded(node, x - 1, y + 1);
                        AddEdgeIfNeeded(node, x + 1, y + 1);
                    }
                }                
                
            }
        }

        private void AddEdgeIfNeeded(GraphGridNode node, int x, int y)
        {
            if (!node.Passable) return;
            if (x < 0 || x >= _width || y < 0 || y >= _height) return;

            GraphGridNode otherNode = _map[x, y];
            if (otherNode.Passable)
            {
                _graph.AddEdge(new GraphEdge(node.Index, otherNode.Index));
            }
        }
        
        private int CalculateLongestLine(List<string> lines)
        {
            int length = 0;
            foreach (var line in lines)
            {
                if (line.Length > length)
                {
                    length = line.Length;
                }
            }
            return length;
        }
        
    }
}