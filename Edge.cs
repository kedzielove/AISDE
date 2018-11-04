using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE
{
    class Edge
    {
        public Node NodeA { get; private set; }
        public Node NodeB { get; private set; } 
        public Edge(Node nodeA, Node nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public double Weight
        {
            get => Math.Sqrt(Math.Pow(NodeA.X - NodeB.X, 2) + (Math.Pow(NodeA.Y - NodeB.Y, 2)));
        }
    }
}
