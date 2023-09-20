using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[FEvent("Friction", typeof(FFrictionTrack))]
public class FFrictionEvent : FEvent {

	public float Value;

	public FrictionSetEvent ToDS()
	{
		FrictionSetEvent fe = new FrictionSetEvent();
		fe.Value = this.Value;
		fe.StartTime = this.FrameRange.Start / 60f;
		fe.EndTime = this.FrameRange.End / 60f;
		return fe;

    }

    public override string Text
    {
        get
        {
            return "Friction:"+Value;
        }

        set
        {
            base.Text = value;
        }
    }
}
