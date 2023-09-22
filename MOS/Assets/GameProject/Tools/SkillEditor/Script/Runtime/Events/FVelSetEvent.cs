using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[FEvent("Vel", typeof(FVelSetTrack))]
public class FVelSetEvent : FEvent {

    [SerializeField]
    public VelRelativeType Relative;
    [SerializeField]
    public float VX;
    [SerializeField]
    public float VY;
    [SerializeField]
    public float VZ;

    public override object ToDS()
    {
        VelSetEvent ae = new VelSetEvent();
        ae.Relative = this.Relative;
        ae.VX = this.VX;
        ae.VY = this.VY;
        ae.VZ = this.VZ;
        ae.StartTime = this.FrameRange.Start / 60f;
        ae.EndTime = this.FrameRange.End / 60f;
        return ae;
    }

    public override string Text
    {
        get
        {
            return "Vel";
        }

        set
        {
            base.Text = value;
        }
    }
}
