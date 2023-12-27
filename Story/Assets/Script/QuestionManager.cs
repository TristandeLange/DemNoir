using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{  

    public void adjustTextboxHeight(int val) 
    {
        switch (val) 
        { 
        
            case 0:
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                gameObject.GetComponent<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Middle;
                break;

            case 1:
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, -0.2f);
                gameObject.GetComponent<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Top;
                break;

            case 2:
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, -0.6f);
                gameObject.GetComponent<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Top;
                break;

            case 3:
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, -1f);
                gameObject.GetComponent<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Top;
                break;

            default:

                break;
        
        }
        
    }


    public void toggleScrollerAnchorSize(bool b)
    {
        if (b)
        {
            gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.74f);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.74f);
        }
    }


}
