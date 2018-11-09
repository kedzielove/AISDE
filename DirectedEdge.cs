using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    sealed class DirectedEdge : Edge
    {
        public DirectedEdge(int id, Node v, Node w) : base(id, v, w) { }

        public Node From() => v;

        public Node To() => w;
    }
}
