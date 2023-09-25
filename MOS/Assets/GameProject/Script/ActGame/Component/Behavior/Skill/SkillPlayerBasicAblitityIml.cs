using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlayerBasicAblitityIml : BasicAblitityIml
{
    public SkillPlayer m_skillPlayer = null;

    public void SetSkillPlayer(SkillPlayer skillPlayer)
    {
        m_skillPlayer = skillPlayer;
    }

    public override void PlaySkill(int id)
    {
        m_skillPlayer.SetNextSkill(id);
    }
}
