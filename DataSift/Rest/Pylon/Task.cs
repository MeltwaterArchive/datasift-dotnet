using RestSharp;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Pylon
{
    public class Task
    {
        DataSiftClient _client = null;

        internal Task(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get(string service, string taskId = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((taskId != null) ? taskId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((taskId != null) ? Constants.TASK_ID_FORMAT.IsMatch(taskId) : true, Messages.INVALID_TASK_ID);
            
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);
            
            if (taskId != null)
                return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/task/" + taskId, null, Method.GET);
            else
                return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/task", new { page = page, per_page = perPage }, Method.GET);

        }

        public RestAPIResponse Create(string service, string recordingId, string name, string type, dynamic parameters)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>((name != null) ? name.Trim().Length > 0 : true);
            
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentException>((type != null) ? type.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(recordingId != null);
            Contract.Requires<ArgumentException>((recordingId != null) ? recordingId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((recordingId != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(recordingId) : true, Messages.INVALID_RECORDING_ID);

            if (ReferenceEquals(null, parameters))
            {
                throw new ArgumentNullException("parameters");
            }

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/task", new { subscription_id = recordingId, name = name, type = type, parameters = parameters }, Method.POST);
        }
    }
}
