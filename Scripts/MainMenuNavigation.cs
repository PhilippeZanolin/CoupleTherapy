using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{

    [SerializeField] GameObject _doorEntranceCanvas;
    [SerializeField] GameObject _creditCanvas;

    [SerializeField] string _scenario1SceneName;

    [SerializeField] AudioSource _mainMenuMusic;

    [SerializeField] AudioClip _mainLoopAUdio;
    [SerializeField] AudioClip _startAudio;

    int knokLeft = 3;

    private void Awake()
    {
        if (!_mainMenuMusic) GetComponent<AudioSource>();
    }


    private void Start()
    {
        StopAllCoroutines();
        DoorMenu();
        StartCoroutine(FadeMusic());

    }

    IEnumerator FadeMusic()
    {
        _mainMenuMusic.clip = _startAudio;
        _mainMenuMusic.loop = false;
        _mainMenuMusic.Play();
        while (_mainMenuMusic.isPlaying)
        {
            yield return null;
        }
        Debug.Log("mainMusic");
        _mainMenuMusic.clip = _mainLoopAUdio;
        _mainMenuMusic.loop = true;
        _mainMenuMusic.Play();
    }

    public void DoorMenu()
    {
        _doorEntranceCanvas.SetActive(true);
        _creditCanvas.SetActive(false);
    }

    public void CreditMenu()
    {
        _creditCanvas.SetActive(true);
        _doorEntranceCanvas.SetActive(false);
    }

    public void LoadScenario1()
    {
        SceneManager.LoadScene(_scenario1SceneName);
    }

    public void QuitApp()
    {
        GameLifeManager.QuitApp();
    }

}
