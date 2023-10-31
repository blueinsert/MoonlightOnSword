using Game.AIBehaviorTree;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComp : ComponentBase {

    public BTree Tree { get { return m_tree; } }

    public TextAsset m_json;
    public string m_treeName = "default";
    private BTree m_tree;
    private AIInput m_aiInput;

    public void Start()
    {
        m_aiInput = new AIInput();
        m_aiInput.Initialize(GetComp<EntityComp>());
        //BTreeMgr.sInstance.Load(m_json.text);
        if (m_json != null) {
            JsonData json = JsonMapper.ToObject(m_json.text);
            json = json["trees"];
            int count = json.Count;
            for (int i = 0; i < count; i++)
            {
                JsonData data = json[i];
                if (m_treeName == data["name"].ToString())
                {
                    BTree bt = new BTree();
                    bt.ReadJson(data);
                    m_tree = bt;
                    break;
                }

            }
        }
        
        if(m_tree == null)
        {
            Debug.LogError("AIComp:m_tree == null");
        }
    }

    public override void Tick()
    {
        if (m_tree != null)
        {
            m_tree.ClearDebugDataBeforeRun();
            m_tree.Run(this.m_aiInput);
        }
    }


}
