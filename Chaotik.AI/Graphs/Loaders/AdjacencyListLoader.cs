using System;
using System.Collections.Generic;
using System.IO;

namespace Chaotik.AI.Graphs.Loaders
{
    public class AdjacencyListLoader
    {
        public SparseGraph<GraphNode, GraphEdge> Graph => _graph;
        
        private SparseGraph<GraphNode, GraphEdge> _graph;

        public AdjacencyListLoader(Stream stream)
        {
            Load(stream);
        }

        public void Load(Stream stream)
        {
            _graph = new SparseGraph<GraphNode, GraphEdge>(false);
            var lines = LoadLines(stream);
            foreach (var line in lines)
            {
                var nodes = line.Split(',');
                var from = int.Parse(nodes[0]);

                GraphNode node = new GraphNode();
                if (_graph.AddNode(node) != from)
                {
                    throw new ArgumentException("Invalid source data: new node does not match graph index.");
                }

                for (var i = 1; i < nodes.Length; i++)
                {
                    var to = int.Parse(nodes[i]);

                    _graph.AddEdge(new GraphEdge(from, to));
                }
            }
        }

        private static List<string> LoadLines(Stream stream)
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
    }
}