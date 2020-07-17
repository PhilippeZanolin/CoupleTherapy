using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SimpleDialogHolder : MonoBehaviour
{

    [SerializeField] string _textToWrite = "";
    public void SetText(string text) { _textToWrite = text; }

    [Range(1, 10)]
    [SerializeField] float _typingSpeed = 0.5f;

    [SerializeField] TextMeshProUGUI _textDisplayed;
    [SerializeField] Image _dialogBox;

    [SerializeField] Sprite _neutralBubble;
    [SerializeField] Sprite _angryBubble;

    public AudioSource _voiceSource;

    bool _angryMode;
    public void SetAngryMode(bool angry)
{
        _angryMode = angry;
    }

    private void Awake()
    {
        if (!_voiceSource) _voiceSource = this.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _textDisplayed.text = "";
        Mute();
    }

    public bool _typing = false;
    public void DisplayText()
    {
       
        if (_typing)
        {
            //On termine de parler.
            StopAllCoroutines();
            _textDisplayed.text = _textToWrite;
            _typing = false;
            _voiceSource.Pause();
        }
        else
        {
            _dialogBox.sprite = _angryMode ? _angryBubble : _neutralBubble;
            _dialogBox.enabled = true;
            StartCoroutine(TypeText());
        }
        
    }

    public void Mute()
    {
        StopAllCoroutines();
        _textDisplayed.text = "";
        _dialogBox.enabled = false;
        _textDisplayed.enabled = false;
        _typing = false;
    }

    public void SetUpVoice(AudioClip voice)
    {
        _voiceSource.clip = voice;
        _voiceSource.loop = true;
    }

    IEnumerator TypeText()
    {
        _typing = true;
        _textDisplayed.text = "";
        _textDisplayed.enabled = true;
        _voiceSource.Play();


        if (_angryMode) _textToWrite = _textToWrite.ToUpper();
        foreach(char letter in _textToWrite.ToCharArray())
        {    
            _textDisplayed.text += letter;
            yield return new WaitForSeconds(_typingSpeed/100);
        }

        _voiceSource.Pause();
        _typing = false;
    }

}
