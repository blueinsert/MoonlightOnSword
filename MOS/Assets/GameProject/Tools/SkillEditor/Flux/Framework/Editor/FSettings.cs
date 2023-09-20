using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace FluxEditor
{
	public class FSettings : ScriptableObject {

		public const string MenuPath = "Window/";
		public const string ProductName = "Flux";
		public const string WindownName = "SkillEditor";
		public const string SequenceName = "Skill";
		public const string ContainerName = "Group";
		public const int DefaultLength = 300;//frame
		public const string SequenceSavePath = "Assets/GameProject/Resources/SkillEditor/SkillPrefabs";

		[SerializeField]
		private Color DefaultEventColor = new Color(0,0,0.9f,0.8f);

		public void Init()
		{
			
		}

		public Color GetEventColor( string str )
		{
		    return FGUI.GetEventColor();
		}

	}

}
