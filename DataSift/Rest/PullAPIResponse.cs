using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class PullAPIResponse : RestAPIResponse
    {
        public PullInfo PullDetails { get; set; }
    }

    public class PullInfo
    {
        public string CursorNext { get; set; }
        public string CursorCurrent { get; set; }
        public string Format { get; set; }
    }
}
