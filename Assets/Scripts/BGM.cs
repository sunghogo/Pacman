using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _start;
    [SerializeField] private AudioClip _siren0Start;
    [SerializeField] private AudioClip _siren0;
    private List<AudioClip> _clips;
    

    public void NextClip() {
        _audioSource.loop = false;
        _audioSource.Stop();
        _clips.RemoveAt(0);
        _audioSource.clip = _clips[0];
    }

    IEnumerator PlayStart() {
        _audioSource.Play();
        yield return new WaitWhile(() => _audioSource.isPlaying);
        NextClip();
        GameManager.Instance.StartGame();
    }

    IEnumerator PlaySiren() {
        _audioSource.Play();
        yield return new WaitWhile(() => _audioSource.isPlaying);
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _clips = new List<AudioClip> { _start, _siren0Start, _siren0 };
        _audioSource.clip = _clips[0];
        StartCoroutine(PlayStart());
    }

    void Update()
    {
        if (GameManager.Instance.isStarted && !_audioSource.isPlaying) StartCoroutine(PlaySiren());
    }
}
