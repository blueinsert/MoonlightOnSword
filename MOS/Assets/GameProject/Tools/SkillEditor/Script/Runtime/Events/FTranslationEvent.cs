using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[FEvent("Translation", typeof(FTranslationTrack))]
public class FTranslationEvent : FEvent
{
    [SerializeField]
    public List<TranslateCondition> Conditions;
    [SerializeField]
    public int To;

    public TranslationEvent ToDS()
    {
        TranslationEvent fe = new TranslationEvent();
        fe.To = To;
        fe.ConditionList = Conditions;
        fe.StartTime = this.FrameRange.Start / 60f;
        fe.EndTime = this.FrameRange.End / 60f;
        return fe;

    }

    public override string Text
    {
        get
        {
            return "Translate2:" + To;
        }

        set
        {
            base.Text = value;
        }
    }
}