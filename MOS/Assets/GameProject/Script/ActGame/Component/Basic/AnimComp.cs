using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimComp : ComponentBase {

	public Animator m_animator;


	public void Start()
	{
		PostInit();
    }

	public override void PostInit()
	{
        m_animator = this.gameObject.GetComponentInChildren<Animator>();
	}

	public void PlayAnim(string animName, float transDuration = 0.05f)
	{
		//m_animator.Play(animName);
		m_animator.CrossFade(animName, 0.06f);
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

	public void SetTurnProgress(float v)
	{
		m_animator.SetFloat("TurnProgress", v);
	}

	public void Freeze()
	{
		m_animator.speed = 0;
	}

	public void UnFreeze()
	{
		m_animator.speed = 1;
	}

}
