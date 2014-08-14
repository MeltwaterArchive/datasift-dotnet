using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Enum
{
    /// <summary>
    /// Valid sorting options when getting lists from the API
    /// </summary>
    public enum OrderBy
    {
        [Description("updated_at")]
        UpdatedAt,
        [Description("created_at")]
        CreatedAt
    }
}
