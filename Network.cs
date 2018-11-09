using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    class Network
    {
        /*
         * The equivalent of a minimum spanning tree in a directed graph is called an optimum 
         * branching or a minimum-cost arborescence. The classical algorithm for solving this 
         * problem is the Chu-Liu/Edmonds algorithm. There have been several optimized 
         * implementations of this algorithm over the years using better data structures; the 
         * best one that I know of uses a Fibonacci heap and runs in time O(m + n log n) and 
         * is due to Galil et al.
         * https://stackoverflow.com/questions/21991823/finding-a-minimum-spanning-tree-on-a-directed-graph
         */
        public Edge[] MST(UndirectedGraph Map)
        {
            Edge[] minSpanningTree = new Edge[Map.NodesCount + 1];

            double[] distanceTo = new double[Map.NodesCount + 1];
            foreach (var node in Map.Nodes())
                distanceTo[node.id] = Double.PositiveInfinity;

            distanceTo[1] = 0.0;

            SortedDictionary<Edge, Node> intersection = new SortedDictionary<Edge, Node>();
            Node first = Map.Nodes().Find(node => node.id == 1);
            intersection.Add(new UndirectedEdge(0, first, first), first);

            Node closestNode;
            bool[] markedNodes = new bool[Map.NodesCount + 1];
            while (intersection.Count > 0)
            {
                closestNode = intersection.ElementAt(0).Value;
                intersection.Remove(intersection.Keys.Min());

                markedNodes[closestNode.id] = true;
                Node neighbour;
                foreach (UndirectedEdge edge in Map[closestNode])
                {
                    neighbour = edge.Other(closestNode);
                    if (markedNodes[neighbour.id])
                        continue;

                    if(edge.Weight < distanceTo[neighbour.id])
                    {
                        minSpanningTree[neighbour.id] = edge;
                        distanceTo[neighbour.id] = edge.Weight;

                        if (intersection.ContainsValue(neighbour))
                            intersection[edge] = neighbour;
                        else
                            intersection.Add(edge, neighbour);
                    }
                }
            }

            return minSpanningTree;
        }
        public Edge[] SPT(DirectedGraph Map, Node from, Node to)
        {
            Edge[] shortestPathTo = new Edge[Map.NodesCount + 1];

            double[] distanceTo = new double[Map.NodesCount + 1];
            foreach (var node in Map.Nodes())
                distanceTo[node.id] = Double.PositiveInfinity;

            distanceTo[from.id] = 0.0;

            SortedDictionary<Edge, Node> intersection = new SortedDictionary<Edge, Node>();
            intersection.Add(new DirectedEdge(0, from, from), from);

            Node closestNode;
            while (intersection.Count > 0)
            {
                closestNode = intersection.ElementAt(0).Value;
                intersection.Remove(intersection.Keys.Min());

                foreach(DirectedEdge edge in Map[closestNode])
                {
                    Node neighbour = edge.To();

                    if(distanceTo[neighbour.id] > distanceTo[closestNode.id] + edge.Weight)
                    {
                        distanceTo[neighbour.id] = distanceTo[closestNode.id] + edge.Weight;
                        shortestPathTo[neighbour.id] = edge;

                        if (intersection.ContainsValue(neighbour))
                            intersection[edge] = neighbour;
                        else
                            intersection.Add(edge, neighbour);
                    }
                }
            }

            return shortestPathTo;
        }
    }
}
