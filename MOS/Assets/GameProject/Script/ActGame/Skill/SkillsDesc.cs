using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillSequenceResourceContainer))]
public class SkillsDesc : MonoBehaviour
{
    public SkillSequenceResourceContainer m_skillContainer = null;
        
    public List<SkillConfig> m_skillList;

    private Dictionary<int, SkillConfig> m_skillDic = null;

    public SkillConfig GetSkillConfig(int id)
    {
        if (m_skillDic == null)
        {
            m_skillDic = new Dictionary<int, SkillConfig>();
            if (m_skillList != null)
            {
                foreach (var skill in m_skillList)
                {
                    m_skillDic.Add(skill.ID, skill);
                }
            }
        }
        if (m_skillDic.ContainsKey(id))
        {
            return m_skillDic[id];
        }
        return null;
    }

    public void Awake()
    {
        //Debug.Log("SkillDesc2:Awake");
        //InitializeFromSequences();
    }

    public void RefreshSkills()
    {
        m_skillContainer.LoadSkill();
        InitializeFromSequences();
        m_skillDic = null;
    }

    public void Start()
    {
        Debug.Log("SkillDesc2:Start");
        m_skillContainer = GetComponent<SkillSequenceResourceContainer>();
        m_skillContainer.LoadSkill();
        InitializeFromSequences();
    }

    private void InitializeFromSequences()
    {
        m_skillList.Clear();
        foreach (var item in m_skillContainer.AssetList)
        {
            var skillCfg = (item.RuntimeAssetCache as GameObject).GetComponent<FSequence>().ToSkillConfig();
            m_skillList.Add(skillCfg);
        }
    }
}