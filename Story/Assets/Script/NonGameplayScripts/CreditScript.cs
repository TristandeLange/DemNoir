using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{

    public UnityEngine.UI.Button ExitBtn;


    

    // Start is called before the first frame update
    void Start()
    {
        ExitBtn.onClick.AddListener(TaskOnClickExitBtn);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClickExitBtn()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

}
