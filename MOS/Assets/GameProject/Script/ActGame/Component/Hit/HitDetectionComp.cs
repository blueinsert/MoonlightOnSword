using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionComp : ComponentBase {

    public bool IsValid { get { return m_isValid; } }

    public HitDef m_hitDef = null;
    public bool m_isValid = false;

    public void SetHitDef(HitDef hitDef)
    {
        m_hitDef = hitDef;
        m_isValid = true;
        if (hitDef == null)
            Invalid();
    }

    public void Invalid()
    {
        m_isValid = false;
    }
    
}
