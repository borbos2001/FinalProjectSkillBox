using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static AudioClip _audioClip;

    static AudioSource _sound;
    private void Start()
    {
        _sound = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(_sound.isPlaying == false)
        {
            int _numMusic = Random.Range(0, 3);
            _audioClip = Resources.Load<AudioClip>("Music" + _numMusic);
            _sound.PlayOneShot(_audioClip);
        }
    }
}
