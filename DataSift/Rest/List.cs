using DataSift.Enum;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class List
    {
        DataSiftClient _client = null;

        internal List(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get()
        {
            return _client.GetRequest().Request("list/get");
        }

        public RestAPIResponse Create(ListType type, string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);

            return _client.GetRequest().Request("list/create", new { type = type, name = name }, Method.POST);
        }
        public RestAPIResponse Delete(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);

            return _client.GetRequest().Request("list/delete", new { id = id }, Method.DELETE);
        }

        public RestAPIResponse Add<T>(string id, T[] items)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);
            Contract.Requires<ArgumentException>(typeof(T) == typeof(string) || typeof(T) == typeof(int));
            Contract.Requires<ArgumentException>(items.Length > 0);
            Contract.Requires<ArgumentException>(items.Length <= 1000);

            return _client.GetRequest().Request("list/add", new { id = id, items = items }, Method.POST);
        }

        public RestAPIResponse Remove<T>(string id, T[] items)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);
            Contract.Requires<ArgumentException>(typeof(T) == typeof(string) || typeof(T) == typeof(int));
            Contract.Requires<ArgumentException>(items.Length > 0);

            return _client.GetRequest().Request("list/remove", new { id = id, items = items }, Method.POST);
        }

        public RestAPIResponse Exists<T>(string id, T[] items)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);
            Contract.Requires<ArgumentException>(typeof(T) == typeof(string) || typeof(T) == typeof(int));
            Contract.Requires<ArgumentException>(items.Length > 0);

            return _client.GetRequest().Request("list/exists", new { id = id, items = items }, Method.POST);
        }
        public RestAPIResponse ReplaceStart(string listId)
        {
            Contract.Requires<ArgumentNullException>(listId != null);
            Contract.Requires<ArgumentException>(listId.Trim().Length > 0);
            Contract.Requires<ArgumentException>((listId != null) ? Constants.LIST_ID_FORMAT.IsMatch(listId) : true, Messages.INVALID_LIST_ID);

            return _client.GetRequest().Request("list/replace/start", new { list_id = listId }, Method.POST);
        }

        public RestAPIResponse ReplaceAbort(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);

            return _client.GetRequest().Request("list/replace/abort", new { id = id }, Method.POST);
        }

        public RestAPIResponse ReplaceCommit(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);

            return _client.GetRequest().Request("list/replace/commit", new { id = id }, Method.POST);
        }

        public RestAPIResponse ReplaceAdd<T>(string id, T[] items)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.LIST_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_LIST_ID);
            Contract.Requires<ArgumentException>(typeof(T) == typeof(string) || typeof(T) == typeof(int));
            Contract.Requires<ArgumentException>(items.Length > 0);
            Contract.Requires<ArgumentException>(items.Length <= 1000);

            return _client.GetRequest().Request("list/replace/add", new { id = id, items = items }, Method.POST);
        }

    }
}
