using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static LinkedListNode;

public class TestingSceneManager : MonoBehaviour
{

    public TMP_Text BtnText;
    public UnityEngine.UI.Button Btn;
    public Node rootNode;
    public Node focusNode;

    [SerializeField]
    private TextAsset graphText;

    // Start is called before the first frame update
    void Start()
    {
        //AssembleGraph();
        rootNode = new Node(0);
        focusNode = rootNode;
        AssembleGraph(graphText);
        Btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        BtnText.text = focusNode.Title + " : " + focusNode.Id;
        
        focusNode = focusNode.Defnext;
        
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string StringNullCheck(string text)
    {
        if (text == "Null" || text == "" || text == "null" || text == "Null ")
        {
            return null;
        }
        return text;
    }

    public Node SearchByID(int id)
    {
        Node focus = rootNode;
        do
        {
            if (focus.Id == id)
            {
                return focus;
            }
            focus = focus.Defnext;
        } while (focus != null);

        return null;
    }


    /*private void AssembleGraph() 
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
            Debug.Log("The File could not be read:");
        }


    }
*/
    public void AssembleGraph(TextAsset textFile)
    {
        Node focus;
        Node temp;
        string line;
        string data = textFile.text.Trim();
        string[] dataSet = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //int i = 0;

        for (int i = 0; i < 105; i++)
        {
            Int32.TryParse(dataSet[i], out int id); //sets id from file
            if ((focus = SearchByID(id)) == null) //checks if the node already exists and if it does, sets it, or makes it a new node.
            {
                Debug.Log("new node is made of id: " + id);
                focus = new Node(id);
            }
            i++;
            line = dataSet[i];   //reads question
            line = line.Trim();
            focus.Question = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads title
            line = line.Trim();
            focus.Title = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads imagetitle
            line = line.Trim();
            focus.ImageTitle = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads C0s
            line = line.Trim();
            focus.C0[0] = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads C1s
            line = line.Trim();
            focus.C1[0] = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads C2s
            line = line.Trim();
            focus.C2[0] = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads C3s
            line = line.Trim();
            focus.C3[0] = StringNullCheck(line);

            i++;
            line = dataSet[i];   //reads C4s
            line = line.Trim();
            focus.C4[0] = StringNullCheck(line);

            //works properly up to this point, no stall or crash

            //checks and sets prev
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)    //determine if there is no prev defined
            {
                Int32.TryParse(line, out id);   //change string to int
                if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                {
                    focus.Prev = temp;
                }
                else
                {
                    focus.Prev = new Node(id);  //this whole section might be unnecessary if the prev always exists
                }
            }
            else
            {
                focus.Prev = null;  //there is no prev so set it to null
            }
            //checks and sets default next
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)    //determine if there is no defnext defined
            {
                Int32.TryParse(line, out id);   //change string to int
                if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                {
                    focus.Defnext = temp;
                }
                else
                {
                    focus.Defnext = new Node(id);   //adds a new node to the tree (through defnext)
                }
            }
            else
            {
                focus.Defnext = null;  //there is no defnext so set it to null
            }

            //checks and sets c0 node id -----------------------------------------------------------------------------------------------------
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C0[1] = line;

            }
            else
            {
                focus.C0[1] = null;
            }
            //checks and sets c0 stat changes
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C0[3] = line;
            }
            else
            {
                focus.C0[3] = null;
            }
            //checks and sets c0 stat restrictioms
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C0[2] = line;
            }
            else
            {
                focus.C0[2] = null;
            }

            //checks and sets c1 node id
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C1[1] = line;
            }
            else
            {
                focus.C1[1] = null;
            }
            //checks and sets c1 stat changes
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C1[3] = line;
            }
            else
            {
                focus.C1[3] = null;
            }
            //checks and sets c1 stat restrictioms
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C1[2] = line;
            }
            else
            {
                focus.C1[2] = null;
            }

            //checks and sets c2 node id
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C2[1] = line;
            }
            else
            {
                focus.C2[1] = null;
            }
            //checks and sets c2 stat changes
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C2[3] = line;
            }
            else
            {
                focus.C2[3] = null;
            }
            //checks and sets c2 stat restrictioms
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C2[2] = line;
            }
            else
            {
                focus.C2[2] = null;
            }

            //checks and sets c3 node id
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C3[1] = line;
            }
            else
            {
                focus.C3[1] = null;
            }
            //checks and sets c3 stat changes
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C3[3] = line;
            }
            else
            {
                focus.C3[3] = null;
            }
            //checks and sets c3 stat restrictioms
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C3[2] = line;
            }
            else
            {
                focus.C3[2] = null;
            }

            //checks and sets c4 node id
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C4[1] = line;
            }
            else
            {
                focus.C4[1] = null;
            }
            //checks and sets c4 stat changes
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C4[3] = line;
            }
            else
            {
                focus.C4[3] = null;
            }
            //checks and sets c4 stat restrictioms
            i++;
            line = dataSet[i];
            line = line.Trim();
            if (StringNullCheck(line) != null)
            {
                focus.C4[2] = line;
            }
            else
            {
                focus.C4[2] = null;
            }

        }

    }

}

