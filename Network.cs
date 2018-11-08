using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    class Network
    {
        public Graph Map { get; private set; }

        public Network(Graph map) => Map = map;

        public Edge[] MST()
        {
            Edge[] minSpanningTree = new Edge[Map.NodesCount + 1];

            double[] distanceTo = new double[Map.NodesCount + 1];
            foreach (var node in Map.Nodes())
                distanceTo[node.id] = Double.PositiveInfinity;

            distanceTo[1] = 0.0;

            SortedDictionary<Edge, Node> intersection = new SortedDictionary<Edge, Node>();
            Node first = Map.Nodes().Find(node => node.id == 1);
            intersection.Add(new Edge(0, first, first), first);

            Node closestNode;
            bool[] markedNodes = new bool[Map.NodesCount + 1];
            while (intersection.Count > 0)
            {
                closestNode = intersection.ElementAt(0).Value;
                intersection.Remove(intersection.Keys.Min());

                markedNodes[closestNode.id] = true;
                Node neighbour;
                foreach (var edge in Map[closestNode])
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
    }
}
