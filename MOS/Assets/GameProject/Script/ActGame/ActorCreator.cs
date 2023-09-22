using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCreator : MonoBehaviour {

	public int m_id;
	public CampType m_campType;
	public bool m_isPlayerCtrl;

	void Start()
	{
        var go = CreateFactory.CreateActor("GameProject/Resources/Model/Samurai_Katana/Prefab/KatanaPrefab.prefab", this.transform.parent,this.transform.position,this.transform.rotation);
        if(m_campType == CampType.Player)
        {
            CameraManager.Instance.BindTarget(go);
        }
        go.name = "Actor_" + m_id;
        go.GetComponent<EntityComp>().CampType = this.m_campType;
        Destroy(this.gameObject);
	}
}
