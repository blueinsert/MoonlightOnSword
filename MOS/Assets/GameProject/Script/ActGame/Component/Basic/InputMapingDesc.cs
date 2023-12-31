﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapingDesc : MonoBehaviour {

	public List<PlayerInputMapDic> m_mapingList = new List<PlayerInputMapDic>();

	public PlayerInputMapDic GetInputMapping(int playerIndex, CampType campType)
	{
		foreach(var pair in m_mapingList)
		{
			if(pair.PlayerIndex == playerIndex && pair.CampType == campType)
			{
				return pair;
			}
		}
		return null;
	}
}
