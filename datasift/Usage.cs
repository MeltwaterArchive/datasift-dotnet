using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class Usage
    {
        private JSONdn m_data = null;

        public Usage(JSONdn data)
        {
            // Validate that the object has the required data
            if (!data.has("start"))
            {
                throw new InvalidDataException("No start date in the usage data");
            }
            if (!data.has("end"))
            {
                throw new InvalidDataException("No end date in the usage data");
            }
            if (!data.has("streams"))
            {
                throw new InvalidDataException("No stream info in the usage data");
            }
            m_data = data;
        }

        public DateTime getStartDate()
        {
            return m_data.getDateTimeVal("start", "ddd, dd MMM yyyy HH:mm:ss zzz");
        }

        public DateTime getEndDate()
        {
            return m_data.getDateTimeVal("end", "ddd, dd MMM yyyy HH:mm:ss zzz");
        }

        public string[] getStreamHashes()
        {
            return m_data.getKeys("streams");
        }

        public int getSeconds(string stream_hash)
        {
            if (!m_data.has("streams." + stream_hash))
            {
                throw new InvalidDataException("No data available for stream " + stream_hash);
            }
            if (!m_data.has("streams." + stream_hash + ".seconds"))
            {
                throw new InvalidDataException("No seconds data available for stream " + stream_hash);
            }
            return m_data.getIntVal("streams." + stream_hash + ".seconds");
        }

        public string[] getLicenseTypes(string stream_hash)
        {
            if (!m_data.has("streams." + stream_hash))
            {
                throw new InvalidDataException("No data available for stream " + stream_hash);
            }
            if (!m_data.has("streams." + stream_hash + ".licenses"))
            {
                throw new InvalidDataException("No license data available for stream " + stream_hash);
            }
            return m_data.getKeys("streams." + stream_hash + ".licenses");
        }

        public int getLicenseUsage(string stream_hash, string type)
        {
            if (!m_data.has("streams." + stream_hash))
            {
                throw new InvalidDataException("No data available for stream " + stream_hash);
            }
            if (!m_data.has("streams." + stream_hash + ".licenses"))
            {
                throw new InvalidDataException("No license data available for stream " + stream_hash);
            }
            if (!m_data.has("streams." + stream_hash + ".licenses." + type))
            {
                throw new InvalidDataException("No " + type + " license data available for stream " + stream_hash);
            }
            return m_data.getIntVal("streams." + stream_hash + ".licenses." + type);
        }
    }
}
