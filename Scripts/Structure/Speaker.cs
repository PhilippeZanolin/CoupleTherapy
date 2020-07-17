using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObjects/CharacterScriptableObject", order = 1)]
public class Speaker : ScriptableObject
{
    public string _nameID;
    public Emotion _currentEmotion = Emotion.Neutral;
    public AudioClip _voice;
    /*
    [SerializeField]
    private EmotionSpriteDictionary _emotionSpriteStore = EmotionSpriteDictionary.New<EmotionSpriteDictionary>();
    private Dictionary<Emotion, Sprite> _emotionSpriteDictionary
    {
        get { return _emotionSpriteStore.dictionary; }
    }
    */


    public List<EmotionSpriteAssociation> _emotionSpriteAssociation = new List<EmotionSpriteAssociation>();
    //public EmotionSpriteDictionary _dictionary;

    public Sprite GetCurrentEmotionSprite()
    {
        foreach (EmotionSpriteAssociation association in _emotionSpriteAssociation)
        {
            if (association._emotion == _currentEmotion) return association._associatedSprite;
        }
        return null;
    }
}