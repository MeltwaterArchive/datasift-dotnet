using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class DpuItem
    {
        private int m_count = 0;
        private double m_dpu = 0;
        private Dictionary<string, DpuItem> m_targets = null;

        public DpuItem(int count, double dpu)
        {
            m_count = count;
            m_dpu = dpu;
            m_targets = new Dictionary<string, DpuItem>();
        }

        public int getCount()
        {
            return m_count;
        }

        public double getDpu()
        {
            return m_dpu;
        }

        public Dictionary<string, DpuItem> getTargets()
        {
            return m_targets;
        }

        public bool hasTargets()
        {
            return m_targets.Count > 0;
        }

        public void addTarget(string target, DpuItem dpuItem)
        {
            m_targets.Add(target, dpuItem);
        }
    }
}
