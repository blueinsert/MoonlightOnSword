using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Game.AIBehaviorTree;
using System.Linq;
using System.IO;
using LitJson;


//	BTreeWin.cs
//	Author: Lu Zexi
//	2014-10-20

#if UNITY_EDITOR

//BTreeWin
public class BTreeWin : EditorWindow
{
    public static int NODE_WIDTH = 20;
    public static int NODE_HEIGHT = 20;
    public static int GUI_WIDTH = 240;

    public static BTree cur_tree;   //current tree
    public static BNode cur_node;   //current node
    public static BTreeWin sInstance = null;
    private static int cur_tree_index = -1;
    private static int last_tree_index = -1;
    private static int select_create_node_id = -1;

    public static BNode select;

    //temp value
    private Vector2 m_cScrollPos = new Vector2(0, 0);
    private Vector2 m_editUIScrollPos = new Vector2(0, 0);

    private string m_strInputName = "";

    private string m_jsonPath = "";


    [@MenuItem("BTree/Editor")]
    static void initwin()
    {
        BTreeWin win = (BTreeWin)BTreeWin.GetWindow(typeof(BTreeWin));
        sInstance = win;
    }

    public static void addnode(object arg)
    {
        Debug.Log("callback " + arg);
    }

    void OnGUI()
    {
        //////////////////// draw the tree /////////////////////
        this.m_cScrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width - 240, position.height), this.m_cScrollPos, new Rect(0, 0, this.maxSize.x, this.maxSize.y));

        Texture2D black = new Texture2D(1, 1);
        black.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f));
        black.Apply();
        Texture2D grey = new Texture2D(1, 1);
        grey.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
        grey.Apply();
        for (int i = 0; i < 1000; i++)
        {
            if (BTreeWin.sInstance != null)
            {
                if (i % 2 == 0)
                    GUI.DrawTexture(new Rect(0, i * NODE_HEIGHT, BTreeWin.sInstance.position.width, NODE_HEIGHT), black);
                else
                    GUI.DrawTexture(new Rect(0, i * NODE_HEIGHT, BTreeWin.sInstance.position.width, NODE_HEIGHT), grey);
            }
        }

        if (cur_tree != null && cur_tree.m_cRoot != null)
        {
            int xx = 0;
            int yy = 0;
            cur_tree.m_cRoot.Render(xx, ref yy);
        }

        GUI.EndScrollView();
        //////////////////// draw the tree /////////////////////

        //////////////////// draw editor gui /////////////////////
        this.m_editUIScrollPos = GUI.BeginScrollView(new Rect(position.width - GUI_WIDTH, 0, GUI_WIDTH, 1000), this.m_editUIScrollPos, new Rect(0, 0, this.maxSize.x, this.maxSize.y));
        //GUI.BeginGroup(new Rect(position.width - GUI_WIDTH, 0, GUI_WIDTH, 1000));
        GUI.BeginGroup(new Rect(0, 0, GUI_WIDTH,1000));
        int x = 0;
        int y = 0;
        GUI.Label(new Rect(x, y, 200, 20), string.Format("path:{0}",m_jsonPath));
        y += 20;

        List<BTree> lst = BTreeMgr.sInstance.GetTrees();
        if (GUI.Button(new Rect(x, y, 200, 40), "Load"))
        {
            cur_tree = null;
            cur_node = null;
            cur_tree_index = -1;
            last_tree_index = -1;
            select_create_node_id = -1;
            select = null;
            EditorLoad();
        }
        y += 40;
        if (GUI.Button(new Rect(x, y, 200, 40), "Save"))
        {
            EditorSave();
            AssetDatabase.Refresh();
        }
        y += 40;
        if (GUI.Button(new Rect(x, y, 200, 40), "Save As"))
        {
            EditorSaveAs();
            AssetDatabase.Refresh();
        }
        y += 40;

        if (lst.Count != 0)
        {
            GUI.Label(new Rect(x, y, 200, 20), "=======================");
            y += 20;

            GUI.Label(new Rect(x, y, 200, 20), "TreeList:");
            y += 20;
            string[] treeNames = new string[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                treeNames[i] = lst[i].m_strName;
            }
            cur_tree_index = EditorGUI.Popup(new Rect(x, y, 200, 45), cur_tree_index, treeNames);
            if (treeNames.Length > 0 && cur_tree_index < 0)
                cur_tree_index = 0;
            if (cur_tree_index != last_tree_index)
            {
                last_tree_index = cur_tree_index;
                cur_tree = lst[cur_tree_index];
                cur_node = null;
            }
            y += 40;
        }
        GUI.Label(new Rect(x, y, 200, 20), "=======================");
        y += 20;

        this.m_strInputName = GUI.TextField(new Rect(x, y + 10, 100, 20), this.m_strInputName);
        if (GUI.Button(new Rect(x + 100, y, 100, 40), "create tree"))
        {
            if (this.m_strInputName != "")
            {
                cur_node = null;
                BTree tree = new BTree();
                tree.m_strName = this.m_strInputName;
                BTreeMgr.sInstance.Add(tree);
                lst = BTreeMgr.sInstance.GetTrees();
                cur_tree = tree;
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].m_strName == tree.m_strName)
                    {
                        cur_tree_index = i;
                        break;
                    }
                }
                last_tree_index = cur_tree_index;
                Repaint();
            }
        }
        y += 40;
        if (GUI.Button(new Rect(x, y, 200, 40), "remove tree"))
        {
            cur_node = null;
            BTreeMgr.sInstance.Remove(cur_tree);
            lst = BTreeMgr.sInstance.GetTrees();
            cur_tree = null;
            cur_tree_index = -1;
            last_tree_index = -1;
            Repaint();
        }
        y += 40;

        GUI.Label(new Rect(x, y, 200, 20), "=======================");
        y += 20;

        //GUI.Label(new Rect(x, y, 200, 20), "Current Editing:");
        //y += 20;
        if (cur_tree != null)
        {
            GUI.Label(new Rect(x, y, 200, 20), "Tree Name: " + cur_tree.m_strName);
            y += 20;
            cur_tree.m_strName = GUI.TextField(new Rect(x, y, 100, 20), cur_tree.m_strName);
            y += 20;

            var nodeList = BNodeFactory.sInstance.GetNodeLst();
            select_create_node_id = EditorGUI.Popup(new Rect(x, y, 100, 40), select_create_node_id, nodeList);
            if (nodeList.Length > 0 && select_create_node_id < 0)
                select_create_node_id = 0;
            if (GUI.Button(new Rect(x + 100, y, 100, 40), "create root"))
            {
                if (select_create_node_id >= 0)
                {
                    BNode node = BNodeFactory.sInstance.Create(select_create_node_id);
                    if (cur_tree != null)
                        cur_tree.m_cRoot = node;
                }
            }
            y += 40;
            if (GUI.Button(new Rect(x, y, 200, 40), "clear"))
            {
                if (cur_tree != null)
                    cur_tree.Clear();
            }
            y += 40;
            GUI.Label(new Rect(x, y, 200, 20), "=======================");
            y += 20;
        }
        if (cur_node != null)
        {
            GUI.Label(new Rect(x, y, 300, 20), "Node Type: " + cur_node.GetType().FullName);
            y += 20;
            GUI.Label(new Rect(x, y, 200, 20), "Node Name: " + cur_node.GetName());
            y += 20;
            GUI.Label(new Rect(x, y, 200, 15), "=======================");
            y += 15;
            cur_node.RenderEditor(x, y);
        }
        //
        GUI.EndGroup();
        GUI.EndScrollView();
        //////////////////// draw editor gui /////////////////////
    }


    //editor load data
    public void EditorLoad()
    {
        string filepath = EditorUtility.OpenFilePanel("Bahvior Tree", Application.dataPath, "json");
        if (filepath == "") return;
        string txt = File.ReadAllText(filepath);
        var res = BTreeMgr.sInstance.Load(txt);
        if (res)
        {
            m_jsonPath = filepath;
        }
        ShowNotification(new GUIContent("Load Success"));
    }

    public void EditorSave()
    {
        if (string.IsNullOrEmpty(m_jsonPath))
        {
            EditorSaveAs();
        }
        else
        {
            var data = BTreeMgr.sInstance.ToJsonData();
            File.WriteAllText(m_jsonPath, data.ToJson());
            ShowNotification(new GUIContent("Save Success"));
        }
    }

    public void EditorSaveAs()
    {
        string filepath = EditorUtility.SaveFilePanel("Behavior Tree", Application.dataPath, "", "json");
        Debug.Log(filepath);
        var data = BTreeMgr.sInstance.ToJsonData();
        File.WriteAllText(filepath, data.ToJson());
        ShowNotification(new GUIContent("Save Success"));
    }

    void Update()
    {
        sInstance = this;
        if (select != null)
        {
            Repaint();
        }
    }
}

#endif