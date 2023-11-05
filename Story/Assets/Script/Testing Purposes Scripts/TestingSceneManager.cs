using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static LinkedListNode;

public class TestingSceneManager : MonoBehaviour
{

    public TMP_Text BtnText;
    public UnityEngine.UI.Button Btn;
    public LinkedListNode focusNode;

    // Start is called before the first frame update
    void Start()
    {
        AssembleGraph();
        Btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        BtnText.text = focusNode.data + " " + focusNode.id;
        
        focusNode = focusNode.next;
        
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AssembleGraph() 
    { 
        string line;
        LinkedListNode head = new LinkedListNode(0, "starty", null);
        LinkedListNode focus = head;

        int i = 0;
        string filepath = "Assets/Resources/GraphData.txt"; //this works!!!!!!
        try
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                
                while ((line = sr.ReadLine()) != "endgame" || line == null)
                {
                    line = line.Trim();

                    focus.id = i;
                    focus.data = line;
                    focus.next = new LinkedListNode();
                    focus = focus.next;
                    
                    i++;
                }
                sr.Close();
            }
            focusNode = head;

        }
        catch (Exception e)
        {
            //so far it get here but no message is shown in the console in unity for some reason
            //c3s.text = "it does not read the file!";
            Console.WriteLine("The File could not be read:");
            Console.WriteLine(e.Message);
        }


    }
}

