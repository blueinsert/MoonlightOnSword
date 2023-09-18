using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorContainer {

	public List<ActorBase> m_actorList = new List<ActorBase>();

	public void AddActor(ActorBase actor)
	{
		m_actorList.Add(actor);
	}


}
