using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// A single item from a DPU breakdown response.
    /// </summary>
    public class DpuItem
    {
        /// <summary>
        /// The number of times this item is used.
        /// </summary>
        private int m_count = 0;

        /// <summary>
        /// The total cost for this item.
        /// </summary>
        private double m_dpu = 0;

        /// <summary>
        /// The targets used in combination with this item.
        /// </summary>
        private Dictionary<string, DpuItem> m_targets = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="count">The number of times this item was used.</param>
        /// <param name="dpu">The DPU cost for this item.</param>
        public DpuItem(int count, double dpu)
        {
            m_count = count;
            m_dpu = dpu;
            m_targets = new Dictionary<string, DpuItem>();
        }

        /// <summary>
        /// Get the number of times this item was used.
        /// </summary>
        /// <returns>The count.</returns>
        public int getCount()
        {
            return m_count;
        }

        /// <summary>
        /// Get the DPU cost for this item.
        /// </summary>
        /// <returns>The DPU cost.</returns>
        public double getDpu()
        {
            return m_dpu;
        }

        /// <summary>
        /// Get the list of targets for this item.
        /// </summary>
        /// <returns>A Dictionary of target => DpuItem.</returns>
        public Dictionary<string, DpuItem> getTargets()
        {
            return m_targets;
        }
        
        /// <summary>
        /// Does this item have a further level of broken down DPU costs.
        /// </summary>
        /// <returns>True if this item has targets.</returns>
        public bool hasTargets()
        {
            return m_targets.Count > 0;
        }

        /// <summary>
        /// Add a target to this item.
        /// </summary>
        /// <param name="target">The name of the target.</param>
        /// <param name="dpuItem">The DPU details for the target.</param>
        public void addTarget(string target, DpuItem dpuItem)
        {
            m_targets.Add(target, dpuItem);
        }
    }
}
