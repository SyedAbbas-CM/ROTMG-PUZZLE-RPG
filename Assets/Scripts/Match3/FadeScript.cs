using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CanvasGroup UIGroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;



    public void ShowUI()
    {
        fadeIn = true;
    }
    public void HideUI()
    {
        fadeOut = true;
    }
    private void Update()
    {
        if (fadeIn)
        {
            if (UIGroup.alpha < 1)
            {
                UIGroup.alpha += Time.deltaTime;
                if(UIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }


        if (fadeOut)
        {
            if (UIGroup.alpha >= 1)
            {
                UIGroup.alpha -= Time.deltaTime;
                if (UIGroup.alpha <= 0 )
                {
                    fadeOut = false;
                }
            }
        }
    }



}
