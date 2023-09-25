using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;
using System;

[Serializable]
public class BehaviorConfig
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public int ID;

    [SerializeField]
    public List<AnimEvent> AnimEvents = new List<AnimEvent>();

    [SerializeField]
    public List<FrictionSetEvent> FrictionSetEvents = new List<FrictionSetEvent>();

    [SerializeField]
    public List<TranslationEvent> TranslationEvents = new List<TranslationEvent>();

    [SerializeField]
    public List<VelSetEvent> VelSetEvents = new List<VelSetEvent>();

    [SerializeField]
    public List<ColliderSetEvent> ColliderSetEvents = new List<ColliderSetEvent>();

    [SerializeField]
    public List<HitDefSetEvent> HitDefSetEvents = new List<HitDefSetEvent>();

    public IEnumerable<EventBase> GetAllEvents()
    {
        List<EventBase> events = new List<EventBase>();
        foreach (var ae in AnimEvents) events.Add(ae);
        foreach (var ae in FrictionSetEvents) events.Add(ae);
        foreach (var ae in TranslationEvents) events.Add(ae);
        foreach (var ae in VelSetEvents) events.Add(ae);
        foreach (var ae in ColliderSetEvents) events.Add(ae);
        foreach (var ae in HitDefSetEvents) events.Add(ae);
        for (int i = 0; i < events.Count; i++)
        {
            yield return events[i];
        }
    }

    public void AddEvent(EventBase e)
    {
        if (e is AnimEvent)
        {
            AnimEvents.Add(e as AnimEvent);
        }
        if (e is FrictionSetEvent)
        {
            FrictionSetEvents.Add(e as FrictionSetEvent);
        }
        if ((e is TranslationEvent))
        {
            TranslationEvents.Add(e as TranslationEvent);
        }
        if (e is VelSetEvent)
        {
            VelSetEvents.Add(e as VelSetEvent);
        }
        if (e is ColliderSetEvent)
        {
            ColliderSetEvents.Add(e as ColliderSetEvent);
        }
        if (e is HitDefSetEvent)
        {
            HitDefSetEvents.Add(e as HitDefSetEvent);
        }
    }
}




