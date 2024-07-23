using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class PopupManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject statScreen;
    public GameObject settingsScreen;
    public TMP_Text statText;
    public TMP_Text settingsText;

    [SerializeField]
    private InventoryScript inventory;

    public UnityEngine.UI.Button statsBtn;
    public UnityEngine.UI.Button settingsBtn;

    // Start is called before the first frame update
    void Start()
    {
        statScreen.SetActive(false);
        settingsScreen.SetActive(false);

        statsBtn.onClick.AddListener(TaskOnClickStats);
        settingsBtn.onClick.AddListener(TaskOnClickSettings);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void TaskOnClickStats() 
    {
        statScreen.SetActive(true);
        statText.text = "Knowledge " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.knowledge + "\nCharm " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.charm + "\nFinesse " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.finesse + "\nMuscle " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.muscle + "\nHeart " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.heart + "\nSense " + GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.sense;
    }

    void TaskOnClickSettings()
    {
        settingsScreen.SetActive(true);
        settingsText.text = "Are You Sure? No take backsies..";
    }

    public void resetGamePosition() 
    {
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.nodeid = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.knowledge = 5;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.charm = 5;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.finesse = 5;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.muscle = 5;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.heart = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().myPlayer.sense = 0;
        GameManager.playerObject.GetComponent<JSONWrite>().outputJSON();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.InitializeGame();
    }


}
