using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class Dpu
    {
        private Dictionary<string, DpuItem> m_dpu = null;

        private double m_total = 0;

        public Dpu(JSONdn data)
        {
            m_dpu = new Dictionary<string, DpuItem>();

            foreach (string key in data.getKeys("detail"))
            {
                DpuItem item = new DpuItem(data.getIntVal("detail." + key + ".count"), data.getDoubleVal("detail." + key + ".dpu"));

                if (data.has("detail." + key + ".targets"))
                {
                    JToken t = data.getJVal("detail." + key + ".targets");
                    foreach (string targetkey in data.getKeys("detail." + key + ".targets"))
                    {
                        JSONdn t2 = new JSONdn(t[targetkey]);
                        item.addTarget(targetkey, new DpuItem(t2.getIntVal("count"), t2.getDoubleVal("dpu")));
                    }
                }

                m_dpu.Add(key, item);
            }

            m_total = data.getDoubleVal("dpu");
        }

        public double getTotal()
        {
            return m_total;
        }

        public Dictionary<string, DpuItem> getDpu()
        {
            return m_dpu;
        }
    }
}
