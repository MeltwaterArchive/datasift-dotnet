using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    class Utils
    {
        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime UnixTimestampToDateTime(long timestamp)
        {
            return UnixEpoch.AddSeconds(timestamp);
        }

        public static long DateTimeToUnixTimestamp(DateTime dt)
        {
            return (long)(dt - UnixEpoch).TotalSeconds;
        }
    }
}
