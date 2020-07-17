using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerHolder : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float _silentAlpha = 0.85f;

    [SerializeField] Sprite _currentSprite;
    [SerializeField] Image _speakerImage;
    [SerializeField] SimpleDialogHolder _dialogBox;

    Speaker _currentSpeaker;
    public void ChangeSpeaker(Speaker sp)
    {
        //animation
        _currentSpeaker = sp;
        if (_currentSpeaker)
        {
            _speakerImage.enabled = true;
            sp._currentEmotion = Emotion.Neutral;
            _dialogBox.SetUpVoice(sp._voice);
            RefreshSpeakerState();
        }
        else
        {
            _speakerImage.enabled = false;
        }
    }


    private void Awake()
    {
        MuteSpeaker();
    }

    public bool IsSpeaking()
    {
        return _dialogBox._typing;
    }
    public void FinishSpeaking()
    {
        if(IsSpeaking())_dialogBox.DisplayText();
    }

    public void RefreshSpeakerState()
    {
        _speakerImage.sprite = _currentSpeaker.GetCurrentEmotionSprite();
        _dialogBox.SetAngryMode(_currentSpeaker._currentEmotion == Emotion.Angry);
    }

    public void SpeakMode(Sentence sentence)
    {
        if(_currentSpeaker == null)
        {
            Debug.LogWarning("appel d'une sentence sur personne non existant");
            return;
        }
        var tempColor = _speakerImage.color;
        tempColor.a = 1;

        _speakerImage.color = tempColor;

        _currentSpeaker._currentEmotion = sentence._emotion;
        RefreshSpeakerState();

        _dialogBox.SetText(sentence._text);
        _dialogBox.DisplayText();      
    }

    public void MuteSpeaker()
    {
        _dialogBox.Mute();
        var tempColor = _speakerImage.color;
        tempColor.a = _silentAlpha;
        _speakerImage.color = tempColor;

    }
}
