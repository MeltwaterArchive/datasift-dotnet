using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class PushSubscription : PushDefinition
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
        /// Get a Push subscription by ID.
        /// </summary>
        /// <param name="user">The user who owns the subscription.</param>
        /// <param name="id">The subscription ID.</param>
        /// <returns>A PushSubscription object.</returns>
        public static PushSubscription get(User user, string id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            return new PushSubscription(user, user.callApi("push/get", parameters));
        }

        /// <summary>
        /// Get a page of Push subscriptions for the given stream hash in the
        /// given user's account, where each page contains up to per_page
        /// items. Results will be ordered according to the supplied ordering
        /// parameters.
        /// </summary>
        /// <param name="user">The user object making the request.</param>
        /// <param name="hash">The stream hash.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public static PushSubscriptionList listByStreamHash(User user, string hash, int page, int per_page, string order_by = ORDERBY_CREATED_AT, string order_dir = ORDERDIR_ASC)
        {
            return list(user, page, per_page, order_by, order_dir, false, "hash", hash);
        }

        /// <summary>
        /// Get a page of Push subscriptions for the given Historics query
        /// playback ID in the given user's account, where each page contains
        /// up to per_page items. Results will be ordered according to the
        /// supplied ordering parameters.
        /// </summary>
        /// <param name="user">The user object making the request.</param>
        /// <param name="playback_id">The playback ID.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <param name="include_finished">True to include subscriptions against finished Historics queries.</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public static PushSubscriptionList listByPlaybackId(User user, string playback_id, int page, int per_page, string order_by = ORDERBY_CREATED_AT, string order_dir = ORDERDIR_ASC, bool include_finished = false)
        {
            return list(user, page, per_page, order_by, order_dir, include_finished, "playback_id", playback_id);
        }

        /// <summary>
        /// Get a page of Push subscriptions in the given user's account,
        /// where each page contains up to per_page items. Results will be
        /// ordered according to the supplied ordering parameters.
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
        public static PushSubscriptionList list(User user, int page, int per_page, string order_by = ORDERBY_CREATED_AT, string order_dir = ORDERDIR_ASC, bool include_finished = false, string hash_type = "", string hash = "")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (hash_type.Length > 0)
            {
                if (hash_type != "hash" && hash_type != "playback_id")
                {
                    throw new InvalidDataException("Hash type is invalid");
                }
                if (hash.Length == 0)
                {
                    throw new InvalidDataException("Hash type given but the hash is empty");
                }
                parameters.Add(hash_type, hash);
            }

            if (page < 1)
            {
                throw new InvalidDataException("The specified page number is invalid");
            }

            if (per_page < 1)
            {
                throw new InvalidDataException("The specified per_page value is invalid");
            }

            if (order_by != ORDERBY_ID && order_by != ORDERBY_CREATED_AT)
            {
                throw new InvalidDataException("The specified order_by is not supported");
            }

            if (order_dir != ORDERDIR_ASC && order_dir != ORDERDIR_DESC)
            {
                throw new InvalidDataException("The specified order_dir is not supported");
            }

            parameters.Add("page", page.ToString());
            parameters.Add("per_page", per_page.ToString());
            parameters.Add("order_by", order_by);
            parameters.Add("order_dir", order_dir);

            if (include_finished)
            {
                parameters.Add("include_finished", "1");
            }

            JSONdn res = user.callApi("push/get", parameters);

            if (!res.has("count"))
            {
                throw new ApiException("No count in the response");
            }
            PushSubscriptionList retval = new PushSubscriptionList(res.getIntVal("count"));

            if (!res.has("subscriptions") && retval.TotalCount > 0)
            {
                throw new ApiException("No subscriptions in the response");
            }
            JToken[] children = res.getChildren("subscriptions");
            for (int i = 0; i < children.Length; i++)
            {
                retval.Add(new PushSubscription(user, new JSONdn(children[i])));
            }
            return retval;
        }

        /// <summary>
        /// Page through recent log entries for the given subscription ID.
        /// </summary>
        /// <param name="user">The user making the request.</param>
        /// <param name="id">The subscription ID.</param>
        /// <param name="page">Which page to fetch.</param>
        /// <param name="per_page">How many entries per page.</param>
        /// <param name="order_by">Which field to sort by.</param>
        /// <param name="order_dir">In asc[ending] or desc[ending] order.</param>
        /// <returns>A PushLog object.</returns>
        public static PushLog getLogsBySubscriptionId(User user, string id, int page, int per_page, string order_by = ORDERBY_REQUEST_TIME, string order_dir = ORDERDIR_DESC)
        {
            return getLogs(user, page, per_page, order_by, order_dir, id);
        }

        /// <summary>
        /// Page through recent Push log entries.
        /// </summary>
        /// <param name="user">The user making the request.</param>
        /// <param name="page">Which page to fetch.</param>
        /// <param name="per_page">How many entries per page.</param>
        /// <param name="order_by">Which field to sort by.</param>
        /// <param name="order_dir">In asc[ending] or desc[ending] order.</param>
        /// <param name="id">Optional subscription ID.</param>
        /// <returns>A PushLog object.</returns>
        public static PushLog getLogs(User user, int page, int per_page, string order_by = ORDERBY_REQUEST_TIME, string order_dir = ORDERDIR_DESC, string id = "")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (page < 1)
            {
                throw new InvalidDataException("The specified page number is invalid");
            }

            if (per_page < 1)
            {
                throw new InvalidDataException("The specified per_page value is invalid");
            }

            if (order_by != ORDERBY_REQUEST_TIME)
            {
                throw new InvalidDataException("The specified order_by is not supported");
            }

            if (order_dir != ORDERDIR_ASC && order_dir != ORDERDIR_DESC)
            {
                throw new InvalidDataException("The specified order_dir is not supported");
            }

            if (id.Length > 0)
            {
                parameters.Add("id", id);
            }

            parameters.Add("page", page.ToString());
            parameters.Add("per_page", per_page.ToString());
            parameters.Add("order_by", order_by);
            parameters.Add("order_dir", order_dir);

            JSONdn res = user.callApi("push/log", parameters);

            if (!res.has("count"))
            {
                throw new ApiException("No count in the response");
            }
            PushLog retval = new PushLog(res.getIntVal("count"));

            if (!res.has("log_entries") && retval.TotalCount > 0)
            {
                throw new ApiException("No log entries in the response");
            }
            JToken[] children = res.getChildren("log_entries");
            for (int i = 0; i < children.Length; i++)
            {
                retval.Add(new PushLogEntry(new JSONdn(children[i])));
            }
            return retval;
        }

        /// <summary>
        /// The subscription ID.
        /// </summary>
        protected string m_id = "";

        /// <summary>
        /// The date/time this subscription was created.
        /// </summary>
        protected DateTime m_created_at = DateTime.MinValue;

        /// <summary>
        /// The name of this subscription.
        /// </summary>
        protected string m_name = "";

        /// <summary>
        /// The current status of this subscription.
        /// </summary>
        protected string m_status = "";

        /// <summary>
        /// The hash to which this subscription is subscribed.
        /// </summary>
        protected string m_hash = "";

        /// <summary>
        /// The hash type: "stream" or "historic".
        /// </summary>
        protected string m_hash_type = "";

        /// <summary>
        /// The date/time of the last Push request.
        /// </summary>
        protected DateTime m_last_request = DateTime.MinValue;

        /// <summary>
        /// The date/time of the last successful Push request.
        /// </summary>
        protected DateTime m_last_success = DateTime.MinValue;

        /// <summary>
        /// Whether this subscription has been deleted.
        /// </summary>
        protected bool m_deleted = false;

        /// <summary>
        /// Construct a PushSubscription object from a JSON object.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="json"></param>
        public PushSubscription(User user, JSONdn json)
            : base(user)
        {
            init(json);
        }

        /// <summary>
        /// Extract data from a JSON object.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        protected void init(JSONdn json)
        {
            if (!json.has("id"))
            {
                throw new InvalidDataException("No ID found");
            }
            m_id = json.getStringVal("id");

            if (!json.has("name"))
            {
                throw new InvalidDataException("No name found");
            }
            m_name = json.getStringVal("name");

            if (!json.has("created_at"))
            {
                throw new InvalidDataException("No created at date found");
            }
            m_created_at = Utils.UnixTimestampToDateTime(json.getLongVal("created_at"));

            if (!json.has("status"))
            {
                throw new InvalidDataException("No status found");
            }
            m_status = json.getStringVal("status");

            if (!json.has("hash_type"))
            {
                throw new InvalidDataException("No hash_type found");
            }
            m_hash_type = json.getStringVal("hash_type");

            if (!json.has("hash"))
            {
                throw new InvalidDataException("No hash found");
            }
            m_hash = json.getStringVal("hash");

            if (!json.has("last_request"))
            {
                throw new InvalidDataException("No last request date found");
            }
            try
            {
                m_last_request = Utils.UnixTimestampToDateTime(json.getLongVal("last_request"));
            }
            catch (Exception)
            {
                m_last_request = DateTime.MinValue;
            }

            if (!json.has("last_success"))
            {
                throw new InvalidDataException("No last success date found");
            }
            try
            {
                m_last_success = Utils.UnixTimestampToDateTime(json.getLongVal("last_success"));
            }
            catch (Exception)
            {
                m_last_success = DateTime.MinValue;
            }

            if (!json.has("output_type"))
            {
                throw new InvalidDataException("No output type found");
            }
            m_output_type = json.getStringVal("output_type");

            if (!json.has("id"))
            {
                throw new InvalidDataException("No ID found");
            }
            m_output_params.Clear();
            m_output_params.parse(new JSONdn(json.getJVal("output_params")));
        }

        /// <summary>
        /// Reload the data for this subscription from the API.
        /// </summary>
        public void reloadData()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            init(m_user.callApi("push/get", parameters));
        }

        /// <summary>
        /// Get the ID of this subscription.
        /// </summary>
        /// <returns>The subscription ID.</returns>
        public string getId()
        {
            return m_id;
        }

        /// <summary>
        /// Get the name of this subscription.
        /// </summary>
        /// <returns>The subscription name.</returns>
        public string getName()
        {
            return m_name;
        }

        /// <summary>
        /// Set the name of this subscription. Note you must call save to
        /// store this change on the server.
        /// </summary>
        /// <param name="new_name">The new name.</param>
        public void setName(string new_name)
        {
            m_name = new_name;
        }

        /// <summary>
        /// Set the value of an output parameter, checking to make sure the
        /// subscription has not been deleted first.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        new public void setOutputParam(string key, string val)
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot modify a deleted subscription");
            }
            ((PushDefinition)this).setOutputParam(key, val);
        }

        /// <summary>
        /// Get the date/time when this subscription was created.
        /// </summary>
        /// <returns>The created_at date.</returns>
        public DateTime getCreatedAt()
        {
            return m_created_at;
        }

        /// <summary>
        /// Get the current status of this subscription.
        /// </summary>
        /// <returns>The subscription status.</returns>
        public string getStatus()
        {
            return m_status;
        }

        /// <summary>
        /// Returns true if this subscription has been deleted.
        /// </summary>
        /// <returns>True if deleted.</returns>
        public bool isDeleted()
        {
            return m_status == STATUS_DELETED;
        }

        /// <summary>
        /// Get the hash type.
        /// </summary>
        /// <returns>The hash type.</returns>
        public string getHashType()
        {
            return m_hash_type;
        }

        /// <summary>
        /// Get the hash.
        /// </summary>
        /// <returns>The hash.</returns>
        public string getHash()
        {
            return m_hash;
        }
        
        /// <summary>
        /// Get the date/time of the last Push request.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getLastRequest()
        {
            return m_last_request;
        }

        /// <summary>
        /// Get the date/time of the last successful Push request.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getLastSuccess()
        {
            return m_last_success;
        }

        /// <summary>
        /// Save changes to the name and output_paramaeters of this
        /// subscription.
        /// </summary>
        public void save()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            parameters.Add("name", getName());
            foreach (string key in m_output_params.Keys)
            {
                parameters.Add(OUTPUT_PARAMS_PREFIX + key, m_output_params[key]);
            }
            init(m_user.callApi("push/update", parameters));
        }

        /// <summary>
        /// Pause this subscription.
        /// </summary>
        public void pause()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            init(m_user.callApi("push/pause", parameters));
        }

        /// <summary>
        /// Resume this subscription.
        /// </summary>
        public void resume()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            init(m_user.callApi("push/resume", parameters));
        }

        /// <summary>
        /// Stop this subscription.
        /// </summary>
        public void stop()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            init(m_user.callApi("push/stop", parameters));
        }

        /// <summary>
        /// Delete this subscription.
        /// </summary>
        public void delete()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", getId());
            m_user.callApi("push/delete", parameters);
            // The delete API call doesn't return the object, so set the
            // status manually.
            m_status = STATUS_DELETED;
        }

        /// <summary>
        /// Page through recent log entries for this subscription.
        /// </summary>
        /// <param name="page">Which page to fetch.</param>
        /// <param name="per_page">How many entries per page.</param>
        /// <param name="order_by">Which field to sort by.</param>
        /// <param name="order_dir">In asc[ending] or desc[ending] order.</param>
        /// <returns>A PushLog object.</returns>
        public PushLog getLog(string id, int page, int per_page, string order_by = ORDERBY_REQUEST_TIME, string order_dir = ORDERDIR_DESC)
        {
            return getLogs(m_user, page, per_page, order_by, order_dir, getId());
        }
    }
}
