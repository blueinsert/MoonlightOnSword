using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManger : MonoBehaviour {
	public static TimeManger Instance;
	public float m_curTime;
	public float m_deltaTime;

	public float CurTime { 
		get {
			return m_curTime;
		} 
	}

	public float DeltaTime
	{
		get
		{
			return m_deltaTime;
		}
	}

	public void Start()
	{
        Instance = this;
        Instance.Init();
    }

	public void Init() {

	}

	public void Tick()
	{
		m_deltaTime = Time.deltaTime;
		m_curTime += m_deltaTime;	
	}
}
