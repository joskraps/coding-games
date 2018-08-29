using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable PossibleNullReferenceException

//https://en.wikipedia.org/wiki/Breadth-first_search
//http://www.technical-recipes.com/2015/graph-traversals-in-c-plus-plus-c-sharp/

internal class Player
{
    private static void Main(string[] args)
    {
        var inputs = Console.ReadLine().Split(' ');
        var totalNodes = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        var linkCount = int.Parse(inputs[1]); // the number of links
        var exitGatewayCount = int.Parse(inputs[2]); // the number of exit gateways
        var nodes = new List<NodePoint>();
        var entryNode = 0;

        for (var i = 0; i < linkCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            var node1 = int.Parse(inputs[0]); // Point1 and Point2 defines a link between these nodes
            var node2 = int.Parse(inputs[1]);

            Console.Error.WriteLine($"Point1: {node1} Point2: {node2}");

            nodes.Add(new NodePoint {Point1 = node1, Point2 = node2});
        }

        for (var i = 0; i < exitGatewayCount; i++)
        {
            var ei = int.Parse(Console.ReadLine()); // the index of a gateway node
            Console.Error.WriteLine($"EI: {ei}");
            entryNode = ei;
        }

        var pointsToAdd = new List<NodePoint>();


        // game loop
        while (true)
        {
            var currentPosition = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
            Console.Error.WriteLine($"SI: {currentPosition}");

            foreach (var result in nodes.Where(np => np.Point1 == currentPosition).ToList())
            {
                if (result.Point2 != entryNode) continue;

                pointsToAdd.Add(result);

                Console.WriteLine($"{result.Point1} {result.Point2}");
            }
        }
    }
    private class NodePoint
    {
        public int Point1 { get; set; }
        public int Point2 { get; set; }
    }
}