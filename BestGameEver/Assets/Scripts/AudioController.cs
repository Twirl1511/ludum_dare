using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController singleton;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _buildSound;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] private AudioClip _platformMoveSound;
    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBuildSound()
    {
        _audioSource.clip = _buildSound;
        if (!_audioSource.isPlaying)
            _audioSource.Play();

    }
    public void PlayScreamSound()
    {
        _audioSource.clip = _screamSound;
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
    public void PlayPlatformSound()
    {
        _audioSource.clip = _platformMoveSound;
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }


}

