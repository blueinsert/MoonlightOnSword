using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateFactory {

    public static GameObject LoadPrefab(string prefabPath, Transform parent, Vector3 position, Quaternion rotation)
    {
        var actorPrefab = ResourceManager.Instance.LoadAsset<GameObject>(prefabPath);
        GameObject actorGo = GameObject.Instantiate(actorPrefab, parent, false);
        actorGo.transform.position = position;
        actorGo.transform.rotation = rotation;
        return actorGo;
    }

    public static GameObject CreateActor(string prefabPath, Transform parent, Vector3 position, Quaternion rotation)
    {
        var go = LoadPrefab(prefabPath, parent, position, rotation);
        return go;
        //actorGo.name = string.Format("Actor_{0}_{1}", m_id, m_campType);
        //var actor = actorGo.GetComponent<ActorBase>();
        //actor.m_configId = m_id;
        //actor.m_campType = m_campType;
        //actor.m_isMunual = m_isPlayerCtrl;
    }
}
