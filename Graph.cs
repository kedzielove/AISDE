using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AISDE
{
    class Graph
    {
        public int V { get; private set; }
        public int E { get; private set; }
        public Dictionary<Node, List<Edge>> Adj { get; private set; }

        public Graph(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string[] firstLine = file.ReadLine().Split(' ');
                    V = Int32.Parse(firstLine[0]);
                    E = Int32.Parse(firstLine[1]);

                    Node[] nodes = new Node[V];
                    for(int i = 0; i < V; i++)
                    {
                        int[] node = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        nodes[i] = new Node(node[0], node[1], node[2]);
                    }

                    Adj = new Dictionary<Node, List<Edge>>();
                    for(int i = 0; i < E; i++)
                    {
                        int[] edge = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        AddEdge(nodes[edge[1]-1], nodes[edge[2]-1]); // Workaround for inapropriate file format
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error during reading the file: " + e.Message);
            }
        }

        private void AddEdge(Node nodeA, Node nodeB)
        {
            SetEdgeBetweenNodes(nodeA, nodeB);
            SetEdgeBetweenNodes(nodeB, nodeA);
        }

        private void SetEdgeBetweenNodes(Node nodeA, Node nodeB)
        {
            List<Edge> existing;
            if (!Adj.TryGetValue(nodeA, out existing))
            {
                existing = new List<Edge>();
                Adj.Add(nodeA, existing);
            }

            existing.Add(new Edge(nodeA, nodeB));
        }
    }
}
