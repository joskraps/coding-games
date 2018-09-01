using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        var nodeCount = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        var linkCount = int.Parse(inputs[1]); // the number of links
        var exitCount = int.Parse(inputs[2]); // the number of exit gateways
        var vertices = new int[nodeCount+1];
        var edges = new List<Tuple<int, int>>();

        for (var i = 0; i < nodeCount; i++)
        {
            vertices[i] = i;
        }

        for (var i = 0; i < linkCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');

            var nodeLink1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            var nodeLink2 = int.Parse(inputs[1]);

            Console.Error.WriteLine($"Node Start: {nodeLink1} Node End: {nodeLink2}");

            edges.Add(new Tuple<int, int>(nodeLink1,nodeLink2));
        }

        var graph = new Graph<int>(vertices, edges);
        var algorithms = new Algorithms();
        var endingG = new List<int>();

        for (var i = 0; i < exitCount; i++)
        {
            var EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            Console.Error.WriteLine($"Index of a gateway: {EI}");
            endingG.Add(EI);
        }
        
        var path = new List<int>();

        // game loop
        while (true)
        {
            var SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this t
            var maxJumps = 999;
            var fp1 = 0;
            var fp2 = 0;
            
            foreach(var ei in endingG)
            {
                var pointsBlah = algorithms.ShortestPathFunction(graph, SI)(ei);
                Console.Error.WriteLine($"{pointsBlah[0]} {pointsBlah[1]} {pointsBlah.Count}");
                
                if(pointsBlah.Count < maxJumps)
                {
                    fp1 = pointsBlah[0];   
                    fp2 = pointsBlah[1];
                    
                    maxJumps = pointsBlah.Count;
                }
            }
            

            Console.Error.WriteLine($"Starting index: {SI}");

            Console.WriteLine($"{fp1} {fp2}");
        }
    }

    private class NodePoint
    {
        public int N1 { get; set; }
        public int N2 { get; set; }

    }

    private class Graph<T>
    {
        public Graph()
        {
        }

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        public void AddVertex(T vertex)
        {
            AdjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }

    private class Algorithms
    {
        public List<T> BFS<T>(Graph<T> graph, T start, Action<T> preVisit = null)
        {
            var visited = new List<T>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                preVisit?.Invoke(vertex);

                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
            }

            return visited;
        }

        public Func<T, IList<T>> ShortestPathFunction<T>(Graph<T> graph, T start)
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, IList<T>> shortestPath = v => {
                var path = new List<T> { };

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}
