using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//纯数据
public class EntityComp : ComponentBase
{
	public CampType CampType { get { return m_campType; } set { m_campType = value; } }

	public CampType m_campType;
	public int m_playerIndex;
    public int m_id;
}
