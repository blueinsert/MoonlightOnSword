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

public enum BTreeWinMode
{
    Edit,
    Debug,
}

//BTreeWin
public class BTreeWin : EditorWindow
{
    const string PrefsKey_LastLoadPath = "LastLoadPath";

    public static int NODE_WIDTH = 20;
    public static int NODE_HEIGHT = 20;
    public static int GUI_WIDTH = 240;

    public static BTreeWinMode m_curMode = BTreeWinMode.Edit;
    public static BTreeWinMode m_lastMode = BTreeWinMode.Edit;

    public static BNode m_copySource = null;

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

    private static string m_jsonPath = "";

    private static int cur_gameObject_index = -1;

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
        this.m_editUIScrollPos = GUI.BeginScrollView(new Rect(position.width - GUI_WIDTH, 0, GUI_WIDTH, position.height), this.m_editUIScrollPos, new Rect(0, 0, this.maxSize.x, this.maxSize.y));
        //GUI.BeginGroup(new Rect(position.width - GUI_WIDTH, 0, GUI_WIDTH, 1000));
        //GUI.BeginGroup(new Rect(0, 0, GUI_WIDTH,1000));

        int x = 0;
        int y = 0;
        GUI.Label(new Rect(x, y, 200, 20), "Mode:");
        y += 20;
        m_curMode = (BTreeWinMode)EditorGUI.EnumPopup(new Rect(x, y, 1000, 20), m_lastMode);
        y += 20;
        if (m_lastMode != m_curMode)
        {
            OnModeChange(m_curMode);
            m_lastMode = m_curMode;
        }
        if (m_curMode == BTreeWinMode.Edit)
        {
            DrawGUI_EditMode(x, y);
        }
        else if (m_curMode == BTreeWinMode.Debug)
        {
            DrawGUI_DebugMode(x, y);
        }

        //GUI.EndGroup();
        GUI.EndScrollView();
        //////////////////// draw editor gui /////////////////////
    }

    private void OnModeChange(BTreeWinMode newMode)
    {

    }

    private void Clear()
    {
        cur_tree = null;
        cur_node = null;
        cur_tree_index = -1;
        last_tree_index = -1;
        select_create_node_id = -1;
        select = null;
    }

    private void DrawGUI_EditMode(int x, int y)
    {

        //var index = m_jsonPath.IndexOf("Assets");
        EditorGUI.LabelField(new Rect(x, y, 1000, 20), string.Format("path:{0}", m_jsonPath));
        y += 20;
        List<BTree> lst = BTreeMgr.sInstance.GetTrees();
        if (GUI.Button(new Rect(x, y, 200, 40), "Load"))
        {
            Clear();
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
    }

    private void DrawGUI_DebugMode(int x, int y)
    {
        //attach
        if (EditorApplication.isPlaying)
        {
            var ais = FindObjectsOfType<AIComp>();
            string[] gameObjectName = new string[ais.Length];
            for (int i = 0; i < ais.Length; i++)
            {
                gameObjectName[i] = ais[i].gameObject.name;
            }
            if (gameObjectName.Length != 0 && cur_gameObject_index < 0)
                cur_gameObject_index = 0;
            cur_gameObject_index = EditorGUI.Popup(new Rect(x, y, 200, 45), cur_gameObject_index, gameObjectName);
            y += 20;
            if (GUI.Button(new Rect(x, y, 200, 40), "Attach"))
            {
                if (cur_gameObject_index >= 0) {
                    var aiComp = ais[cur_gameObject_index];
                    Clear();
                    BTreeMgr.sInstance.Clear();
                    BTreeMgr.sInstance.Add(aiComp.Tree);
                    cur_tree = aiComp.Tree;
                }
            }
            y += 40;
        }
    }

    //editor load data
    public void EditorLoad()
    {
        string path = Application.dataPath;
        if (EditorPrefs.HasKey(PrefsKey_LastLoadPath))
        {
            path = EditorPrefs.GetString(PrefsKey_LastLoadPath);
            if (!File.Exists(path))//fall back
            {
                path = Application.dataPath;
            }
            //path = Path.GetDirectoryName(path);
        }
        string filepath = EditorUtility.OpenFilePanel("Bahvior Tree", path, "json");
        if (filepath == "") return;
        string txt = File.ReadAllText(filepath);
        var res = BTreeMgr.sInstance.Load(txt);
        if (res)
        {
            m_jsonPath = filepath;
        }
        ShowNotification(new GUIContent("Load Success"));
        EditorPrefs.SetString(PrefsKey_LastLoadPath, filepath);
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