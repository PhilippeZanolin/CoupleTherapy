using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomAnswerButton : Button
{
    float _fillSpeed = 0.1f;
    float _currentFillAmount = 0f;


    AudioSource _fillSoundSource;

    protected override void Awake()
    {
        base.Awake();
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Horizontal;
        image.fillAmount = 0;
        _currentFillAmount = 0;

        _fillSoundSource = this.GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        image.fillAmount = _currentFillAmount = 0;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        FillButton(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        FillButton(false);
    }

    
   void FillButton(bool fill)
   {

        StopAllCoroutines();
        if (fill)
        {
            StartCoroutine(FillButtonAnimation());
        }
        else
        {
            StartCoroutine(UnfillButtonAnimation());
        }
   }

    IEnumerator FillButtonAnimation()
    {
        _fillSoundSource.Play();
        
        while(_currentFillAmount != 1)
        {
            _currentFillAmount += _fillSpeed;
            if (_currentFillAmount > 1) _currentFillAmount = 1;

            image.fillAmount = _currentFillAmount;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    IEnumerator UnfillButtonAnimation()
    {
        _fillSoundSource.Stop();
        
        while (_currentFillAmount != 0)
        {
            _currentFillAmount -= _fillSpeed;
            if (_currentFillAmount < 0) _currentFillAmount = 0;

            image.fillAmount = _currentFillAmount;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
