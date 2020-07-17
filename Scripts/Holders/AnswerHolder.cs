using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerHolder : MonoBehaviour
{

    [SerializeField] CustomAnswerButton _answerButton;
    [SerializeField] TextMeshProUGUI _answerTextMesh;

    public MyChoiceSelectedEvent OnChoiceSelected;

    Choice _selectedChoice;
    public void SetChoice(Choice choice) { _selectedChoice = choice; }

    private void Awake()
    {
        
        _answerButton.onClick.AddListener(OnValidChoice);
    }

    void Start()
    {
        _answerTextMesh.text = _selectedChoice._text;
    }

    public void CreateAnswer(Choice choice)
    {
        _selectedChoice = choice;
        _answerTextMesh.text = choice._text;       
    }


    void OnValidChoice()
    {
        OnChoiceSelected.Invoke(_selectedChoice);
    }
}
