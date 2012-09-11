using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// Represents a set of Push output parameters.
    /// </summary>
    public class PushOutputParams : Dictionary<string, string>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PushOutputParams()
            : base()
        {
        }

        /// <summary>
        /// Construct from data in a JSON object.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        public PushOutputParams(JSONdn json)
            : base()
        {
            parse(json);
        }

        /// <summary>
        /// Set the value of a parameter.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void set(string key, string val)
        {
            Add(key, val);
        }

        /// <summary>
        /// Parse the data in a JSON object and store it in this instance.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        public void parse(JSONdn json)
        {
            setOutputParams(json, "");
        }

        /// <summary>
        /// Recursive method to parse a tree in JSON into the flat
        /// dot-notation parameters used by the API.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <param name="key_prefix">The current key prefix.</param>
        protected void setOutputParams(JSONdn json, string key_prefix)
        {
            foreach (string key in json.getKeys())
            {
                string full_key = (key_prefix.Length == 0 ? "" : key_prefix + ".") + key;
                if (json.hasChildren(key))
                {
                    setOutputParams(new JSONdn(json.getJVal(key)), full_key);
                }
                else
                {
                    set(full_key, json.getStringVal(key));
                }
            }
        }
    }
}
