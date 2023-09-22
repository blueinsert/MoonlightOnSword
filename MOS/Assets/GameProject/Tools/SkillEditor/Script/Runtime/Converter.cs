using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Converter {
    /*
    public static IEnumerable<EventBase> GetAllEvents(this FSequence sequence)
    {
        List<EventBase> events = new List<EventBase>();

        foreach (var container in sequence.Containers)
        {
            foreach (var track in container.Tracks)
            {
                foreach (var evt in track.Events)
                {
                    var ds = evt.ToDS() as EventBase;
                    if (ds != null)
                    {
                        events.Add(ds);
                    }
                }
            }
        }

        for (int i = 0; i < events.Count; i++)
        {
            yield return events[i];
        }
    }
    */

    public static SkillConfig ToSkillConfig(this FSequence sequence)
	{
		SkillConfig skillConfig = new SkillConfig();
		skillConfig.ID = sequence.ID;
		skillConfig.Name = sequence.name;

        foreach (var container in sequence.Containers)
		{
			foreach(var track in container.Tracks)
			{
                if (track.enabled)
                {
                    foreach (var evt in track.Events)
                    {
                        var ds = evt.ToDS();
                        if (ds != null && ds is EventBase)
                        {
                            skillConfig.AddEvent(ds as EventBase);
                        }
                    }
                }
			}
		}

		return skillConfig;
	}

}
