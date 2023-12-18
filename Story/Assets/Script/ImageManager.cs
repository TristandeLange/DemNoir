using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
