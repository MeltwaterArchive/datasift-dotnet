using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataSift
{
    public class Constants
    {
        public static Regex APIKEY_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex STREAM_HASH_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex SUBSCRIPTION_ID_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex CURSOR_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex HISTORICS_ID_FORMAT = new Regex(@"[a-z0-9]{20}");
        public static Regex HISTORICS_PREVIEW_ID_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex SOURCE_ID_FORMAT = new Regex(@"[a-z0-9]{32}");
        public static Regex LIST_ID_FORMAT = new Regex(@"[a-z0-9]{10}_[a-z0-9]{4}_[a-z0-9]{4}_[a-z0-9]{4}_[a-z0-9]{12}");

        public const string DATA_FORMAT_META_PLUS_INTERACTIONS = "json_meta";
        public const string DATA_FORMAT_ARRAY_INTERACTIONS = "json_array";
        public const string DATA_FORMAT_NEWLINE_INTERACTIONS = "json_new_line";

        public const string HEADER_RATELIMIT_LIMIT = "X-RateLimit-Limit";
        public const string HEADER_RATELIMIT_REMAINING = "X-RateLimit-Remaining";
        public const string HEADER_RATELIMIT_COST = "X-RateLimit-Cost";
        public const string HEADER_DATA_FORMAT = "X-DataSift-Format";
        public const string HEADER_CURSOR_CURRENT = "X-DataSift-Cursor-Current";
        public const string HEADER_CURSOR_NEXT = "X-DataSift-Cursor-Next";
    }
}
