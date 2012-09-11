using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class HistoricList : ListWithTotalCount<Historic>
    {
        public HistoricList(int total_count)
            : base(total_count)
        {
        }
    }
}
