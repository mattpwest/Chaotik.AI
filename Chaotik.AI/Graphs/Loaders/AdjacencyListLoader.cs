using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace Chaotik.AI.Graphs.Loaders
{
    public class AdjacencyListLoader
    {
        public SparseGraph<GraphNode, GraphEdge> Graph { get; private set; }

        public AdjacencyListLoader(Stream stream)
        {
            Load(stream);
        }

        [PublicAPI]
        public void Load(Stream stream)
        {
            Graph = new SparseGraph<GraphNode, GraphEdge>(false);
            var lines = LoadLines(stream);
            foreach (var line in lines)
            {
                var nodes = line.Split(',');
                var from = int.Parse(nodes[0]);

                var node = new GraphNode();
                if (Graph.AddNode(node) != from)
                {
                    throw new ArgumentException("Invalid source data: new node does not match graph index.");
                }

                for (var i = 1; i < nodes.Length; i++)
                {
                    var to = int.Parse(nodes[i]);

                    Graph.AddEdge(new GraphEdge(from, to));
                }
            }
        }

        private static IEnumerable<string> LoadLines(Stream stream)
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
    }
}