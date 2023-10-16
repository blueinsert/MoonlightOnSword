using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责普通运动，走跑...
/// 及空中自由落体运动
/// </summary>
public class MoveComp : ComponentBase
{
    public float RotateValue { get { return m_rotateValue; } }
    public float Speed { get { return m_speed; } }
    public Vector2 VelPreferHorizon { get { return m_hPreferVel; } }

    public Vector2 Facing { get { return m_facingDir; } }

    public CharacterController m_cc;
    //期望速度
    public float m_vPreferVel;
    private bool m_isVPreferVelBeenUsed = false;
    private bool m_isVPreferVelUseOnce = false;

    public Vector2 m_hPreferVel;

    public bool m_isOnGround = false;
    public float m_mass = 60f;
    public float m_friction = 1.2f;
    /// <summary>
    /// 加速度
    /// </summary>
    public float m_accler = 10f;
    public float m_unaccler = 20f;
    /// <summary>
    /// 正数：向左旋转 负数：向右旋转
    /// 用于动画混合
    /// </summary>
    public float m_rotateValue = 0;
    /// <summary>
    /// 当前朝向
    /// </summary>
    public Vector2 m_facingDir;
    /// <summary>
    /// 当前速度
    /// </summary>
    public Vector3 m_vel;
    /// <summary>
    /// 用于控制动画
    /// </summary>
    public float m_speed = 0;
    [Header("重力加速度")]
    public float m_g = -9.7f;
    public bool m_drawGizmons = true;
    //自驱移动：只能往前走，有转弯半径
    public bool m_isSelfDrive = true;

    public PushComp m_pushComp = null;

    public void Start()
    {
        PostInit();
        var forward = this.transform.forward;
        m_facingDir = new Vector2(forward.x, forward.z);
        m_facingDir.Normalize();
        m_vel = Vector3.zero;
        //m_pushComp = GetComp<PushComp>();
    }

    public override void PostInit()
    {
        m_cc = this.GetComponent<CharacterController>();
    }

    #region 外部接口

    public void SetPreferVelHorizon(float vX, float vZ, bool? isSelfDrive = null)
    {
        m_hPreferVel.x = vX;
        m_hPreferVel.y = vZ;
        if(isSelfDrive != null)
            m_isSelfDrive = isSelfDrive.Value;
    }

    public void SetPreferVelVertical(float vY)
    {
        m_vPreferVel = vY;
        m_isVPreferVelBeenUsed = false;
        m_isVPreferVelUseOnce = false;
    }

    public void SetPreferVelVerticalOnce(float vY)
    {
        m_vPreferVel = vY;
        m_isVPreferVelBeenUsed = false;
        m_isVPreferVelUseOnce = true;
    }

    #endregion

    private void UpdateVelAir()
    {
        var deltaTime = TimeManger.Instance.DeltaTime;
        if (m_isVPreferVelUseOnce)
        {
            if (!m_isVPreferVelBeenUsed)
            {
                m_isVPreferVelBeenUsed = true;
                m_vel.y = m_vPreferVel;
            }
        }
        else
        {
            m_vel.y = Mathf.Lerp(m_vel.y, m_vPreferVel, deltaTime * 30f);
        }
        m_vel.y += m_g * Time.deltaTime;
        m_speed = m_vel.magnitude;
    }

    private void UpdateVelGround()
    {
        Vector2 newVel = m_vel;
        var speed = Mathf.Sqrt(m_vel.x * m_vel.x + m_vel.z * m_vel.z);
        var preferDir = m_hPreferVel.normalized;
        var preferSpeed = m_hPreferVel.magnitude;

        if (m_isSelfDrive)
        {
            if (Mathf.Abs(preferSpeed) > 0.001f)
            {
                //lerp的思路
                //var newFacingDir = MathUtil.VectorLerp(m_facingDir, preferDir, 0.5f);
                //var newFacingDir = Vector2.Lerp(m_facingDir, preferDir, Time.deltaTime * 30f);//todo 球面插值
                //if (newFacingDir.magnitude == 0)
                //{
                //    Debug.LogError(string.Format("{0} {1} {2} {3}", m_facingDir, preferDir, 0.5f, newFacingDir));
                //}

                //每帧旋转一定角度的思路
                var newFacingDir = m_facingDir;
                float angle = MathUtil.Vector2Angle(m_facingDir, preferDir);
                var newRotateValue = 0.0f;
                //m_rotateValue = Mathf.Lerp(m_rotateValue, newRotateValue, Time.deltaTime * 40f);//rotate damp
                if (Mathf.Abs(angle) > 5.1f)
                {
                    //旋转当前朝向得到新的朝向
                    float angleSpeed = 720f; //360f;//180 degree per second
                    var degreePerFrame = -Mathf.Sign(angle) * angleSpeed * Time.deltaTime;
                    var rotation = Quaternion.AngleAxis(degreePerFrame, Vector3.up);
                    var t = rotation * new Vector3(m_facingDir.x, 0, m_facingDir.y);
                    newFacingDir = new Vector2(t.x, t.z);

                    newRotateValue = Mathf.Clamp(angle / 90f, -1, 1);
                }
                else
                {
                    newRotateValue = 0;
                }
                m_rotateValue = newRotateValue;
                m_facingDir = newFacingDir.normalized;

            }
           
            var newSpeed = speed;
            if (newSpeed < preferSpeed)
            {
                newSpeed += m_accler * Time.deltaTime;
                newSpeed = Mathf.Clamp(newSpeed, 0, preferSpeed);
            }
            else
            {
                newSpeed += m_unaccler * Time.deltaTime;
                newSpeed = Mathf.Clamp(newSpeed, 0, preferSpeed);
            }

             newVel = m_facingDir * newSpeed;
        }
        else
        {
            speed = preferSpeed;
            newVel = preferSpeed * preferDir;
        }
        m_vel = new Vector3(newVel.x, m_vel.y, newVel.y);
        m_speed = speed;
    }

    private void UpdateVel()
    {
        if (m_vPreferVel > 0 || !m_isOnGround)
            UpdateVelAir();
        else
        {
            UpdateVelGround();
            m_vel.y = 0;
        }
    }

    void Move(Vector3 vel)
    {
        m_cc.Move(vel * Time.deltaTime);
    }

    void UpdatePosition()
    {
        Move(m_vel);
    }

    void UpdateIsOnGround()
    {
        if (m_pushComp != null)
        {
            m_isOnGround = m_pushComp.IsOnGround;
        }else
            m_isOnGround = m_cc.isGrounded;
    }

    public void LookAt(Vector3 targetPos)
    {
        var dir = (targetPos - this.transform.position).normalized;
        m_facingDir = new Vector2(dir.x, dir.z).normalized;
        this.transform.LookAt(this.transform.position + new Vector3(m_facingDir.x, 0, m_facingDir.y) * 3f);
    }

    public override void Tick()
    {
        base.Tick();
        UpdateVel();
        UpdatePosition();
        this.transform.LookAt(this.transform.position + new Vector3(m_facingDir.x, 0, m_facingDir.y) * 3f);
        UpdateIsOnGround();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var pos = transform.position;
        if (m_cc != null) {
            pos += new Vector3(0, m_cc.height / 2.0f, 0);
        }
        Gizmos.DrawLine(pos, pos + transform.forward);
    }
}
