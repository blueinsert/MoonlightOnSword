using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Camera m_camera;
	public GameObject m_agent;
	public GameObject m_target;
    public InputComp m_input;

	public float m_minDist;
	public float m_maxDist;
	public float m_distance;
	public Vector3 m_headOffset;
	public float m_minXRotate = -5f;
	public float m_maxXRotate = 89f;
	public float m_minDistance = 1.0f;
	public float m_maxDistance = 10.0f;
	public float m_rotateXRate = 1.0f;
	public float m_rotateYRate = 1.0f;
	public float m_zoomRate = 0.1f;

	void Awake() {
	}

	public Vector3 GetForward()
	{
		return m_agent.transform.forward;
	}

	/// <summary>
	/// 
	/// </summary>
    public void Init(CameraConfig config)
	{
		m_distance = config.InitDist;
		m_maxDist = config.MaxDist;
		m_minDist = config.MinDist;
		m_minXRotate = config.MinXRotate;
		m_maxXRotate = config.MaxXRotate;

		m_rotateXRate = config.RotateXRate;
		m_rotateYRate = config.RotateYRate;
		m_zoomRate = config.ZoomRate;

		m_headOffset.y = config.HeadOffsetY;
	}

	public void BindTarget(GameObject target)
	{
		m_target = target;
        m_input = m_target.GetComponent<InputComp>();
        if (m_input == null)
        {
            Debug.LogError("FollowCamera:BindTarget input is null");
        }
		m_agent.transform.position = target.transform.position;
		m_agent.transform.rotation = target.transform.rotation;
	}

	void UpdatePosition()
	{
		//todo 跟随缓动
		m_agent.transform.position = m_target.transform.position;
	}

	void UpdateManualRotation(Vector2 value)
	{
		var euler = m_agent.transform.localEulerAngles;
		euler.y += value.x;
		euler.x += value.y;
		if (euler.x > 180)
		{
			euler.x -= 360;
		}
		euler.x = Mathf.Clamp(euler.x, m_minXRotate, m_maxXRotate);
		var newRotate = Quaternion.Euler(euler.x, euler.y, 0);
		m_agent.transform.rotation = newRotate;
	}

	void UpdateRotation()
	{
		if (m_input == null)
			return;
		var rotate = m_input.RotateValue;
		if (rotate.x == 0 && rotate.y == 0)
			return;
		rotate.x /= Screen.width;
		rotate.y /= Screen.height;
		rotate.x *= 360* m_rotateXRate;
		rotate.y *= 180 * m_rotateYRate;
		UpdateManualRotation(rotate);
	}

	void UpdateZoom()
	{
		if (m_input == null)
			return;
		var scrollVaule = m_input.ScrollValue.y;
		if (scrollVaule == 0)
			return;
		if (scrollVaule < 0)
		{
			m_distance *= 1 + m_zoomRate;
		}
		else
		{
			m_distance /= (1 + m_zoomRate);
		}
		m_distance = Mathf.Clamp(m_distance, m_minDistance, m_maxDistance);
	}

	// Update is called once per frame
	public void Tick () {
		if (m_target == null)
			return;
		UpdateZoom();
		UpdatePosition();
		UpdateRotation();
		var targetPos = m_agent.transform.position + m_headOffset;
		var forward = m_agent.transform.forward;
		var cameraPos = targetPos - forward * m_distance;
		m_camera.transform.position = cameraPos;
		m_camera.transform.LookAt(targetPos);
	}
}
