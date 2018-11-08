using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AISDEG
{
    sealed class DirectedGraph : Graph
    {
        public DirectedGraph(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string[] firstLine = file.ReadLine().Split(' ');
                    NodesCount = Int32.Parse(firstLine[0]);
                    EdgesCount= Int32.Parse(firstLine[1]);

                    Dictionary<int, Node> vertices = new Dictionary<int, Node>();
                    for (int i = 0; i < NodesCount; i++)
                    {
                        int[] node = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        vertices.Add(node[0], new Node(node[0], node[1], node[2]));
                    }

                    for (int i = 0; i < EdgesCount; i++)
                    {
                        int[] edge = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        AddEdge(edge[0], vertices[edge[1]], vertices[edge[2]]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during reading the file: " + e.Message);
            }
        }

        public override void DrawEdges()
        {
            
        }
    }
}
