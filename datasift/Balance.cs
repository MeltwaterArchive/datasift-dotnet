using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class Balance
    {
        /// <summary>
        /// The internal data.
        /// </summary>
        private JSONdn m_data = null;

        /// <summary>
        /// Construct an instance from a JSONdn object.
        /// </summary>
        /// <param name="data">The source JSONdn object.</param>
        public Balance(JSONdn data)
        {
            // Validate that the object has the required data
            if (!data.has("balance"))
            {
                throw new InvalidDataException("No balance data");
            }

            // Save it for later.
            m_data = data;
        }

        public double getCredit()
        {
            return 0;
        }

        public string getPlan()
        {
            return "";
        }

        public double getThreshold()
        {
            return 0;
        }
    }



}
