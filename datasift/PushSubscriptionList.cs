using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class PushSubscriptionList : List<PushSubscription>
    {
        /// <summary>
        /// The total number of matches.
        /// </summary>
        private int m_total_count = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="total_count">The total count.</param>
        public PushSubscriptionList(int total_count)
        {
            m_total_count = total_count;
        }

        /// <summary>
        /// Return the total count.
        /// </summary>
        /// <returns>The total count.</returns>
        public int getTotalCount()
        {
            return m_total_count;
        }
    }
}
