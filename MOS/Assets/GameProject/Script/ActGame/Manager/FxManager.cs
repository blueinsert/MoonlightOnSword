using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluebean.ProjectD
{
    public class FxManager : MonoBehaviour
    {
        public static FxManager Instance { get{ return s_instance; } }
        public static FxManager s_instance;

        public GameObject[] fx_prefabs;

        private void Awake() {
			s_instance = this;
		}

        public void Play(FxEffectObj effect){
			var prefab = fx_prefabs[effect.m_id];
			var go = Instantiate(prefab, effect.m_pos, Quaternion.identity);	
			effect.m_go = go;
		}
    }
}
