using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

//[FEventDS(typeof(AnimEvent))]
[FEvent("Anim", typeof(FAnimTrack))]
public class FAnimEvent : FEvent {

    [SerializeField]
    public string Anim;
    /// <summary>
    /// 原始长度
    /// </summary>
    [SerializeField]
    public float OriginLen;

    [SerializeField]
    public bool IsUseTrigger = false;

    public override object ToDS()
    {
        AnimEvent ae = new AnimEvent();
        ae.Anim = Anim;
        ae.OriginLen = this.OriginLen;
        ae.IsUseTrigger = this.IsUseTrigger;
        ae.StartTime = this.FrameRange.Start / 60f;
        ae.EndTime = this.FrameRange.End / 60f;
        return ae;
    }

    public override string Text
    {
        get
        {
            return "Anim:" + Anim;
        }

        set
        {
            base.Text = value;
        }
    }
}
