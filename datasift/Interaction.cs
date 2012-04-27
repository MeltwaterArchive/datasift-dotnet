using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class Interaction : JSONdn
    {
        public Interaction(JToken obj)
            : base(obj)
        {
        }

        public Interaction(JObject obj)
            : base(obj)
        {
        }
        
        public Interaction(string source)
            : base(source)
        {
        }
    }
}
