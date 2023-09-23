using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[FEvent("Collider", typeof(FColliderSetTrack))]
public class FColliderSetEvent : FEvent {

    [SerializeField]
    public string Name;
    [SerializeField]
    public bool IsEnable;

    public override object ToDS()
    {
        ColliderSetEvent e = new ColliderSetEvent();
        e.Name = this.Name;
        e.IsEnable = IsEnable;
        e.StartTime = this.FrameRange.Start / 60f;
        e.EndTime = this.FrameRange.End / 60f;
        return e;
    }

    public override string Text
    {
        get
        {
            return string.Format("Collider: {0} {1}", this.Name, this.IsEnable);
        }

        set
        {
            base.Text = value;
        }
    }
}
