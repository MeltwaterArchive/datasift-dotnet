using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Enum
{
    /// <summary>
    /// All valid sort directions
    /// </summary>
    public enum OrderDirection
    {
        [Description("asc")]
        Ascending,
        [Description("desc")]
        Descending
    }
}
