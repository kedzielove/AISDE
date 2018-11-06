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

        public int V { get; protected set; }
        public int E { get; protected set; }

        public Graph() => adj = new Dictionary<Node, List<Edge>>();

        public void AddEdge(int i, Node from, Node to)
        {
            List<Edge> existing;
            if (!adj.TryGetValue(from, out existing))
            {
                existing = new List<Edge>();
                adj.Add(from, existing);
            }

            existing.Add(new Edge(i, from, to));
        }

        public IEnumerable<Node> Neighbors(Node v) => (IEnumerable<Node>)adj[v];
    }
}
