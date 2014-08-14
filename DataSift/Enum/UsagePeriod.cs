using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Enum
{
    /// <summary>
    /// Valid usage periods, when using the /usage API endpoint
    /// </summary>
    public enum UsagePeriod
    {
        Day,
        Hour,
        Current
    }
}
