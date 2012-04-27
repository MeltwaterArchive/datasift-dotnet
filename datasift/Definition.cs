using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class Definition
    {
        private User m_user = null;
        private string m_hash = "";
        private DateTime m_created_at = DateTime.MinValue;
        private double m_total_dpu = -1;
        private string m_csdl = "";

        public Definition(User u, string csdl = "", string hash = "")
        {
            m_user = u;
            m_hash = hash;
            set(csdl);
        }

        public User getUser()
        {
            return m_user;
        }

        public string get()
        {
            return m_csdl;
        }

        public void set(string csdl)
        {
            csdl = csdl.Trim();

            if (m_csdl != csdl)
            {
                m_csdl = csdl;
                clearHash();
            }
        }

        public string getHash()
        {
            if (m_hash.Length == 0)
            {
                compile();
            }

            return m_hash;
        }

        public void clearHash()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot clear the hash of a hash-only definition object");
            }

            m_hash = "";
            m_created_at = DateTime.MinValue;
            m_total_dpu = -1;
        }

        public DateTime getCreatedAt()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Created at date not available");
            }

            if (m_created_at == DateTime.MinValue)
            {
                validate();
            }

            return m_created_at;
        }

        public double getTotalDpu()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Created at date not available");
            }
            if (m_total_dpu == -1)
            {
                validate();
            }
            return m_total_dpu;
        }

        public void compile()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot compile an empty definition");
            }

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("csdl", m_csdl);
                JSONdn res = m_user.callApi("compile", parameters);

                if (!res.has("hash"))
                {
                    throw new CompileFailedException("Compiled successfully but no hash in the response");
                }
                m_hash = res.getStringVal("hash");

                if (!res.has("created_at"))
                {
                    throw new CompileFailedException("Compiled successfully but no created_at in the response");
                }
                m_created_at = res.getDateTimeVal("created_at", "yyyy-MM-dd HH:mm:ss");

                if (!res.has("dpu"))
                {
                    throw new CompileFailedException("Compiled successfully but no DPU in the response");
                }
                m_total_dpu = res.getDoubleVal("dpu");
            }
            catch (ApiException e)
            {
                clearHash();
                if (e.Code == 400)
                {
                    throw new CompileFailedException(e.Message);
                }
                throw new CompileFailedException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        public void validate()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot validate an empty definition");
            }

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("csdl", m_csdl);
                JSONdn res = m_user.callApi("validate", parameters);

                if (!res.has("created_at"))
                {
                    throw new CompileFailedException("Validated successfully but no created_at in the response");
                }
                m_created_at = res.getDateTimeVal("hash", "yyyy-MM-dd HH:mm:ss");

                if (!res.has("dpu"))
                {
                    throw new CompileFailedException("Validated successfully but no DPU in the response");
                }
                m_total_dpu = res.getDoubleVal("dpu");
            }
            catch (ApiException e)
            {
                clearHash();
                if (e.Code == 400)
                {
                    throw new CompileFailedException(e.Message);
                }
                throw new CompileFailedException("Unexpected API response code: " + e.Code.ToString() + " " + e.Message);
            }
        }

        public Dpu getDpuBreakdown()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot get the DPU breakdown for an empty definition");
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("hash", getHash());
            return new Dpu(m_user.callApi("dpu", parameters));
        }

        public Interaction[] getBuffered(uint count = 0, ulong from_id = 0)
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot get buffered interactions for an empty definition");
            }

            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("hash", getHash());
            if (count > 0)
            {
                parameters.Add("count", count.ToString());
            }
            if (from_id > 0)
            {
                parameters.Add("from_id", from_id.ToString());
            }

            JSONdn res = m_user.callApi("stream", parameters);

            if (!res.has("stream"))
            {
                throw new ApiException("No data in the response", -1);
            }

            List<Interaction> retval = new List<Interaction>();

            JToken[] children = res.getChildren("stream");
            for (int i = 0; i < children.Length; i++)
            {
                retval.Add(new Interaction(children[i]));
            }

            return retval.ToArray();
        }

        public StreamConsumer getConsumer(IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(m_user, type, this, event_handler);
        }
    }
}
