using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    
    //change the image with whatever sprite is supplied
    public void changeImage(Sprite newSprite) 
    {
        
        GetComponent<Image>().sprite = newSprite;
    }

    //if b is true, set the gameobject to active, else set it to inactive
    public void toggleActive(bool b)
    {
        GetComponent<Image>().gameObject.SetActive(b);
    }


}
