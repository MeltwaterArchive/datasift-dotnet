using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class JSONdn
    {
        private JToken m_data = null;

        public JSONdn(JToken obj)
        {
            m_data = obj;
        }

        public JSONdn(JObject obj)
        {
            m_data = obj.Root;
        }

        public JSONdn(string source)
        {
            m_data = JObject.Parse(source).Root;
        }

        public JToken resolveString(string key)
        {
            string[] parts = key.Split(new Char[] { '.' });
            JToken retval = m_data[parts[0]];
            for (int i = 1; i < parts.Length; i++)
            {
                if (retval[parts[i]] == null)
                {
                    retval = null;
                    break;
                }
                retval = retval[parts[i]];
            }
            return retval;
        }

        public JToken getJVal(string key)
        {
            return resolveString(key);
        }

        public JToken[] getChildren(string key)
        {
            List<JToken> retval = new List<JToken>();
            JToken current = getJVal(key).First;
            while (current != null)
            {
                retval.Add(current);
                current = current.Next;
            }
            return retval.ToArray();
        }

        public bool getBoolVal(string key)
        {
            return Convert.ToBoolean(getStringVal(key));
        }

        public int getIntVal(string key)
        {
            return Convert.ToInt32(getStringVal(key));
        }

        public long getLongVal(string key)
        {
            return Convert.ToInt64(getStringVal(key));
        }

        public double getDoubleVal(string key)
        {
            return Convert.ToDouble(getStringVal(key));
        }

        public DateTime getDateTimeVal(string key, string format)
        {
            return DateTime.ParseExact(getStringVal(key), format, null);
        }

        public string getStringVal(string key)
        {
            return resolveString(key).ToString();
        }

        public bool has(string key)
        {
            return resolveString(key) != null;
        }

        public string[] getKeys(string key)
        {
            List<string> retval = new List<string>();

            JToken jtoken = getJVal(key).First;

            while (jtoken != null)
            {
                retval.Add(((JProperty)jtoken).Name);
                jtoken = jtoken.Next;
            }

            return retval.ToArray();
        }

    }
}
