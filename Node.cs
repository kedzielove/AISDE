using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE
{
    class Node : IEquatable<Node>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int ID { get; private set; }

        public Node(int x, int y, int id)
        {
            X = x;
            Y = y;
            ID = id;
        }

        #region IEquatable 
        public override bool Equals(object obj) => Equals(obj as Node);

        public bool Equals(Node node)
        {
            //If parameter is null, return false
            if (Object.ReferenceEquals(node, null))
            {
                return false;
            }

            //Optimization for reflexivity 
            if(Object.ReferenceEquals(this, node))
            {
                return true;
            }

            return (X == node.X) && (Y == node.Y);
        }

        public static bool operator ==(Node lhs, Node rhs)
        {
            if(Object.ReferenceEquals(lhs, null))
            {
                return Object.ReferenceEquals(rhs, null);
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Node lhs, Node rhs) => !(lhs == rhs);
      
        public override int GetHashCode()
        {
            return new Tuple<int, int>(X, Y).GetHashCode();
        }
        #endregion

    }
}
