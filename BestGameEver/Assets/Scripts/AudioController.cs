using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController singleton;
    [SerializeField] private AudioSource _buildSound;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _screamSound;
    [SerializeField] private AudioSource _platformMoveSound;
    [SerializeField] private AudioSource _pipeTensionSound;
    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {

    }

    public void PlayBuildSound()
    {
        if (!_buildSound.isPlaying)
            _buildSound.Play();
    }
    public void PlayScreamSound()
    {
        if (!_screamSound.isPlaying)
            _screamSound.Play();
    }
    public void StopScreamSound()
    {
        if (_screamSound.isPlaying)
            _screamSound.Stop();
    }
    public void PlayPlatformSound()
    {
        if (!_platformMoveSound.isPlaying)
            _platformMoveSound.Play();
    }
    public void PlayClickSound()
    {
        if (!_clickSound.isPlaying)
            _clickSound.Play();
    }

    private bool _tensFlag = true;
    public void PlayPipeTensionSound()
    {
        if (_tensFlag)
        {
            _pipeTensionSound.Play();
            _tensFlag = false;
            Invoke(nameof(PlayPipeTensionSoundONN), 3);
        }
    }

    public void PlayPipeTensionSoundONN()
    {
        _tensFlag = true;
    }
    public void PlayPipeTensionSoundOFF()
    {
        _tensFlag = false;
    }
}

