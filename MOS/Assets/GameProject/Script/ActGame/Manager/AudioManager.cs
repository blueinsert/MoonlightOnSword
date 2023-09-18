using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bluebean.ProjectD
{
	public class AudioChannel : MonoBehaviour
    {
        public enum AudioChannelType
        {
            Default = 0,
            Player,
            HitSnd,
            Env,
            Reserve1,
            Reserve2,
            Reserve3,
        }

        private AudioSource m_audioSource;

        private void Awake()
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Play(AudioClip clip, float volume)
        {
            m_audioSource.PlayOneShot(clip, volume);
        }

        public void Update()
        {
           
        }
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get{ return s_instance; } }
        public static AudioManager s_instance;
        public List<AudioClip> m_audioClips = new List<AudioClip>();
       private Dictionary<AudioChannel.AudioChannelType, AudioChannel> m_audioChannelDic = new Dictionary<AudioChannel.AudioChannelType, AudioChannel>();

		private void Awake()
        {
			s_instance = this;
            for(int i = 0; i < System.Enum.GetNames(typeof(AudioChannel.AudioChannelType)).Length; i++)
            {
                AudioChannel channel = gameObject.AddComponent<AudioChannel>();
                m_audioChannelDic.Add((AudioChannel.AudioChannelType)i, channel);
            }    
        }

		public void Play(AudioEffectObj effect){
            m_audioChannelDic[effect.m_channelType].Play(m_audioClips[effect.m_id],1.0f);
		}
    }
}
