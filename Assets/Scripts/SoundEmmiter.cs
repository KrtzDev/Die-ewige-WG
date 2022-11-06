using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmmiter : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clip;

    public void PlayAudio(AudioClip audioClip)
    {
        if (_audioSource)
        {
            _audioSource.clip = _clip;
            if (_audioSource.clip)
            {
                _audioSource.Play();
            }
        }
    }
}
