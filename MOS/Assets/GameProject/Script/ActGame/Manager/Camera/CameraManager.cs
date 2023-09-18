using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public static CameraManager Instance;
    public FollowCamera m_camera;

    public void Awake()
	{
        Debug.Log("CameraManager:Awake");
        Instance = this;
		
	}

    public void Start()
    {
        Instance.Init();
    }

	private void Init() {
		var go = new GameObject("FollowCamera");
		go.transform.parent = this.transform;
		m_camera = go.AddComponent<FollowCamera>();
		var followAgent = new GameObject("FollowAgent");
		followAgent.transform.parent = this.transform;
		m_camera.m_agent = followAgent;
		m_camera.m_camera = Camera.main;
		m_camera.Init(ConfigDataManager.Instance.GetConfigDataCameraConfig(1));
	}

    public void Update()
    {
        Tick();
    }

    public void Tick()
	{
		if(m_camera!=null)
		    m_camera.Tick();
	}

	public void BindTarget(GameObject target)
	{
		m_camera.BindTarget(target);
	}

	public Vector3 GetForward()
	{
		return m_camera.GetForward();
	}
}
