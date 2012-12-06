using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    class HistoricDataAvailability
    {
	    private DateTime m_start = DateTime.MinValue;
	    private DateTime m_end = DateTime.MinValue;
	    private Dictionary<string, HistoricDataAvailabilitySource> m_sources = new Dictionary<string,HistoricDataAvailabilitySource>();
	
	    public HistoricDataAvailability(string json)
            :  this(new JSONdn(json))
        {
	    }
	
	    public HistoricDataAvailability(JSONdn json)
        {
		    m_start = json.getDateTimeFromLongVal("start");
            m_end = json.getDateTimeFromLongVal("end");
		    
            if (json.has("sources"))
            {
                foreach (string key in json.getKeys("sources"))
                {
			        m_sources.Add(key, new HistoricDataAvailabilitySource(new JSONdn(json.getJVal("sources." + JSONdn.EscapeDots(key)))));
                }
            }
	    }
	
	    public DateTime getStartDate() {
		    return m_start;
	    }
	
	    public DateTime getEndDate() {
		    return m_end;
	    }
	
	    public Dictionary<string, HistoricDataAvailabilitySource> getSources() {
		    return m_sources;
	    }
	
	    public HistoricDataAvailabilitySource getSource(string source)
        {
		    return m_sources[source];
	    }
    }
}
