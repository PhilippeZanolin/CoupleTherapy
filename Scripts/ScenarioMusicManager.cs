using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMusicManager : MonoBehaviour
{

    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _mainLoopAUdio;
    [SerializeField] AudioClip _startAudio;
    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(FadeMusic());
    }

    IEnumerator FadeMusic()
    {
        _source.clip = _startAudio;
        _source.loop = false;
        _source.Play();
        while (_source.isPlaying)
        {
            yield return null;
        }
        _source.clip = _mainLoopAUdio;
        _source.loop = true;
        _source.Play();
    }
}
