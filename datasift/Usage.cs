using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// Parses and provides access to a response from the usage endpoint.
    /// </summary>
    public class Usage
    {
        /// <summary>
        /// The internal data.
        /// </summary>
        private JSONdn m_data = null;

        /// <summary>
        /// Construct an instance from a JSONdn object.
        /// </summary>
        /// <param name="data">The source JSONdn object.</param>
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

            // Save it for later.
            m_data = data;
        }

        /// <summary>
        /// Get the start date.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getStartDate()
        {
            return m_data.getDateTimeVal("start", "ddd, dd MMM yyyy HH:mm:ss zzz");
        }

        /// <summary>
        /// Get the end date.
        /// </summary>
        /// <returns>A DateTime object.</returns>
        public DateTime getEndDate()
        {
            return m_data.getDateTimeVal("end", "ddd, dd MMM yyyy HH:mm:ss zzz");
        }

        /// <summary>
        /// Get an array of the stream hashes for which there is usage data.
        /// </summary>
        /// <returns>An array of stream hashes.</returns>
        public string[] getStreamHashes()
        {
            return m_data.getKeys("streams");
        }

        /// <summary>
        /// Get the number of seconds consumed for the given stream.
        /// </summary>
        /// <param name="stream_hash">The stream hash.</param>
        /// <returns>The number of seconds.</returns>
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

        /// <summary>
        /// Get the license types for which data exists.
        /// </summary>
        /// <param name="stream_hash">The stream hash.</param>
        /// <returns>An array of license types.</returns>
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

        /// <summary>
        /// Get the license usage for the given stream and type.
        /// </summary>
        /// <param name="stream_hash">The stream hash.</param>
        /// <param name="type">The license type.</param>
        /// <returns>The usage.</returns>
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
