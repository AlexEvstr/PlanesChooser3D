using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _tap;
    [SerializeField] private AudioClip _select;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _win2;
    [SerializeField] private AudioClip _lose;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_click);
    }

    public void TapSound()
    {
        _audioSource.PlayOneShot(_tap);
    }

    public void SelectSound()
    {
        _audioSource.PlayOneShot(_select);
    }

    public void WinSound()
    {
        _audioSource.PlayOneShot(_win);
    }

    public void Win2Sound()
    {
        _audioSource.PlayOneShot(_win2);
    }

    public void LoseSound()
    {
        _audioSource.PlayOneShot(_lose);
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
