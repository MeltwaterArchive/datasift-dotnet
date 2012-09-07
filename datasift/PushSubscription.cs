using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class PushSubscription
    {
        /// <summary>
        /// Constant for the stream hash type.
        /// </summary>
		public const string HASH_TYPE_STREAM = "stream";
        
        /// <summary>
        /// Constant for the historic hash type.
        /// </summary>
        public const string HASH_TYPE_HISTORIC = "historic";

        /// <summary>
        /// Constant for the "active" status.
        /// </summary>
        public const string STATUS_ACTIVE = "active";
        /// <summary>
        /// Constant for the "paused" status.
        /// </summary>
        public const string STATUS_PAUSED = "paused";
        /// <summary>
        /// Constant for the "stopped" status.
        /// </summary>
        public const string STATUS_STOPPED = "stopped";
        /// <summary>
        /// Constant for the "finishing" status.
        /// </summary>
        public const string STATUS_FINISHING = "finishing";
        /// <summary>
        /// Constant for the "finished" status.
        /// </summary>
        public const string STATUS_FINISHED = "finished";
        /// <summary>
        /// Constant for the "failed" status.
        /// </summary>
        public const string STATUS_FAILED = "failed";
        /// <summary>
        /// Constant for the "deleted" status.
        /// </summary>
        public const string STATUS_DELETED = "deleted";

        /// <summary>
        /// Constant for the order by ID option.
        /// </summary>
        public const string ORDERBY_ID = "id";
        /// <summary>
        /// Constant for the order by created_at option.
        /// </summary>
        public const string ORDERBY_CREATED_AT = "created_at";
        /// <summary>
        /// Constant for the order by request_time option.
        /// </summary>
        public const string ORDERBY_REQUEST_TIME = "request_time";

        /// <summary>
        /// Constant for ascending order option.
        /// </summary>
        public const string ORDERDIR_ASC = "asc";
        /// <summary>
        /// Constnat for the descending order option.
        /// </summary>
        public const string ORDERDIR_DESC = "desc";

        /// <summary>
        /// Get a page of Push subscriptions in the given user's account, where each page contains up to per_page items. Results will be ordered according to the supplied ordering parameters.
        /// </summary>
        /// <param name="user">The user object making the request.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <param name="include_finished">True to include subscriptions against finished Historics queries.</param>
        /// <param name="hash_type">Optional hash type to look for (hash is also required)</param>
        /// <param name="hash">Optional hash to look for (hash_type is also required)</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public static PushSubscriptionList list(User user, int page, int per_page, string order_by, string order_dir, bool include_finished = false, string hash_type = "", string hash = "")
        {
            throw new NotImplementedException();
        }
    }
}
