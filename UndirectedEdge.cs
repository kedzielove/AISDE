using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    sealed class UndirectedEdge : Edge
    {
        public UndirectedEdge(int id, Node v, Node w) : base(id, v, w) { }

        public Node Either() => v;

        public Node Other(Node node)
        {
            if (node == v)
                return w;
            else if (node == w)
                return v;
            else
                throw new Exception("Wrong edge");
        }
    }
}
