using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace Chaotik.AI.Graphs.Loaders
{
    public class MapLoader
    {
        [PublicAPI] public SparseGraph<GraphGridNode, GraphEdge> Graph { get; private set; }
        [PublicAPI] public GraphGridNode[,] Map { get; private set; }
        [PublicAPI] public GraphGridNode Source { get; private set; }
        [PublicAPI] public GraphGridNode Destination { get; private set; }
        [PublicAPI] public int Width { get; private set; }
        [PublicAPI] public int Height { get; private set; }

        private readonly bool _orthogonal;
        private readonly IGridNodeFactory _nodeFactory;

        public MapLoader(Stream fileStream, bool orthogonal, IGridNodeFactory nodeFactory)
                : this(LoadLines(fileStream), orthogonal, nodeFactory)
        {
        }
        
        public MapLoader(List<string> lines, bool orthogonal, IGridNodeFactory nodeFactory)
        {
            _orthogonal = orthogonal;
            _nodeFactory = nodeFactory;

            lines.Reverse();
            LoadGraph(lines);
        }
        
        private void LoadGraph(IReadOnlyCollection<string> lines)
        {
            Width = CalculateLongestLine(lines);
            Height = lines.Count;
            
            Graph = new SparseGraph<GraphGridNode, GraphEdge>(false);
            Map = new GraphGridNode[Width, Height];
            
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
                        Source = node;
                    else if (tileChar == 'X')
                    {
                        Destination = node;
                    }

                    x++;
                }

                y++;
            }
            
            CreateEdges();
        }

        private void CreateEdges()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var node = Map[x, y];
                    
                    if (node == null) continue;

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
            
            if (x < 0 || x >= Width || y < 0 || y >= Height) return;

            var otherNode = Map[x, y];
            if (otherNode != null && otherNode.Passable)
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