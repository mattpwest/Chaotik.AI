using System.Reflection;
using Chaotik.AI.Graphs;
using Chaotik.AI.Graphs.Algorithms;
using Chaotik.AI.Graphs.Loaders;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests.Graphs.Algorithms
{
    [TestFixture]
    public class DepthFirstSearch
    {
        [Test]
        public void VisitsAllNodes()
        {
            // Given
            var assembly = Assembly.GetExecutingAssembly();
            AdjacencyListLoader loader;
            using (var stream = assembly.GetManifestResourceStream("Tests.Stubs.Maps." + "simple.al.txt"))
            {
                loader = new AdjacencyListLoader(stream);
            }
            
            // When
            var dfs = new GraphDepthFirstSearch<GraphNode, GraphEdge>(loader.Graph, 0);
            
            // Then
            Assert.AreEqual(6, dfs.NumNodesExplored);
        }

        [Test]
        public void FindsPathInSimpleGraph()
        {
            // Given
            var assembly = Assembly.GetExecutingAssembly();
            AdjacencyListLoader loader;
            using (var stream = assembly.GetManifestResourceStream("Tests.Stubs.Maps." + "simple.al.txt"))
            {
                loader = new AdjacencyListLoader(stream);
            }
            
            // When
            var dfs = new GraphDepthFirstSearch<GraphNode, GraphEdge>(loader.Graph, 0, 5);
            
            // Then
            Assert.AreEqual(4, dfs.Path.Count);
        }
        
        /*
        This test essentially just illustrates that DFS does not necessarily find the best path: 0,1,4 is best, but it
        finds 0,2,3,4 instead.
        */
        [Test]
        public void PathFoundIsNotOptimal()
        {
            // Given
            var assembly = Assembly.GetExecutingAssembly();
            AdjacencyListLoader loader;
            using (var stream = assembly.GetManifestResourceStream("Tests.Stubs.Maps." + "simple.al.txt"))
            {
                loader = new AdjacencyListLoader(stream);
            }
            
            // When
            var dfs = new GraphDepthFirstSearch<GraphNode, GraphEdge>(loader.Graph, 0, 4);
            var path = dfs.Path;
            
            // Then
            Assert.AreNotEqual(3, path.Count);
        }

        [Test]
        public void FindsPathOnComplexMap()
        {
            // Given
            var assembly = Assembly.GetExecutingAssembly();
            MapLoader mapLoader = null;
            using (var stream = assembly.GetManifestResourceStream("Tests.Stubs.Maps." + "simple.txt"))
            {
                mapLoader = new MapLoader(stream, true, new SimpleGridNodeFactory());                
            }
            
            // When
            var dfs = new GraphDepthFirstSearch<GraphGridNode, GraphEdge>(
                mapLoader.Graph,
                mapLoader.Source.Index,
                mapLoader.Destination.Index
            );
            
            // Then
            Assert.IsTrue(dfs.Found);
        }
    }
}