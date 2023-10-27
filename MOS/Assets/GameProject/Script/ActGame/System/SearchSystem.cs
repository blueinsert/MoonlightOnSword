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

    public EntityComp FindNearestEnemyInRange(EntityComp me, float range)
    {
        var enemyCamp = GameLogicUtil.GetEnemyCampType(me.CampType);
        return FindNearestInRange(me, range, enemyCamp);
    }

    public EntityComp FindNearestInRange(EntityComp me,float range, CampType campType)
    {
        float mindist = 99999;
        EntityComp nearestTarget = null;
        foreach(var comp in m_compList)
        {
            if (comp.IsEnable && comp.gameObject != me.gameObject)
            {
                var target = comp.GetComp<EntityComp>();
                if((target.CampType & campType) == target.CampType)
                {
                    var dist = (me.gameObject.transform.position - comp.transform.position).magnitude;
                    if (dist < mindist && dist < range)
                    {
                        mindist = dist;
                        nearestTarget = target;
                    }
                } 
            }
        }
        return nearestTarget;
    }
}

