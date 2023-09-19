using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool m_isAttackClick = false;
    public float m_attackClickTime = float.MinValue;

    private void Start()
    {
        PostInit();
    }

    public override void PostInit()
    {
       
    }

    public override void Tick()
    {
        base.Tick();
        Vector2 moveValue = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveValue.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveValue.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveValue.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
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

        if (Input.GetMouseButtonUp(0))
        {
            m_isAttackClick = true;
            m_attackClickTime = TimeManger.Instance.CurTime;
        }
        m_isAttackClick = TimeManger.Instance.CurTime < m_attackClickTime + 10 / 60f;
    }
}
