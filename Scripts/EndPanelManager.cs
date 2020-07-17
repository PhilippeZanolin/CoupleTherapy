using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndPanelManager : MonoBehaviour
{

    [SerializeField] GameObject _happyImage;
    [SerializeField] GameObject _brokenImage;
    [SerializeField] TextMeshProUGUI _scoreText;

    [SerializeField] string _mainMenuSceneName;



    public void SetUpEnding(int score)
    {
        bool win = (score < 0) ? false : true;
        _happyImage.SetActive(win);
        _brokenImage.SetActive(!win);

        _scoreText.text = "Your Score : " + score + "!";

        Debug.Log("set up end");
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void QuitGame()
    {
        GameLifeManager.QuitApp();
    }
}
