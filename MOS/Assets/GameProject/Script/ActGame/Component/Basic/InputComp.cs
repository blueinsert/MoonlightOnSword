using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInputMapDic
{
    [SerializeField]
    public int PlayerIndex;
    [SerializeField]
    public CampType CampType;
    [SerializeField]
    public KeyCode Left;
    [SerializeField]
    public KeyCode Right;
    [SerializeField]
    public KeyCode Up;
    [SerializeField]
    public KeyCode Down;
    [SerializeField]
    public KeyCode Attack;
    [SerializeField]
    public KeyCode Defense;
}

/// <summary>
/// 根据配置从输入中提取对应部分，形成及存储输入指令
/// </summary>
public class InputComp : ComponentBase {
    /// <summary>
    /// 输入方向
    /// </summary>
    public Vector2 Dir { get { return m_moveValue; } }
    public Vector2 m_moveValue;

    //摄像机控制变量
    public Vector2 m_rotateValue;
    public Vector2 RotateValue { get { return m_rotateValue; } }
    public Vector3 m_lastMousePosition;
    public Vector2 m_scrollValue;
    public Vector2 ScrollValue { get { return m_scrollValue; } }

    public bool IsAttackClick { 
        get { 
            return m_isAttackClick;
        } 
    }

    public bool IsBlcokHoldon
    {
        get
        {
            return m_isBlockHoldon;
        }
    }

    public bool m_isBlockHoldon = false;
    public float m_blockClickTime = float.MinValue;
    public bool m_isAttackClick = false;
    public float m_attackClickTime = float.MinValue;

    public InputMapingDesc m_intpuMappingDesc = null;
    public EntityComp m_entity = null;

    private void Start()
    {
        PostInit();
        m_intpuMappingDesc = FindObjectOfType<InputMapingDesc>();
        m_entity = GetComp<EntityComp>();
    }

    public override void PostInit()
    {
       
    }

    public override void Tick()
    {
        base.Tick();
        var inputMapping = m_intpuMappingDesc.GetInputMapping(m_entity.m_playerIndex, m_entity.CampType);
        if (inputMapping == null)
            return;
        Vector2 moveValue = Vector2.zero;
        if (Input.GetKey((KeyCode)inputMapping.Up))
        {
            moveValue.y = 1;
        }
        if (Input.GetKey(inputMapping.Down))
        {
            moveValue.y = -1;
        }
        if (Input.GetKey(inputMapping.Left))
        {
            moveValue.x = -1;
        }
        if (Input.GetKey(inputMapping.Right))
        {
            moveValue.x = 1;
        }
        moveValue = moveValue.normalized;
        m_moveValue = Vector2.Lerp(m_moveValue, moveValue, Time.deltaTime*30f);//input damp,speed damp
        if (Mathf.Abs(m_moveValue.x)< 0.001f)
        {
            m_moveValue.x = 0;
        }
        if (Mathf.Abs(m_moveValue.y) < 0.001f)
        {
            m_moveValue.y = 0;
        }

        var mouseDelta = Input.mousePosition - m_lastMousePosition;
        m_rotateValue = new Vector2(mouseDelta.x, -mouseDelta.y);
        m_lastMousePosition = Input.mousePosition;
        m_scrollValue = Input.mouseScrollDelta;

        if (Input.GetKeyUp(inputMapping.Attack))
        {
            m_isAttackClick = true;
            m_attackClickTime = TimeManger.Instance.CurTime;
        }
        m_isAttackClick = TimeManger.Instance.CurTime < m_attackClickTime + 10 / 60f;//缓存指令

        if (Input.GetKey(inputMapping.Defense))
        {
            m_isBlockHoldon = true;
            m_blockClickTime = TimeManger.Instance.CurTime;
        }
        m_isBlockHoldon = TimeManger.Instance.CurTime < m_blockClickTime + 10 / 60f;
    }
}
