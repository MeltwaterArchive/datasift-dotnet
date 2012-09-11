using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class ListWithTotalCount<T> : List<T>
    {
        protected int m_total_count = 0;

        public ListWithTotalCount(int total_count)
        {
            m_total_count = total_count;
        }

        public int TotalCount { get { return m_total_count; } }
    }
}
