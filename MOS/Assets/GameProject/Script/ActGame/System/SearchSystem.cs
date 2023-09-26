using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CaredCompType(typeof(SearchableComp))]
public class SearchSystem : SystemBase
{
    public static SearchSystem Instance;

    public void Start()
    {
        Instance = this;
    }

    protected override void OnTick()
    {
        base.OnTick();
    }

    public EntityComp FindNearestInRange(EntityComp me,float range)
    {
        float mindist = 99999;
        GameObject go = null;
        foreach(var comp in m_compList)
        {
            if (comp.IsEnable && comp.gameObject != me.gameObject)
            {
                var dist = (me.gameObject.transform.position - comp.transform.position).magnitude;
                if (dist < mindist && dist < range)
                {
                    mindist = dist;
                    go = comp.gameObject;
                }
            }
        }
        if(go != null)
        {
            return go.GetComponent<EntityComp>();
        }
        return null;
    }
}

