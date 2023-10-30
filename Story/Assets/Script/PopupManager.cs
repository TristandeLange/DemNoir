using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static GameManager;

public class PopupManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject statScreen;
    public GameObject settingsScreen;
    public TMP_Text statText;
    public TMP_Text settingsText;


    // Start is called before the first frame update
    void Start()
    {
        statScreen.SetActive(false);
        settingsScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        statText.text = "Knowledge: " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.mind + "\nCharm: " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.heart + "\nFinesse: " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness + "\nMuscle: " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.strength;

        settingsText.text = "Are You Sure? No take backsies..";
    }
    public void resetGamePosition() 
    {
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.nodeid = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.mind = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.heart = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.strength = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().outputJSON();

        GameManager.Start();
    }


}