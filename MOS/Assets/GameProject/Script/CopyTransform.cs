using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluebean.ProjectD
{
	[ExecuteInEditMode]
    public class CopyTransform : MonoBehaviour
    {
       
	   public Transform m_target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
			if (m_target != null){
               this.transform.position = m_target.position;
			   this.transform.rotation = m_target.rotation;
			   //this.transform.lossyScale = m_target.lossyScale;
			}
            
        }

    }
}
