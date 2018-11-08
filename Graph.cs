using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AISDEG
{
    abstract class Graph
    {
        protected Dictionary<Node, List<Edge>> adjacencyLists;
        public int NodesCount { get; protected set; }
        public int EdgesCount { get; protected set; }

        public Graph() => adjacencyLists = new Dictionary<Node, List<Edge>>();

        public List<Edge> this[Node key]
        {
            get => adjacencyLists[key];
        }

        public List<Node> Nodes() => adjacencyLists.Keys.ToList();

        public void AddEdge(int index, Node from, Node to)
        {
            List<Edge> existing;
            if (!adjacencyLists.TryGetValue(from, out existing))
            {
                existing = new List<Edge>();
                adjacencyLists.Add(from, existing);
            }

            existing.Add(new Edge(index, from, to));
        }

        abstract public void DrawEdges();

        public void DrawNodes(Graphics graphics)
        {
            graphics.TranslateTransform(100, 100);
            graphics.ScaleTransform(20, 20);

            float radius = 1;
            foreach(var node in adjacencyLists.Keys)
            {
                graphics.FillEllipse(Brushes.LimeGreen, node.x, node.y, 2*radius, 2*radius);
            }
        }
    }
}
