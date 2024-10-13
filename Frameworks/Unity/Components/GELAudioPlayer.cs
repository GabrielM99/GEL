using UnityEngine;

namespace Fusyon.GEL.Unity
{
    [RequireComponent(typeof(AudioSource))]
    public class GELAudioPlayer : GELBehaviour<AudioSource>, IGELAudioPlayer
    {
        public void Play(object audio, bool loop = false)
        {
            if (audio is AudioClip audioClip)
            {
                if (loop)
                {
                    Base.clip = audioClip;
                    Base.loop = true;
                    Base.Play();
                }
                else
                {
                    Base.PlayOneShot(audioClip);
                }
            }
        }
    }
}