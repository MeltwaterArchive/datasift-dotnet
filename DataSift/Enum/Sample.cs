using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Enum
{
    /// <summary>
    /// Valid sample rates when using historics
    /// </summary>
    public enum Sample
    {
        [Description("10")]
        TenPercent,
        [Description("100")]
        OneHundredPercent
    }
}
