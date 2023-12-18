using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if given True, enlarge anchors, if set false, shrink them
    public void toggleAnchorSize(bool b)
    {
        if(b) 
        {
            gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1,0.74f); 
        }
        else 
        {
            gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.74f);
        }
    }

}
