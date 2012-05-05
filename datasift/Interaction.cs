using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// An interaction object.
    /// </summary>
    public class Interaction : JSONdn
    {
        /// <summary>
        /// Constructs an Interaction object from a JToken object.
        /// </summary>
        /// <param name="obj">The JToken object.</param>
        public Interaction(JToken obj)
            : base(obj)
        {
        }

        /// <summary>
        /// Constructs an Interaction object from a JObject object.
        /// </summary>
        /// <param name="obj">The JObject object.</param>
        public Interaction(JObject obj)
            : base(obj)
        {
        }

        /// <summary>
        /// Constructs an Interaction object from a JSON string.
        /// </summary>
        /// <param name="source">The JSON string.</param>
        public Interaction(string source)
            : base(source)
        {
        }
    }
}
