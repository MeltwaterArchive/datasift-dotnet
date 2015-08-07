using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSift.Rest;
using RestSharp;
using DataSift.Enum;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using DataSift.Streaming;
using WebSocket4Net;
using System.Diagnostics;
using DataSift.Rest.Account;

namespace DataSift
{
    public class DataSiftClient
    {
        private string _username;
        private string _apikey;
        private string _baseUrl = "https://api.datasift.com/";
        private string _apiVersion = "1.2";
        private GetAPIRequestDelegate _getRequest;
        private GetStreamConnectionDelegate _getConnection;
        private Historics _historics;
        private HistoricsPreview _historicsPreview;
        private Source _source;
        private Push _push;
        private Pylon _pylon;
        private Account _account;
        public delegate IRestAPIRequest GetAPIRequestDelegate(string username, string apikey, string baseUrl, string apiVersion);
        public delegate IStreamConnection GetStreamConnectionDelegate(string url);

        public DataSiftClient(string username, string apikey, GetAPIRequestDelegate requestCreator = null, GetStreamConnectionDelegate connectionCreator = null, string baseUrl = null, string apiVersion = null)
        {
            Contract.Requires<ArgumentNullException>(username != null);
            Contract.Requires<ArgumentException>(username.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(apikey != null);
            Contract.Requires<ArgumentException>(apikey.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.APIKEY_FORMAT.IsMatch(apikey), Messages.INVALID_APIKEY);

            _username = username;
            _apikey = apikey;

            if(!String.IsNullOrEmpty(baseUrl))
                _baseUrl = baseUrl;

            if (!String.IsNullOrEmpty(apiVersion))
                _apiVersion = apiVersion;

            if (requestCreator == null)
                _getRequest = GetRequestDefault;
            else
                _getRequest = requestCreator;

            _getConnection = connectionCreator;
        }

        #region Mocking / Faking

        private IRestAPIRequest GetRequestDefault(string username, string apikey, string baseUrl, string apiVersion)
        {
            return new RestAPIRequest(username, apikey, baseUrl, apiVersion);
        }

        internal IRestAPIRequest GetRequest()
        {
            return _getRequest(_username, _apikey, _baseUrl,_apiVersion);
        }

        #endregion

        #region Properties

        public Source Source
        {
            get
            {
                if (_source == null) _source = new Source(this);
                return _source;
            }
        }

        public Pylon Pylon
        {
            get
            {
                if (_pylon == null) _pylon = new Pylon(this);
                return _pylon;
            }
        }

        public Historics Historics
        {
            get
            {
                if (_historics == null) _historics = new Historics(this);
                return _historics;
            }
        }

        public Push Push
        {
            get
            {
                if (_push == null) _push = new Push(this);
                return _push;
            }
        }

        public HistoricsPreview HistoricsPreview
        {
            get
            {
                if (_historicsPreview == null) _historicsPreview = new HistoricsPreview(this);
                return _historicsPreview;
            }
        }

        public Account Account
        {
            get
            {
                if (_account == null) _account = new Account(this);
                return _account;
            }
        }

        #endregion

        #region Streaming

        public DataSiftStream Connect(bool secure = true, string domain = "stream.datasift.com", bool autoReconnect = true)
        {
            var stream = new DataSiftStream(_getConnection, domain, autoReconnect);
            stream.Connect(_username, _apikey, secure);
            return stream;
        }

        #endregion

        #region Core API Endpoints

        public RestAPIResponse Validate(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return GetRequest().Request("validate", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Compile(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return GetRequest().Request("compile", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Usage(UsagePeriod? period = null)
        {
            return GetRequest().Request("usage", new { period = period.ToString().ToLower() });
        }

        public RestAPIResponse DPU(string hash = null, string historicsId = null)
        {
            Contract.Requires<ArgumentException>(hash != null || historicsId != null, Messages.PUSH_MUST_PROVIDE_HASH_OR_HISTORIC);
            Contract.Requires<ArgumentException>(hash == null || historicsId == null, Messages.PUSH_ONLY_HASH_OR_HISTORIC);
            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);
            Contract.Requires<ArgumentException>((historicsId != null) ? historicsId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((historicsId != null) ? Constants.HISTORICS_ID_FORMAT.IsMatch(historicsId) : true, Messages.INVALID_HISTORICS_ID);

            return GetRequest().Request("dpu", new { hash = hash, historics_id = historicsId });
        }

        public RestAPIResponse Balance()
        {
            return GetRequest().Request("balance");
        }

        public PullAPIResponse Pull(string id, int? size = null, string cursor = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);
            Contract.Requires<ArgumentException>((size.HasValue) ? size > 0: true);
            Contract.Requires<ArgumentException>((cursor != null) ? cursor.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((cursor != null) ? Constants.CURSOR_FORMAT.IsMatch(cursor) : true, Messages.INVALID_CURSOR);

            return (PullAPIResponse)GetRequest().Request("pull", new { id = id, size = size, cursor = cursor });
        }

        #endregion

    }
}
