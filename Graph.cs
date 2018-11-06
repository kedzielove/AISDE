using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE
{
    abstract class Graph
    {
        protected Dictionary<Node, List<Edge>> adj;

        public int NodesCount { get; protected set; }
        public int EdgesCount { get; protected set; }

        public Graph() => adj = new Dictionary<Node, List<Edge>>();

        public void AddEdge(int index, Node from, Node to)
        {
            List<Edge> existing;
            if (!adj.TryGetValue(from, out existing))
            {
                existing = new List<Edge>();
                adj.Add(from, existing);
            }

            existing.Add(new Edge(index, from, to));
        }

        public IEnumerable<Node> Neighbors(Node v) => (IEnumerable<Node>)adj[v];

        public IEnumerable<Edge> Edges
        {
            get
            {
                List<Edge> edges = new List<Edge>();
                foreach(var node in adj.Keys)
                {
                    foreach(var edge in adj[node])
                    {
                        if (edge.w.id > edge.v.id)
                            edges.Add(edge);
                    }
                }

                return edges;
            }
        }
    }
}
