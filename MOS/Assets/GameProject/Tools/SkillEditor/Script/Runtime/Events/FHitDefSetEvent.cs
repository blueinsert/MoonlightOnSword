using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[FEvent("HitDef", typeof(FHitDefSetTrack))]
public class FHitDefSetEvent : FEvent {
    [SerializeField]
    public HitDef HitDef = null;

    public override object ToDS()
    {
        HitDefSetEvent e = new HitDefSetEvent();
        e.HitDef = HitDef;
        e.StartTime = this.FrameRange.Start / 60f;
        e.EndTime = this.FrameRange.End / 60f;
        return e;
    }

    public override string Text
    {
        get
        {
            return "HitDef";
        }

        set
        {
            base.Text = value;
        }
    }

}
