using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnswersManager : MonoBehaviour
{
    
    [SerializeField] List<AnswerHolder> _answerChoices;

    string _currentDialogSelected;


    void Awake()
    {
        
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSelection(string dialogSelected)
    {
        Debug.Log("current text selected : " + dialogSelected);
    }
    
}
