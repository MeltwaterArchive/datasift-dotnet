using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

/*! \mainpage DataSift .Net API Client Test
 *
 * \section Introduction
 *
 * This is the <a href='https://github.com/datasift/datasift-dotnet'>official .Net library</a> for accessing <a href='http://datasift.com'>DataSift</a>.
 *
 * \section Examples
 *
 * See the <a href='https://github.com/datasift/datasift-dotnet/tree/master/datasift-examples'>examples</a> folder for some simple example usage.
 *
 * This is the <a href='https://github.com/datasift/datasift-dotnet'>official .Net library</a> for accessing <a href='http://detasift.com'>DataSift</a>.
 *
 * \section Source Code
 *
 * The DataSift API Library is available for <a href='https://github.com/datasift/datasift-dotnet'>download</a>.
 *
 * \section Documentation
 *
 * The DataSift API Library is used to access the <a href='http://datasift.com/'>DataSift</a> platform via its Application Programming Interface. You can learn more about the DataSift API on our <a href='http://dev.datasift.com/docs/'>documentation</a> site.
 *
 * \section Licensing
 *
 * The DataSift .Net API Library is Open Source software. Please read the <a href="https://github.com/datasift/datasift-dotnet/blob/master/LICENSE">license</a>.
 *
 */

namespace datasift
{
    /// <summary>
    /// A Definition represents a stream. For a Definition object to be valid
    /// it must either be constructed with a hash or contain CSDL.
    /// </summary>
    public class Definition
    {
        /// <summary>
        /// The user that "owns" this Definition.
        /// </summary>
        private User m_user = null;

        /// <summary>
        /// The hash for this definition, or an empty string if it has yet to
        /// be validated or compiled.
        /// </summary>
        private string m_hash = String.Empty;

        /// <summary>
        /// The date that this Definition was first created.
        /// </summary>
        private DateTime m_created_at = DateTime.MinValue;

        /// <summary>
        /// The total DPU cost of this Definition, or -1 if it has yet to be
        /// validated or compiled.
        /// </summary>
        private double m_total_dpu = -1;

        /// <summary>
        /// The CSDL for this Definition.
        /// </summary>
        private string m_csdl = String.Empty;

        /// <summary>
        /// Constructor. Note that if you create a Definition with both a
        /// non-empty CSDL and a non-empty hash the hash will be ignored.
        /// </summary>
        /// <param name="u">The user to whom this Definition should belong.</param>
        /// <param name="csdl">An initial CSDL string.</param>
        /// <param name="hash">An initial hash string.</param>
        public Definition(User user, string csdl = "", string hash = "")
        {
            m_user = user;
            m_hash = hash;
            set(csdl);
        }

        /// <summary>
        /// Get the User object that created this Definition.
        /// </summary>
        /// <returns>The User object.</returns>
        public User getUser()
        {
            return m_user;
        }

        /// <summary>
        /// Get the CSDL string.
        /// </summary>
        /// <returns>The CSDL.</returns>
        public string get()
        {
            return m_csdl;
        }

        /// <summary>
        /// Set the CSDL for this definition. If it doesn't match the current
        /// CSDL the hash will be cleared to force a compilation if the hash
        /// is then subsequently requested.
        /// </summary>
        /// <param name="csdl">The new CSDL string.</param>
        public void set(string csdl)
        {
            csdl = csdl.Trim();

            if (m_csdl != csdl)
            {
                m_csdl = csdl;
                clearHash();
            }
        }

        /// <summary>
        /// Get the hash for this Definition. If the hash is empty the CSDL
        /// will be compiled to obtain it.
        /// </summary>
        /// <returns>The hash.</returns>
        public string getHash()
        {
            if (m_hash.Length == 0)
            {
                compile();
            }

            return m_hash;
        }

        /// <summary>
        /// Clears the details obtained from DataSift when the CSDL was
        /// compiled.
        /// </summary>
        public void clearHash()
        {
            if (m_csdl.Length == 0)
            {
                throw new InvalidDataException("Cannot clear the hash of a hash-only definition object");
            }

            m_hash = String.Empty;
            m_created_at = DateTime.MinValue;
            m_total_dpu = -1;
        }

        /// <summary>
        /// Get the date when this definition was first created. The CSDL will
        /// be validated to obtain the created_at date if necessary.
        /// </summary>
        /// <returns>The created_at date.</returns>
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

        /// <summary>
        /// Get the total DPU cost for this Definition. The CSDL will be
        /// validated to obtain this if necessary.
        /// </summary>
        /// <returns>The total DPU cost.</returns>
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

        /// <summary>
        /// Send the CSDL to DataSift for compilation.
        /// </summary>
        public void compile()
        {
            // We must have some CSDL in order to compile the CSDL!
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

        /// <summary>
        /// Send the CSDL to DataSift for validation.
        /// </summary>
        public void validate()
        {
            // We must have some CSDL to be validate.
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
                m_created_at = res.getDateTimeVal("created_at", "yyyy-MM-dd HH:mm:ss");

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

        /// <summary>
        /// Get the DPU cost breakdown for this Definition.
        /// </summary>
        /// <returns>A Dpu object.</returns>
        public Dpu getDpuBreakdown()
        {
            if (m_csdl.Length == 0 && m_hash.Length == 0)
            {
                throw new InvalidDataException("Cannot get the DPU breakdown for an empty definition");
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("hash", getHash());
            return new Dpu(m_user.callApi("dpu", parameters));
        }

        /// <summary>
        /// Get buffered tweets.
        /// </summary>
        /// <param name="count">The number of tweets to get - see http://dev.datasift.com/ for the limit.</param>
        /// <param name="from_id">The ID of the latest interaction that you received.</param>
        /// <returns>An array of Interaction objects.</returns>
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

        /// <summary>
        /// Get a consumer object of the given type for this Definition.
        /// </summary>
        /// <param name="event_handler">An object that implements the IEventHandler interface.</param>
        /// <param name="type">The consumer type required.</param>
        /// <returns>An instance of a class derived from StreamConsumer.</returns>
        public StreamConsumer getConsumer(IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(m_user, type, this, event_handler);
        }
    }
}
