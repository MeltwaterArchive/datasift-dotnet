using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class PushSubscriptionList : ListWithTotalCount<PushSubscription>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="total_count">The total count.</param>
        public PushSubscriptionList(int total_count)
            : base(total_count)
        {
        }
    }
}
