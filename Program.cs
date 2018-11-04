using System;
using System.Collections.Generic;
using System.IO;

namespace AISDE
{
    class Node
    {
        private int id;
        private int x, y;

        public Node(int id, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }

        public int GetId() { return id; }
        public int GetX() { return x; }
        public int GetY() { return y; }

        public override int GetHashCode()
        {
            return new Tuple<int, int>(x, y).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
                return false;
            } else {
                Node node = (Node)obj;
                return this.x == node.GetX() && this.y == node.GetY();
            }
        }
    }
    class Edge
    {
        private int id;
        private Node A, B;

        public Edge(int id, Node A, Node B)
        {
            this.id = id;
            this.A = A;
            this.B = B;
        }

        public int GetNodeAId() { return A.GetId(); }
        public int GetNodeBId() { return B.GetId(); }
        public double GetWeight()
        {
            return Math.Sqrt(Math.Pow(A.GetX() - B.GetX(), 2) + (Math.Pow(A.GetY() - B.GetY(), 2)));
        }
    }

    class Network
    {
        private Dictionary<int, List<Edge>> nodeIdToEdges;
        private Dictionary<int, Node> nodeIdToNode;

        /**
         * File format (edges are directed):
         * node_count edge_count
         * node_id node_x1 node_x2
         * ...
         * node_id node_x1 node_x2
         * edge_id node_A_id node_B_id
         * ...
         * edge_id node_A_id node_B_id
         */
        public Network(string path)
        {
            nodeIdToEdges = new Dictionary<int, List<Edge>>();
            nodeIdToNode = new Dictionary<int, Node>();
            
            try {
                using (StreamReader sr = new StreamReader(path)) {
                    string[] firstLine = sr.ReadLine().Split(' ');
                    int nodeCount = int.Parse(firstLine[0]);
                    int edgeCount = int.Parse(firstLine[1]);

                    for (int i = 0; i < nodeCount; i++)
                    {
                        int[] node = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                        nodeIdToNode.Add(node[0], new Node(node[0], node[1], node[2]));
                        nodeIdToEdges.Add(node[0], new List<Edge>());

                    }

                    for (int i = 0; i < edgeCount; i++) {
                        int[] edge = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                        nodeIdToEdges[edge[1]].Add(new Edge(edge[0], nodeIdToNode[edge[1]], nodeIdToNode[edge[2]]));
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("Error during reading the file: " + e.Message);
            }
        }
        public Edge[] minimumSpanningTree()
        {
            List<Edge> result = new List<Edge>();
            HashSet<int> visitedNodes = new HashSet<int>();
            // priority queue - (cost, nodeId)
            foreach (int currentNodeId in nodeIdToNode.Keys) {
                SortedSet<Tuple<double, int, Edge>> priorityQueue = new SortedSet<Tuple<double, int, Edge>> {
                    new Tuple<double, int, Edge>(0, currentNodeId, null)
                };

                while (priorityQueue.Count > 0) {
                    double cost = priorityQueue.Min.Item1;
                    int nodeId = priorityQueue.Min.Item2;
                    Edge edge = priorityQueue.Min.Item3;
                    priorityQueue.Remove(priorityQueue.Min);

                    if (visitedNodes.Contains(nodeId)) {
                        continue;
                    }
                    visitedNodes.Add(nodeId);

                    foreach (Edge e in nodeIdToEdges[nodeId]) {
                        if (!visitedNodes.Contains(e.GetNodeBId())) {
                            priorityQueue.Add(new Tuple<double, int, Edge>(cost + e.GetWeight(), e.GetNodeBId(), e));
                        }
                    }

                    if (edge != null) {
                        result.Add(edge);
                    }
                }
            }

            return result.ToArray();
        }
        public Edge[] shortestPath(Node from, Node to)
        {
            // visitedNodes - nodeId to edge
            Dictionary<int, Edge> visitedNodes = new Dictionary<int, Edge>();
            // priority queue - (cost, nodeId, edge)
            SortedSet<Tuple<double, int, Edge>> priorityQueue = new SortedSet<Tuple<double, int, Edge>>
            {
                new Tuple<double, int, Edge>(0, from.GetId(), null)
            };

            while (priorityQueue.Count > 0) {
                int nodeId = priorityQueue.Min.Item2;
                double currentCost = priorityQueue.Min.Item1;
                Edge edge = priorityQueue.Min.Item3;

                priorityQueue.Remove(priorityQueue.Min);
                if (visitedNodes.ContainsKey(nodeId)) {
                    continue;
                }
                visitedNodes.Add(nodeId, edge);

                if (nodeId == to.GetId()) {
                    List<Edge> result = new List<Edge>();
                    int currentNodeId = to.GetId();

                    while (currentNodeId != from.GetId()) {
                        Edge currentEdge = visitedNodes[currentNodeId];
                        result.Add(currentEdge);
                        currentNodeId = currentEdge.GetNodeAId();
                    }

                    result.Reverse();
                    return result.ToArray();
                }

                foreach (Edge e in nodeIdToEdges[nodeId]) {
                    if (!visitedNodes.ContainsKey(e.GetNodeBId())) {
                        priorityQueue.Add(new Tuple<double, int, Edge>(currentCost + e.GetWeight(), e.GetNodeBId(), e));
                    }                   
                }
            }

            return null;
        }
        public void drawEdges(Edge[] edges)
        {
            // narysuj graf i zaznacz w nim podane krawedzie
            // tj. linia drawEdges(shortestPath(A,B)) wyswietli nam sciezke z A do B w grafie
        }
        public static void Main() 
        {

        }
    }
}