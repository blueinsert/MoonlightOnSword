using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  BNodeAction.cs
//  Author: Lu Zexi
//  2014-06-07


namespace Game.AIBehaviorTree
{
    /// <summary>
    /// 执行节点
    /// </summary>
    public class BNodeAction : BNode
    {
        //private bool over = false;
        private float m_ftime;

        public BNodeAction()
			:base()
		{
			this.m_strName = "Action";
		}

        public override void OnEnter(BInput input)
        {
            base.OnEnter(input);
            //this.over = false;
            this.m_ftime = TimeManger.Instance.CurTime;
        }

        protected virtual float GetDuration()
        {
            return 0.5f;
        }

        protected bool IsFinish()
        {
            return TimeManger.Instance.CurTime - this.m_ftime > GetDuration();
        }

    }
}
