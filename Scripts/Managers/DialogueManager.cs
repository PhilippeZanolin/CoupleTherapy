using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] SpeakerHolder _speakerRightUI;
    [SerializeField] SpeakerHolder _speakerLeftUI;

    Speaker _speakerRight;
    Speaker _speakerLeft;
    List<Sentence> _currentSpeech;
    int _currentSentenceIndx = 0;

    string _nextSpeechID;

    bool _dialogueActive = false;
    public MyIDEvent OnNextSpeech;

    public void Initialize(Dialogue d)
    {
        _currentSpeech = d._sentences;
        _nextSpeechID = d._nextSpeechID;


        _speakerLeftUI.ChangeSpeaker(d._leftSpeaker);
        _speakerRightUI.ChangeSpeaker(d._rightSpeaker);
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _dialogueActive)
        {
            NextSentence();
        }

        //Cheats
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartNewDialogue();
            NextSentence();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            MuteDialogue();
        }
        */
    }

    public void StartNewDialogue()
    {
        _dialogueActive = true;
        _currentSentenceIndx = 0;
        NextSentence();
    }

    
    void NextSentence()
    {
        //Behaviour when one of the speaker speak.
        if (_speakerLeftUI.IsSpeaking() || _speakerRightUI.IsSpeaking())
        {
            _speakerRightUI.FinishSpeaking();
            _speakerLeftUI.FinishSpeaking();
            return;
        }

        if (_currentSpeech.Count > _currentSentenceIndx)
        {
            Sentence sentence = _currentSpeech[_currentSentenceIndx];

            
            if (sentence._speakerPos == 0)
            {
                _speakerRightUI.MuteSpeaker();
                _speakerLeftUI.SpeakMode(sentence);              
            }
            else
            {
                _speakerLeftUI.MuteSpeaker();
                _speakerRightUI.SpeakMode(sentence);
            }

            _currentSentenceIndx++;
        }
        else
        {
            NextDialog();
        }
    }

    void NextDialog()
    {
        MuteDialogue();
        OnNextSpeech.Invoke(_nextSpeechID);
    }

    public void MuteDialogue()
    {
        _dialogueActive = false;
        _speakerRightUI.MuteSpeaker();
        _speakerLeftUI.MuteSpeaker();
    }



}




