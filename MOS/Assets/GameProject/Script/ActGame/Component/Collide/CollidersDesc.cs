using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersDesc : MonoBehaviour {
    /// <summary>
    /// attack triggers
    /// </summary>
	public List<Collider> m_attackBoxList = new List<Collider>();
    /// <summary>
    /// defense triggers
    /// </summary>
    public List<Collider> m_defenceBoxList = new List<Collider>();
    /// <summary>
    /// 占据一定空间，会和其他occupyBox发生推挤
    /// </summary>
    //public List<Collider> m_occupyBoxList = new List<Collider>();
    public CapsuleCollider m_pushCollider = null;

    public void Awake()
    {
        
    }
}
