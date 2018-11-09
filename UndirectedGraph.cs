using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AISDEG
{
    sealed class UndirectedGraph : Graph
    {
        public UndirectedGraph(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string[] firstLine = file.ReadLine().Split(' ');
                    NodesCount = Int32.Parse(firstLine[0]);
                    EdgesCount = Int32.Parse(firstLine[1]);

                    for (int i = 0; i < NodesCount; i++)
                    {
                        int[] parsedLine = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        Node node = new Node(parsedLine[0], parsedLine[1], parsedLine[2]);
                        adjacencyLists.Add(node, new List<Edge>());
                    }

                    for (int i = 0; i < EdgesCount; i++)
                    {
                        int[] parsedLine = Array.ConvertAll(file.ReadLine().Split(' '), Int32.Parse);
                        AddEdge(parsedLine[0], adjacencyLists.Keys.Where(v => v.id == parsedLine[1]).FirstOrDefault(), adjacencyLists.Keys.Where(v => v.id == parsedLine[2]).FirstOrDefault());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during reading the file: " + e.Message);
            }
        }

        public override void AddEdge(int index, Node v, Node w)
        {
            List<Edge> existingFrom = adjacencyLists[v];
            existingFrom.Add(new UndirectedEdge(index, v, w));

            List<Edge> existingTo = adjacencyLists[w];
            existingTo.Add(new UndirectedEdge(index, w, v));
        }

        public override void DrawEdges()
        {
            
        }
    }
}
