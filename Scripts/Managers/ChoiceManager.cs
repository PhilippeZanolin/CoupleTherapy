using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceManager : MonoBehaviour
{

    List<Choice> _currentChoices;

    [SerializeField] AnswerHolder _choicePref;
    [SerializeField] Transform _choicesHolder;
    [SerializeField] Image _backGround;


    [SerializeField] AudioSource _validSound;

    public MyIDEvent OnNextSpeech;

    int _totalScore = 0;
    public int GetScore() { return _totalScore; }

    private void OnEnable()
    {
        _totalScore = 0;
    }

    public void Initialize(MCA mca)
    {
        _currentChoices = mca._choices;
        CreateChoicesHolders();
    }
    public void StartChoices()
    {
        _backGround.gameObject.SetActive(true);
        _choicesHolder.gameObject.SetActive(true);
    }

 
    void CreateChoicesHolders()
    {
        foreach(Transform child in _choicesHolder)
        {
            Destroy(child.gameObject);
        }
        foreach (Choice choice in _currentChoices)
        {
            AnswerHolder choiceHolder =  Instantiate(_choicePref, _choicesHolder.transform);
            choiceHolder.SetChoice(choice);
            choiceHolder.OnChoiceSelected.AddListener(OnChoiceSelected);
        }
    }
    
    void OnChoiceSelected(Choice choice)
    {
        _totalScore += choice._score;
        if (_validSound) _validSound.Play();
        OnNextSpeech.Invoke(choice._nextSpeechID);
    }


    public void Mute()
    {
        _choicesHolder.gameObject.SetActive(false);
        _backGround.gameObject.SetActive(false);
    }
}
