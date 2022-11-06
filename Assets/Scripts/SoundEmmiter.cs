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
            _audioSource.clip = audioClip;
            if (_audioSource.clip)
            {
                StartCoroutine(PlaySoundAfterSpawn());
            }
        }
    }
    IEnumerator PlaySoundAfterSpawn()
    {
        yield return null;
        _audioSource.Play();
    }
}
