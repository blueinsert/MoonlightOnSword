using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TestCases.TestVectorLerp();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log(string.Format("Time.deltaTime:{0} timeScale:{1}", Time.deltaTime, Time.timeScale));
	}
}
