using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    class Node : IEquatable<Node>
    {
        /* Readonly vs Private Field
         * Readonly variables are set once(e.g. in a constructor) and can never be modified.
         * Private fields can be modified, but only from the inside of the class.
         */
        public readonly int id;
        public readonly int x;
        public readonly int y;

        public Node(int id, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }

        #region IEquatable 
        public bool Equals(Node node)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(node, null))
                return false;

            // Optimization for a reflexivity case
            if (Object.ReferenceEquals(this, node))
                return true;

            return (x == node.x) && (y == node.y);
        }

        public override bool Equals(object obj) => Equals(obj as Node);

        public static bool operator ==(Node lhs, Node rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if(Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Node lhs, Node rhs) => !(lhs == rhs);

        public override int GetHashCode() => new Tuple<int, int>(x, y).GetHashCode();
        #endregion
    }
}
