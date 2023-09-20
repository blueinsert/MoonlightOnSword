using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsDesc : MonoBehaviour
{
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
        InitializeFromSequences();
        m_skillDic = null;
    }

    public void Start()
    {
        Debug.Log("SkillDesc2:Start");
        InitializeFromSequences();
    }

    private void InitializeFromSequences()
    {
        m_skillList.Clear();
        foreach (var sequence in this.GetComponentsInChildren<FSequence>())
        {
            var skillCfg = sequence.ToSkillConfig();
            m_skillList.Add(skillCfg);
        }
    }
}