using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Chaotik.AI.Graphs.Loaders
{
    public class MapLoader
    {
        public SparseGraph<GraphGridNode, GraphEdge> Graph { get; private set; }
        public GraphGridNode[,] Map { get; private set; }
        public GraphGridNode Source { get; private set; }
        public GraphGridNode Destination { get; private set; }

        private readonly bool _orthogonal;
        private readonly IGridNodeFactory _nodeFactory;
        
        private int _width;
        private int _height;

        public MapLoader(Stream fileStream, bool orthogonal, IGridNodeFactory nodeFactory)
                : this(LoadLines(fileStream), orthogonal, nodeFactory)
        {
        }
        
        public MapLoader(List<string> lines, bool orthogonal, IGridNodeFactory nodeFactory)
        {
            _orthogonal = orthogonal;
            _nodeFactory = nodeFactory;
            
            LoadGraph(lines);
        }
        
        private void LoadGraph(List<string> lines)
        {
            _width = CalculateLongestLine(lines);
            _height = lines.Count;
            
            Graph = new SparseGraph<GraphGridNode, GraphEdge>(false);
            Map = new GraphGridNode[_width, _height];
            
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var tileChar in line)
                {
                    var node = _nodeFactory.CreateNode(x, y, tileChar.ToString());
                    Graph.AddNode(node);
                    Map[x, y] = node;

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
                    var node = _nodeFactory.CreateNode(x, y, '.'.ToString());
                    Graph.AddNode(node);
                    Map[x, y] = node;
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
                    var node = Map[x, y];

                    AddEdgeIfNeeded(node, x + 1, y + 0);
                    AddEdgeIfNeeded(node, x - 1, y + 0);
                    AddEdgeIfNeeded(node, x + 0, y + 1);
                    AddEdgeIfNeeded(node, x + 0, y - 1);

                    if (_orthogonal) continue;
                    AddEdgeIfNeeded(node, x - 1, y - 1);
                    AddEdgeIfNeeded(node, x + 1, y - 1);
                    AddEdgeIfNeeded(node, x - 1, y + 1);
                    AddEdgeIfNeeded(node, x + 1, y + 1);
                }                
                
            }
        }

        private void AddEdgeIfNeeded(GraphGridNode node, int x, int y)
        {
            if (!node.Passable) return;
            
            if (x < 0 || x >= _width || y < 0 || y >= _height) return;

            var otherNode = Map[x, y];
            if (otherNode.Passable)
            {
                Graph.AddEdge(new GraphEdge(node.Index, otherNode.Index));
            }
        }
        
        private static List<string> LoadLines(Stream stream)
        {
            var lines = new List<string>();
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
        
        private static int CalculateLongestLine(IEnumerable<string> lines)
        {
            return lines
                .Select(line => line.Length)
                .Concat(new[] {0}) // If lines is empty at least return 0
                .Max();
        }
    }
}