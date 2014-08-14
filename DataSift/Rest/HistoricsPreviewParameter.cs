using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class HistoricsPreviewParameter
    {
        public string Target { get; set; }
        public string Analysis { get; set; }
        public string Argument { get; set; }

        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.Target, this.Analysis, this.Argument);
        }
    }
}
