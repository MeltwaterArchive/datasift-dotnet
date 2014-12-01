using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift
{
    internal class Messages
    {
        internal const string INVALID_APIKEY = "API key should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_STREAM_HASH = "Hash should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_SUBSCRIPTION_ID = "Subscription ID should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_CURSOR = "Cursor should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_HISTORICS_ID = "Historics ID should be a 20 character string of lower-case letters and numbers";
        internal const string INVALID_HISTORICS_PREVIEW_ID = "Preview ID should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_SOURCE_ID = "Source ID should be a 32 character string of lower-case letters and numbers";
        internal const string INVALID_LIST_ID = "List ID is not in the correct format";

        internal const string HISTORICS_END_TOO_LATE = "End must be at least one hour ago";
        internal const string HISTORICS_START_TOO_LATE = "Start must be at least two hours ago";
        internal const string HISTORICS_START_TOO_EARLY = "Start cannot be before 2010";
        internal const string HISTORICS_START_MUST_BE_BEFORE_END = "Start date must be before end date";
        internal const string HISTORICS_END_CANNOT_BE_IN_FUTURE = "End cannot be in the future";

        internal const string PUSH_MUST_PROVIDE_HASH_OR_HISTORIC = "You must provide either a hash or historicsId";
        internal const string PUSH_ONLY_HASH_OR_HISTORIC = "You cannot specify both a hash AND historicsId";

        internal const string UNRECOGNISED_DATA_FORMAT = "Unrecognised serialization format for data";

        internal const string ANALYSIS_START_TOO_LATE = "Start cannot be in the future";
        internal const string ANALYSIS_END_TOO_LATE = "End cannot be in the future";
        internal const string ANALYSIS_START_MUST_BE_BEFORE_END = "Start date must be before end date";
    }
}
