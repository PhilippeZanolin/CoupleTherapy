using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] DialogueManager _dialogueManager;
    [SerializeField] ChoiceManager _choiceManager;

    [SerializeField] Speaker _speaker1Exemple;
    [SerializeField] Speaker _speaker2Exemple;


    [SerializeField] List<Speaker> _speakersList;

    int _finalScore = 0;

    List<Dialogue> _dialoguesList = new List<Dialogue>();
    List<MCA> _choicesList = new List<MCA>();

    public void Start()
    {
        _dialogueManager.gameObject.SetActive(false);
        _choiceManager.gameObject.SetActive(false);
        _endManager.gameObject.SetActive(false);

        StartCoroutine(StartScenarioWithDelay());
        
    }

    public float _delay;
    IEnumerator StartScenarioWithDelay()
    { 
        yield return new WaitForSeconds(_delay);

        _dialogueManager.gameObject.SetActive(true);
        _choiceManager.gameObject.SetActive(true);
        _endManager.gameObject.SetActive(false);
        LoadFirstScenario();
    }

    private void Update()
    {
        //Cheats
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScenarioExemple();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFirstScenario();
        }
        */
    }

    private void OnEnable()
    {
        _dialogueManager.OnNextSpeech.AddListener(EvaluateNextSpeech);
        _choiceManager.OnNextSpeech.AddListener(EvaluateNextSpeech);
    }
    private void OnDisable()
    {
        _dialogueManager.OnNextSpeech.RemoveAllListeners();
        _choiceManager.OnNextSpeech.RemoveAllListeners();
    }


   
    void EvaluateNextSpeech(string nextSpeechID)
    {
        switch (nextSpeechID[0])
        {
            case 'D':
                OpenDialogue(FindDialogueWithID(nextSpeechID));
                break;
            case 'P':
                OpenDialogue(FindDialogueWithID(nextSpeechID));
                break;
            case 'Q':
                OpenMCA(FindMCAWithID(nextSpeechID));
                break;

            case 'E':
                StartScenarioEnding();
                break;

            case 'X':
                FinishScenario();
                break;

            default:
                Debug.LogWarning("Incorrect 1st characterID : " + nextSpeechID[0]);
                break;
        }
    }


    void StartScenarioEnding()
    {
        _finalScore = _choiceManager.GetScore();
        
        if(_finalScore < 0)
        {
            OpenDialogue(FindDialogueWithID("E00"));
        }
        else
        {
            OpenDialogue(FindDialogueWithID("E01"));
        }
    }

    [SerializeField] EndPanelManager _endManager;
    void FinishScenario()
    {
        _endManager.SetUpEnding(_finalScore);
        _endManager.gameObject.SetActive(true);
        

        _dialogueManager.gameObject.SetActive(false);
        _choiceManager.gameObject.SetActive(false);
    }

    void LoadFirstScenario()
    {
        DialogLoader loader = new DialogLoader(_speakersList);
        
        _choicesList = loader._mca; 
        _dialoguesList = loader._dialogue;

        OpenDialogue(_dialoguesList[0]);
        //OpenMCA(_choicesList[0]);

        //OpenDialogue(FindDialogueWithID("D31"));

       
    }

    void OpenDialogue(Dialogue d)
    {
        _choiceManager.Mute();
        _dialogueManager.Initialize(d);
        _dialogueManager.StartNewDialogue();
    }

    void OpenMCA(MCA m)
    {
        _dialogueManager.MuteDialogue();
        _choiceManager.Initialize(m);
        _choiceManager.StartChoices();
    }


    Dialogue FindDialogueWithID(string idCheck)
    {
        foreach(Dialogue d in _dialoguesList)
        {
            if (d._id.Equals(idCheck)) return d;
        }
        return null;
    }
    MCA FindMCAWithID(string idCheck)
    {
        foreach (MCA m in _choicesList)
        {
            if (m._id.Equals(idCheck)) return m;
        }
        return null;
    }
    
    void ScenarioExemple()
    {
        _dialoguesList.Clear();
        _choicesList.Clear();

        List<Sentence> randomSpeech1 = new List<Sentence>()
        {
            new Sentence( 0, "hello! :)"),
            new Sentence( 0, "Je suis james, comment vas-tu?"),
            new Sentence ( 1, "Moi c'est cythia", Emotion.Angry),
            new Sentence (0, "quel joli nom!" )
        };

        Dialogue d1 = new Dialogue("D1", randomSpeech1, "C1", _speaker1Exemple, _speaker2Exemple);

        List<Sentence> randomSpeech2 = new List<Sentence>()
        {
            new Sentence( 0, "Je suis triste dans la vie"),
            new Sentence( 1, "Comment puis-je t'aider?", Emotion.Surprised),
            new Sentence ( 0, "J'aimerai un financier aux amandes :)", Emotion.Angry),
        };

        Dialogue d2 = new Dialogue("D2", randomSpeech2, "C1", _speaker2Exemple, _speaker1Exemple);


        _dialoguesList.Add(d1);
        _dialoguesList.Add(d2);

        List<Choice> randomChoices1 = new List<Choice>()
        {
            new Choice("Suddenly gave too much unexpected attention", 1, "D2"),
            new Choice("second", 2, "D1"),
        };

        MCA m1 = new MCA("C1", randomChoices1);
        _choicesList.Add(m1);


        OpenDialogue(d1);
    }
    
}
