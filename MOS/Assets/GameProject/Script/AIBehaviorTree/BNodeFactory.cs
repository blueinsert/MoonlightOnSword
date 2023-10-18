using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using Game.AIBehaviorTree;
using UnityEditor.Compilation;

//	BNodeFactory.cs
//	Author: Lu Zexi
//	2014-08-11

/// <summary>
/// Behavior node factory.
/// </summary>
public class BNodeFactory
{
	public List<Type> m_lstComposite = new List<Type>();
	public List<Type> m_lstAction = new List<Type>();
	public List<Type> m_lstCondition = new List<Type>();
	public List<Type> m_lstDecorator = new List<Type>();

	private static BNodeFactory s_cInstance;
	public static BNodeFactory sInstance
	{
		get
		{
			if(s_cInstance == null)
			{
				s_cInstance = new BNodeFactory();
			}
			return s_cInstance;
		}
	}

	public BNodeFactory()
	{
//		m_lstGen.Add(typeof(BNodeSequence));
//		m_lstGen.Add(typeof(BNodeSelector));
//		m_lstGen.Add(typeof(BNodeParallel));
		this.m_lstComposite = GetSubClass(typeof(BNodeComposite));
		this.m_lstAction = GetSubClass(typeof(BNodeAction));
		this.m_lstCondition = GetSubClass(typeof(BNodeCondition));
		this.m_lstDecorator = GetSubClass(typeof(BNodeDecorator));
	}

#if UNITY_EDITOR
	//get subclass
	public static List<Type> GetSubClass(Type nodeType)
	{
		List<Type> lstType = new List<Type>();

        var mainDomainAssemblyList = AppDomain.CurrentDomain.GetAssemblies();
		foreach(var assembly in mainDomainAssemblyList)
		{
			foreach(var tt in assembly.GetTypes())
			{
                if (tt != null)
                {
                    if (tt.IsSubclassOf(nodeType))
                    {
                        lstType.Add(tt);
                    }
                }
            }
		}
		return lstType;
	}
#endif

	public BNode Create( int index )
	{
		if( this.m_lstComposite.Count > index )
		{
			Type t = this.m_lstComposite[index];
			BNode node = Activator.CreateInstance(t) as BNode;
			return node;
		}
		Debug.LogError("The type index is none : " + index);
		return null;
	}

	public string[] GetNodeLst()
	{
		string[] str = new string[this.m_lstComposite.Count];
		for( int i = 0 ; i<this.m_lstComposite.Count ;i++ )
		{
			Type item = this.m_lstComposite[i];
			str[i] = item.Name;
		}
		return str;
	}
}

