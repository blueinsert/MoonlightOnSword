using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Converter {

	public static SkillConfig ToSkillConfig(this FSequence sequence)
	{
		SkillConfig skillConfig = new SkillConfig();
		skillConfig.ID = sequence.ID;
		skillConfig.Name = sequence.name;

        foreach (var container in sequence.Containers)
		{
			foreach(var track in container.Tracks)
			{
				foreach(var evt in track.Events)
				{
					AddEvent(skillConfig, evt);
				}
			}
		}

		return skillConfig;
	}

    private static void AddEvent(SkillConfig skillConfig, FEvent e)
    {

        if (e is FAnimEvent)
        {
            AddAnimEvent(skillConfig, e as FAnimEvent);
        } else if (e is FFrictionEvent)
        {
            AddFrictionEvent(skillConfig, e as FFrictionEvent);
        }

	}

	private static void AddAnimEvent(SkillConfig skillConfig, FAnimEvent e)
	{
        List<AnimEvent> aes = new List<AnimEvent>();
        if (skillConfig.AnimEvents != null)
        {
            aes.AddRange(skillConfig.AnimEvents);
        }

        var ae = e .ToDS();
        aes.Add(ae);
       
        skillConfig.AnimEvents = aes.ToArray();
    }

    private static void AddFrictionEvent(SkillConfig skillConfig, FFrictionEvent e)
    {
        List<FrictionSetEvent> aes = new List<FrictionSetEvent>();
        if (skillConfig.FrictionSetEvents != null)
        {
            aes.AddRange(skillConfig.FrictionSetEvents);
        }

        var ae = e.ToDS();
        aes.Add(ae);

        skillConfig.FrictionSetEvents = aes.ToArray();
    }
}
