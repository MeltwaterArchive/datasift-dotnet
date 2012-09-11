using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class PushLog : ListWithTotalCount<PushLogEntry>
    {
        public PushLog(int total_count)
            : base(total_count)
        {
        }
    }
}
