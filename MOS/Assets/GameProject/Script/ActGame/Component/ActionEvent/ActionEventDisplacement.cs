using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventDisplacement : ActionEventBase {
	public Vector2 m_offset;

	public override void OnStart()
	{
		base.OnStart();
		m_owner.transform.position += new Vector3(m_offset.x, 0, m_offset.y);
		Debug.Log("ActionEventDisplacement:OnStart");
	}

	public void SetParams(Vector2 offset)
	{
		m_offset = offset;
	}

}
