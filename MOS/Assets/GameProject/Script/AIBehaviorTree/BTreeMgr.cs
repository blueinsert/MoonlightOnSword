using UnityEngine;
using UnityEditor;
using Game.AIBehaviorTree;
using System;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using LitJson;

//	EditorBTreeMgr.cs
//	Author: Lu Zexi
//	2014-08-07


namespace Game.AIBehaviorTree
{

	/// <summary>
	/// Editor Behavior node manager
	/// </summary>
	public class BTreeMgr
	{
		public Dictionary<string,BTree> m_mapTree = new Dictionary<string, BTree>();

		private static BTreeMgr s_cInstance;
		public static BTreeMgr sInstance
		{
			get
			{
				if(s_cInstance == null)
				{
					s_cInstance = new BTreeMgr();
				}
				return s_cInstance;
			}
		}

		public BTreeMgr()
		{
			//
		}

		public void Clear()
		{
            this.m_mapTree.Clear();
        }

		//load data
		public bool Load(string jsontxt)
		{
			Clear();

			try
			{
				JsonData json = JsonMapper.ToObject(jsontxt);
				json = json["trees"];
				int count = json.Count;
				for (int i = 0; i < count; i++)
				{
					BTree bt = new BTree();
					JsonData data = json[i];
					bt.ReadJson(data);
					this.m_mapTree.Add(bt.m_strName, bt);
				}
			}
			catch(Exception e)
			{
				Debug.LogError(e.ToString());
				return false;
			}

			return true;
		}

		public JsonData ToJsonData()
		{
            JsonData data = new JsonData();
            data["trees"] = new JsonData();
            data["trees"].SetJsonType(JsonType.Array);
            foreach (KeyValuePair<string, BTree> item in this.m_mapTree)
            {
                item.Value.WriteJson(data["trees"]);
            }
			return data;
        }

		public BTree GetTree( string name )
		{
			if( this.m_mapTree.ContainsKey(name))
				return this.m_mapTree[name];
			return null;
		}

		public List<BTree> GetTrees()
		{
			List<BTree> lst = new List<BTree>();
			foreach( BTree item in this.m_mapTree.Values )
				lst.Add(item);
			return lst;
		}

		public void Add( BTree tree )
		{
			if( this.m_mapTree.ContainsKey(tree.m_strName))
			{
//				Debug.LogError("The tree id is exist.");
#if UNITY_EDITOR
				UnityEditor.EditorUtility.DisplayDialog("Error" ,"The tree named " + tree.m_strName + " is already exist.","ok");
#endif
				return;
			}
			this.m_mapTree.Add(tree.m_strName , tree);
		}

		public void Remove(BTree tree)
		{
			if(tree == null ) return;
			if( this.m_mapTree.ContainsKey(tree.m_strName))
				this.m_mapTree.Remove(tree.m_strName);
			else
				Debug.LogError("The tree id is not exist.");
			return;
		}
	}

}
