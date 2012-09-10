using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// A Historic object represents a Historics query.
    /// </summary>
    public class Historic
    {
        /// <summary>
        /// Status: queued
        /// </summary>
        public const string STATUS_QUEUED = "queued";
        
        /// <summary>
        /// Status: running
        /// </summary>
        public const string STATUS_RUNNING = "running";

        /// <summary>
        /// Status: succeeded
        /// </summary>
        public const string STATUS_SUCCEEDED = "succeeded";

        /// <summary>
        /// Status: failed
        /// </summary>
        public const string STATUS_FAILED = "failed";

        /// <summary>
        /// Status: stopped
        /// </summary>
        public const string STATUS_STOPPED = "stopped";

        /// <summary>
        /// Status: deleted
        /// </summary>
        public const string STATUS_DELETED = "deleted";

        /// <summary>
        /// Status: killed
        /// </summary>
        public const string STATUS_KILLED = "killed";

        /// <summary>
        /// Status: init
        /// </summary>
        public const string STATUS_INIT = "init";

        /// <summary>
        /// Status: submitted
        /// </summary>
        public const string STATUS_SUBMITTED = "submitted";

        /// <summary>
        /// Status: prep
        /// </summary>
        public const string STATUS_PREP = "prep";

        /// <summary>
        /// Get a list of Historics queries in your account.
        /// </summary>
        /// <param name="user">The user making the request.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <returns>A list of Historic objects.</returns>
        public static HistoricList list(User user, int page = 1, int per_page = 20)
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("page", page.ToString());
                parameters.Add("per_page", per_page.ToString());
                JSONdn res = user.callApi("historics/get", parameters);

                if (!res.has("count"))
                {
                    throw new ApiException("No count in the response");
                }
                HistoricList retval = new HistoricList(res.getIntVal("count"));

                if (!res.has("data") && retval.getTotalCount() > 0)
                {
                    throw new ApiException("No historics in the response");
                }
                JToken[] children = res.getChildren("data");
                for (int i = 0; i < children.Length; i++)
                {
                    retval.Add(new Historic(user, new JSONdn(children[i])));
                }
                return retval;
            }
            catch (ApiException e)
            {
                if (e.Code == 400)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        /// <summary>
        /// The User object that owns this Historics query.
        /// </summary>
        private User m_user = null;

        /// <summary>
        /// The ID of this Historics query.
        /// </summary>
        private string m_playback_id = String.Empty;

        /// <summary>
        /// The stream hash which this Historics query is executing.
        /// </summary>
        private string m_stream_hash = String.Empty;

        /// <summary>
        /// The friendly name for this Historics query.
        /// </summary>
        private string m_name = String.Empty;

        /// <summary>
        /// The start date for this Historics query.
        /// </summary>
		private DateTime m_start_date = DateTime.MinValue;

        /// <summary>
        /// The end date for this Historics query.
        /// </summary>
		private DateTime m_end_date = DateTime.MinValue;

        /// <summary>
        /// The date/time when this Historics query was created.
        /// </summary>
		private DateTime m_created_at = DateTime.MinValue;

        /// <summary>
        /// The current status of this Historics query.
        /// </summary>
		private string m_status = "created";

        /// <summary>
        /// The current progress in percent of this Historics query.
        /// </summary>
		private int m_progress = 0;

        /// <summary>
        /// The data sources for which this Historics query is looking.
        /// </summary>
		private List<string> m_sources = new List<string>();

        /// <summary>
        /// The sample percentage that this Historics query will match.
        /// </summary>
		private double m_sample = 100.0;

        /// <summary>
        /// The DPU cost of running this Historics query.
        /// </summary>
		private double m_dpus = 0;

        /// <summary>
        /// The data availability information for this query. Filled in by the prepare method.
        /// </summary>
        private HistoricDataAvailability m_availability = null;

        /// <summary>
        /// The data availability for this Historics query. 
        /// </summary>
		private Dictionary<string,int> m_volume_info = null;

        /// <summary>
        /// Constructor. Creates a Historic object by fetching an existing query from the API.
        /// </summary>
        /// <param name="user">The User creating the object.</param>
        /// <param name="playback_id">The Historics query playback ID.</param>
        public Historic(User user, string playback_id)
        {
            m_user = user;
            m_playback_id = playback_id;
            reloadData();
        }

        /// <summary>
        /// Constrcutor. Creates a Historic object from the data contained within a JSON object.
        /// </summary>
        /// <param name="user">The User creating the object.</param>
        /// <param name="data">The JSON data.</param>
        public Historic(User user, JSONdn data)
        {
            m_user = user;
            init(data);
        }

        /// <summary>
        /// Constructor. All parameters are required.
        /// </summary>
        /// <param name="user">The user creating the Historic object.</param>
        /// <param name="hash">The stream hash that this Historic will use.</param>
        /// <param name="start_date">The start date for the query.</param>
        /// <param name="end_date">The end date for the query.</param>
        /// <param name="sources">An array of data sources for the query.</param>
        /// <param name="sample">The sample rate for the query.</param>
        /// <param name="name">A friendly name for the query.</param>
        public Historic(User user, string hash, DateTime start_date, DateTime end_date, List<string> sources, double sample, string name)
        {
            m_user        = user;
			m_stream_hash = hash;
			m_start_date  = start_date;
			m_end_date    = end_date;
			m_sources     = sources;
			m_name        = name;
			m_sample      = sample;
			m_progress    = 0;
        }

        /// <summary>
        /// Reload the data for this Historics query from the API.
        /// </summary>
        public void reloadData()
        {
            // Can't do this if we've been deleted.
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot reload the data for a deleted Historics query");
            }

            // Can't do this without a playback ID.
            if (m_playback_id.Length == 0)
            {
                throw new InvalidDataException("Cannot reload the data for a Historics query with no playback ID.");
            }

            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("id", m_playback_id);
            try
            {
                init(m_user.callApi("historics/get", parameters));
            }
            catch (ApiException e)
            {
                if (e.Code == 400)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        /// <summary>
        /// Initialise this object from teh data in a JSON object.
        /// </summary>
        /// <param name="data">The JSON object.</param>
        public void init(JSONdn data)
        {
            if (!data.has("id"))
            {
                throw new ApiException("No playback ID in the response");
            }
            if (m_playback_id.Length > 0 && m_playback_id.CompareTo(data.getStringVal("id")) != 0)
            {
                throw new ApiException("Incorrect playback ID in the response");
            }
            m_playback_id = data.getStringVal("id");

            if (!data.has("definition_id"))
            {
                throw new ApiException("No definition hash in the response");
            }
            m_stream_hash = data.getStringVal("definition_id");

            if (!data.has("name"))
            {
                throw new ApiException("No name in the response");
            }
            m_name = data.getStringVal("name");

            if (!data.has("start"))
            {
                throw new ApiException("No start timestamp in the response");
            }
            m_start_date = data.getDateTimeFromLongVal("start");

            if (!data.has("end"))
            {
                throw new ApiException("No end timestamp in the response");
            }
            m_end_date = data.getDateTimeFromLongVal("end");

            if (!data.has("created_at"))
            {
                throw new ApiException("No created at timestamp in the response");
            }
            m_created_at = data.getDateTimeFromLongVal("created_at");

            if (!data.has("status"))
            {
                throw new ApiException("No status in the response");
            }
            m_status = data.getStringVal("status");

            if (!data.has("progress"))
            {
                throw new ApiException("No progress in the response");
            }
            m_progress = data.getIntVal("progress");

            if (!data.has("sources"))
            {
                throw new ApiException("No sources in the response");
            }
            m_sources.Clear();
            foreach (JToken source in data.getJVal("sources"))
            {
                m_sources.Add(source.ToString());
            }

            if (!data.has("sample"))
            {
                throw new ApiException("No sample in the response");
            }
            m_sample = data.getDoubleVal("sample");

            if (!data.has("volume_info"))
            {
                throw new ApiException("No volume_info in the response");
            }
            m_volume_info.Clear();
            foreach (string key in data.getKeys("volume_info"))
            {
                m_volume_info.Add(key, data.getIntVal("volume_info." + key));
            }
        }

        /// <summary>
        /// Returns whether this query has been deleted.
        /// </summary>
        /// <returns>True if it has been deleted.</returns>
        public bool isDeleted()
        {
            return m_status == STATUS_DELETED;
        }

        /// <summary>
        /// Get the start date.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getStartDate()
        {
            return m_start_date;
        }

        /// <summary>
        /// Get the end date.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getEndDate()
        {
            return m_end_date;
        }

        /// <summary>
        /// Get the date/time that this query was created.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getCreatedAtDate()
        {
            return m_created_at;
        }

        /// <summary>
        /// Get the data sources this Historics query will be examining.
        /// </summary>
        /// <returns>A List of strings.</returns>
        public List<string> getSources()
        {
            return m_sources;
        }

        /// <summary>
        /// Gets the current progress of this query. To update this from the
        /// API call reloadData().
        /// </summary>
        /// <returns>An integer percentage.</returns>
        public int getProgress()
        {
            return m_progress;
        }
        
        /// <summary>
        /// Gets the sample rate for this query.
        /// </summary>
        /// <returns>The percentage sample rate.</returns>
        public double getSample()
        {
            return m_sample;
        }

        /// <summary>
        /// Gets the current status of this query. To update this from the
        /// API call reloadData().
        /// </summary>
        /// <returns></returns>
        public string getStatus()
        {
            return m_status;
        }

        /// <summary>
        /// Get the data volume information for this query.
        /// </summary>
        /// <returns>A Dictionary of string => int.</returns>
        public Dictionary<string, int> getVolumeInfo()
        {
            return m_volume_info;
        }

        /// <summary>
        /// Get the DPU cost of the query.
        /// </summary>
        /// <returns>The DPU cost.</returns>
        public double getDPUs()
        {
            return m_dpus;
        }

        /// <summary>
        /// Get the playback ID. If the Historics query has not yet been
        /// prepared that will be done automagically to obtain the playback ID.
        /// </summary>
        /// <returns>The playback ID.</returns>
        public string getHash()
        {
            if (m_playback_id.Length == 0)
            {
                prepare();
            }
            return m_playback_id;
        }

        /// <summary>
        /// Get the stream hash that this query is running.
        /// </summary>
        /// <returns>The stream hash.</returns>
        public string getStreamHash()
        {
            return m_stream_hash;
        }

        /// <summary>
        /// Get the friendly name for this query.
        /// </summary>
        /// <returns>The name.</returns>
        public string getName()
        {
            return m_name;
        }

        /// <summary>
        /// Set the friendly name of this Historics query. If the query has been prepared the change will be sent to the API.
        /// </summary>
        /// <param name="new_name">The new name.</param>
        public void setName(string new_name)
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot set the name of a deleted Historics query");
            }
            
            if (m_playback_id.Length == 0)
            {
                // Not yet prepared, change locally only.
                m_name = new_name;
            }
            else
            {
                // Prepared, send the change to the API.
                Dictionary<string,string> parameters = new Dictionary<string,string>();
                parameters.Add("id", m_playback_id);
                parameters.Add("name", new_name);
                m_user.callApi("historics/update", parameters);
                reloadData();
            }
        }

        /// <summary>
        /// Prepare this Historics query.
        /// </summary>
        public void prepare()
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot set the name of a deleted Historics query");
            }
            if (m_playback_id.Length > 0)
            {
                throw new InvalidDataException("This Historics query has already been prepared");
            }

            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("hash", m_stream_hash);
            parameters.Add("start", Utils.DateTimeToUnixTimestamp(m_start_date).ToString());
            parameters.Add("end", Utils.DateTimeToUnixTimestamp(m_end_date).ToString());
            parameters.Add("name", m_name);
            parameters.Add("sources", string.Join(",", m_sources.ToArray()));
            try
            {
                JSONdn res = m_user.callApi("historics/prepare", parameters);

                if (!res.has("id"))
                {
                    throw new InvalidDataException("Prepared successfully but no playback ID in the response");
                }
                m_playback_id = res.getStringVal("id");

                if (!res.has("dpus"))
                {
                    throw new InvalidDataException("Prepared successfully but no DPU cost in the response");
                }
                m_dpus = res.getDoubleVal("dpus");

                if (!res.has("availability"))
                {
                    throw new InvalidDataException("Prepared successfully but no availability in the response");
                }
                m_availability = new HistoricDataAvailability(new JSONdn(res.getJVal("availability")));
            }
            catch (ApiException e)
            {
                // 400 = Missing or bad parameters.
                // 404 = Historics query not found.
                if (e.Code == 400 || e.Code == 404)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }

            // Update our data.
            reloadData();
        }

        /// <summary>
        /// Start this Historics query.
        /// </summary>
        public void start()
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot start a deleted Historics query");
            }
            if (m_playback_id.Length == 0)
            {
                throw new InvalidDataException("Cannot start a Historics query that has not been prepared");
            }

            try
            {
                Dictionary<string,string> parameters = new Dictionary<string,string>();
                parameters.Add("id", m_playback_id);
                m_user.callApi("historics/start", parameters);
            }
            catch (ApiException e)
            {
                // 400 = Missing or bad parameters.
                // 404 = Historics query not found.
                if (e.Code == 400 || e.Code == 404)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        /// <summary>
        /// Stop this Historics query.
        /// </summary>
        public void stop()
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot stop a deleted Historics query");
            }
            if (m_playback_id.Length == 0)
            {
                throw new InvalidDataException("Cannot stop a Historics query that has not been prepared");
            }

            try
            {
                Dictionary<string,string> parameters = new Dictionary<string,string>();
                parameters.Add("id", m_playback_id);
                m_user.callApi("historics/stop", parameters);
            }
            catch (ApiException e)
            {
                // 400 = Missing or bad parameters.
                // 404 = Historics query not found.
                if (e.Code == 400 || e.Code == 404)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        /// <summary>
        /// Delete this Historics query.
        /// </summary>
        public void delete()
        {
            if (isDeleted())
            {
                throw new InvalidDataException("Cannot delete a deleted Historics query");
            }
            if (m_playback_id.Length == 0)
            {
                throw new InvalidDataException("Cannot delete a Historics query that has not been prepared");
            }

            try
            {
                Dictionary<string,string> parameters = new Dictionary<string,string>();
                parameters.Add("id", m_playback_id);
                m_user.callApi("historics/delete", parameters);
                m_status = STATUS_DELETED;
            }
            catch (ApiException e)
            {
                // 400 = Missing or bad parameters.
                // 404 = Historics query not found.
                if (e.Code == 400 || e.Code == 404)
                {
                    throw new InvalidDataException(e.Message);
                }
                throw new ApiException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        /// <summary>
        /// Get a page of Push subscriptions for this Historics query, where each page contains up to per_page items. Results will be returned in the order requested.
        /// </summary>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public PushSubscriptionList getPushSubscriptions(int page = 1, int per_page = 20, string order_by = PushSubscription.ORDERBY_CREATED_AT, string order_dir = PushSubscription.ORDERDIR_ASC)
        {
            return PushSubscription.list(m_user, page, per_page, order_by, order_dir, true, "playback_id", m_playback_id);
        }
    }
}
