using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    class HistoricDataAvailabilitySource
    {
	    private int m_status = 0;
	    private List<int> m_versions = new List<int>();
	    private Dictionary<string,int> m_augmentations = new Dictionary<string,int>();
	
	    public HistoricDataAvailabilitySource(string json)
            : this(new JSONdn(json))
        {
	    }
	
	    public HistoricDataAvailabilitySource(JSONdn json)
        {
		    m_status = json.getIntVal("status");

            foreach (JToken version in json.getJVal("versions"))
	        {
		        m_versions.Add(Convert.ToInt32(version));
	        }
            
            foreach (string key in json.getKeys("augmentations"))
            {
			    m_augmentations.Add(key, json.getIntVal("augmentations." + key));
		    }
	    }
	
	    public int getStatus()
        {
		    return m_status;
	    }
	
	    public List<int> getVersions()
        {
		    return m_versions;
	    }
	
	    public Dictionary<string,int> getAugmentations()
        {
		    return m_augmentations;
	    }
	
	    public int getAugmentation(string augmentation)
        {
            if (m_augmentations.ContainsKey(augmentation)) {
                return m_augmentations[augmentation];
            }
            return 0;
        }
    }
}
