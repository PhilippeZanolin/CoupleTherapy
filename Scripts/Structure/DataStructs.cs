using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Sentence
{
    public int _speakerPos; //0 == left, 1 == right
    public string _text;
    public Emotion _emotion;

    public Sentence(int pos, string text, Emotion emotion = Emotion.Neutral) : this()
    {
        this._speakerPos = pos;
        this._text = text;
        this._emotion = emotion;
    }
}

public class Choice
{
    public string _text;
    public int _score;
    public string _nextSpeechID;

    public Choice(string text, int score, string nextID)
    {
        _text = text;
        _score = score;
        _nextSpeechID = nextID;
    }
}

public class Dialogue
{
    public string _id;
    public List<Sentence> _sentences;
    public string _nextSpeechID;


    public Speaker _leftSpeaker;
    public Speaker _rightSpeaker;


    public Dialogue(string id, List<Sentence> sentences, string nextSpeechID, Speaker leftSpeaker, Speaker rightSpeaker)
    {
        _id = id;
        _sentences = sentences;
        _nextSpeechID = nextSpeechID;
        _leftSpeaker = leftSpeaker;
        _rightSpeaker = rightSpeaker;
    }
}

public class MCA
{
    public string _id;
    public List<Choice> _choices;

    public MCA(string id, List<Choice> choices)
    {
        _id = id;
        _choices = choices;
    }
}

public enum Emotion
{
    Neutral,
    Angry,
    Confused,
    Surprised,
    Happy
}



[Serializable]
public class EmotionSpriteAssociation
{
    [SerializeField] public Emotion _emotion;
    [SerializeField] public Sprite _associatedSprite;
}

