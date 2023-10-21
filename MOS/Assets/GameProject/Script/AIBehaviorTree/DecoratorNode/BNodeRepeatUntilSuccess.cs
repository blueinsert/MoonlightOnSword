using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.AIBehaviorTree
{
    public class BNodeRepeatUntilSuccess : BNodeDecorator
    {
        private bool m_isRetrying = false;
        private float m_lastFailedTime = 0;
        private const float RetryPeriod = 0.5f;

        public BNodeRepeatUntilSuccess()
            : base()
        {
            this.m_strName = "RepeatUntilSuccess";
        }

        //onenter
        public override void OnEnter(BInput input)
        {
        }

        //exceute
        public override ActionResult Excute(BInput input)
        {
            if (m_lstChildren.Count == 0)
                return ActionResult.SUCCESS;
            if (m_lstChildren.Count > 1)
            {
                //not support
                return ActionResult.FAILURE;
            }
            if (m_isRetrying)
            {
                var cur = TimeManger.Instance.CurTime;
                if (cur < m_lastFailedTime + RetryPeriod)
                    return ActionResult.RUNNING;
                else
                    m_isRetrying = false;
            }
            ActionResult res =  this.m_lstChildren[0].RunNode(input);
            if (res == ActionResult.SUCCESS)
                return ActionResult.SUCCESS;

            if (res == ActionResult.FAILURE) {
                m_isRetrying = true;
                m_lastFailedTime = TimeManger.Instance.CurTime;
                return ActionResult.RUNNING;
            }
            return ActionResult.RUNNING;
        }
    }
}
