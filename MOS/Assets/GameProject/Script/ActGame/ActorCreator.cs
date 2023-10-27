﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCreator : MonoBehaviour {

	public int m_id;
	public CampType m_campType;
    public bool m_isAI;
    public bool m_isLocalPlayer;

    public const string PrefabPath = "GameProject/Resources/Model/Samurai_Katana/Prefab/KatanaPrefab.prefab";
    public const string AIPrefabPath = "GameProject/Resources/Model/Samurai_Katana/Prefab/KatanaAIPrefab.prefab";

    void Start()
	{
        var go = CreateFactory.CreateActor(m_isAI ? AIPrefabPath : PrefabPath, this.transform.parent, this.transform.position, this.transform.rotation);
        if(m_campType == CampType.Player && m_isLocalPlayer)
        {
            CameraManager.Instance.BindTarget(go);
        }
        go.name = "Actor_" + m_id;
        go.GetComponent<EntityComp>().CampType = this.m_campType;
        if (!m_isAI)
        {
            var input = go.GetComponent<InputComp>();
            if (input != null)
            {
                input.m_isEnable = m_campType == CampType.Player;
            }
        }
        Destroy(this.gameObject);
	}
}
