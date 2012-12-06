using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// A Dpu object represents the cost breakdown of a Definition. Once
    /// created, an instance of this class is immutable.
    /// </summary>
    public class Dpu
    {
        /// <summary>
        /// Operation (string) => DpuItem
        /// </summary>
        private Dictionary<string, DpuItem> m_dpu = null;

        /// <summary>
        /// The total DPU cost.
        /// </summary>
        private double m_total = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The JSON API response.</param>
        public Dpu(JSONdn data)
        {
            m_dpu = new Dictionary<string, DpuItem>();

            foreach (string key in data.getKeys("detail"))
            {
                DpuItem item = new DpuItem(data.getIntVal("detail." + JSONdn.EscapeDots(key) + ".count"), data.getDoubleVal("detail." + JSONdn.EscapeDots(key) + ".dpu"));

                if (data.has("detail." + JSONdn.EscapeDots(key) + ".targets"))
                {
                    JToken t = data.getJVal("detail." + JSONdn.EscapeDots(key) + ".targets");
                    foreach (string targetkey in data.getKeys("detail." + JSONdn.EscapeDots(key) + ".targets"))
                    {
                        JSONdn t2 = new JSONdn(t[targetkey]);
                        item.addTarget(targetkey, new DpuItem(t2.getIntVal("count"), t2.getDoubleVal("dpu")));
                    }
                }

                m_dpu.Add(key, item);
            }

            m_total = data.getDoubleVal("dpu");
        }

        /// <summary>
        /// Get the total DPU cost.
        /// </summary>
        /// <returns>The total DPU cost.</returns>
        public double getTotal()
        {
            return m_total;
        }

        /// <summary>
        /// Get the DPU structure.
        /// </summary>
        /// <returns>A dictionary of string => DpuItem.</returns>
        public Dictionary<string, DpuItem> getDpu()
        {
            return m_dpu;
        }
    }
}
