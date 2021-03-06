﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEG
{
    abstract class Edge : IComparable<Edge>
    {
        public readonly int id;
        public readonly Node v;
        public readonly Node w;

        public double Weight
        {
            get => Math.Sqrt(Math.Pow(v.x - w.x, 2) + (Math.Pow(v.y - w.y, 2)));
        }

        public Edge(int id, Node v, Node w)
        {
            this.id = id;
            this.v = v;
            this.w = w;
        }

        /* IComparable interface implementation
         * Compares (smaller, greater, equals) values of this and other p(Weight) property
         */
        #region IComparable
        public int CompareTo(Edge other)
        {
            if (Object.ReferenceEquals(other, null))
                return -1;

            return Weight.CompareTo(other.Weight);
        }
        #endregion
    }
}
