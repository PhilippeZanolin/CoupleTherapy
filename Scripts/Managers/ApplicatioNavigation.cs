using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicatioNavigation
{
    public void Start()
    {
        
    }
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
