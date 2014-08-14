using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Enum
{
    /// <summary>
    /// Valid push / subscription statuses
    /// </summary>
    public enum PushStatus
    {
        Active,
        Paused,
        [Description("waiting_for_start")]
        WaitingForStart
    }
}
