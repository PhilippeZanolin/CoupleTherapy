using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knocking : MonoBehaviour
{
    [SerializeField] int knockCount = 1;
    int knockleft;
    public GameObject menuSelect;
    public AudioClip soundfx;
    public AudioSource audioSource;


    //public Text doorText;
    /*
    void RefreshDoorText()
    {
        doorText.text = "(" + knockCount + ")";
        if ((knockCount == 0)
        {
            doorText.gameObject.SetActive(false);
        }
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        knockleft = knockCount;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

   

    public void Knock() 
    {
       
        if (knockleft > 0 && !audioSource.isPlaying)
        {
            knockleft--;
            audioSource.Play();
        }
       
        if(knockleft == 0 && !menuSelect.activeSelf)
        {
            StartCoroutine(WaitForKnockFinished());
        }

        //RefreshDoorText();
    }




    IEnumerator WaitForKnockFinished()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        menuSelect.SetActive(true);
    }


}
