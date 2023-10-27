using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLogicUtil {
	public static CampType GetEnemyCampType(CampType myCamp)
	{
		switch (myCamp)
		{
			case CampType.Player:
				return CampType.Enemy;
			case CampType.Enemy:
				return CampType.Player;
			case CampType.Neutral:
				return CampType.None;
		}
		return CampType.None;
	}
	
}
