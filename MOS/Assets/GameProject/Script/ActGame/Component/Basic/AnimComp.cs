﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimComp : ComponentBase {

	public Animator m_animator;

	public float m_lastSpeed = 1;

	public void Start()
	{
		PostInit();
    }

	public override void PostInit()
	{
        m_animator = this.gameObject.GetComponentInChildren<Animator>();
	}

	public void SetSpeed(float speed)
	{
        m_animator.speed = speed;
    }

	public void PlayAnim(string animName, bool useTrigger = false)
	{
        if(useTrigger)
        {
            m_animator.SetTrigger(animName);
        }
        else
        {
            m_animator.Play(animName);
        }
	}

	public void PlayAnimFrom(string animName, float normalizedTime = 0f)
	{
		m_animator.Play(animName, -1, normalizedTime:normalizedTime);
	}

	public void Walk(float speed, float rotateValue)
	{
		m_animator.SetFloat("MoveSpeed", speed);
        m_animator.SetFloat("RotateValue", rotateValue);
    }

	public void Blocking(bool isOn)
	{
		m_animator.SetBool("IsBlocking", isOn);
	}

	public void GetHit(int level,int posType)
	{
		m_animator.SetTrigger("BeHit");
		m_animator.SetInteger("HitForceType", level);
		m_animator.SetInteger("HitPosType", posType);
        m_animator.SetBool("HitFly", false);
    }

	public void Dead(int type)
	{
        m_animator.SetTrigger("Dead");
    }

	public void BeHitFly()
	{
        m_animator.SetTrigger("BeHit");
		m_animator.SetBool("HitFly", true);
    }

	public void SetVY(float vy)
	{
		m_animator.SetFloat("VY", vy);
	}

	public void SetLanding(bool landing)
	{
		m_animator.SetBool("IsLanding", landing);
	}

	public void Getup()
	{
		m_animator.SetTrigger("Getup");
	}

	public void ExitHit()
	{
        m_animator.SetTrigger("BeHitFinish");

        m_animator.SetBool("HitFly", false);

		SetVY(0);
    }

	public void SetTurnProgress(float v)
	{
		m_animator.SetFloat("TurnProgress", v);
	}

	public void Freeze()
	{
		m_lastSpeed = m_animator.speed;
        m_animator.speed = 0;
	}

	public void UnFreeze()
	{
		m_animator.speed = m_lastSpeed;
	}

}
