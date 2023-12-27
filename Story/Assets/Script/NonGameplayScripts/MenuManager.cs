using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsScreen;

    public UnityEngine.UI.Button StartBtn;
    public UnityEngine.UI.Button SettingsBtn;
    public UnityEngine.UI.Button CreditsBtn;

    // Start is called before the first frame update
    void Start()
    {
        settingsScreen.SetActive(false);
        StartBtn.onClick.AddListener(TaskOnClickStartBtn);
        CreditsBtn.onClick.AddListener(TaskOnClickCreditBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClickStartBtn() 
    {
        SceneManager.LoadScene("Main Scene Image", LoadSceneMode.Single);
    }

    void TaskOnClickCreditBtn()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

}
