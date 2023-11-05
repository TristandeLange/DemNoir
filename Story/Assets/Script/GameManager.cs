using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.IO;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEditor;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Linq;
using static Node;
using Unity.VisualScripting;
//using static JSONWrite;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    public Node rootNode;
    public Node focusNode;
    public TMP_Text title;
    public TMP_Text question;
    public TMP_Text c0s;
    public TMP_Text c1s;
    public TMP_Text c2s;
    public TMP_Text c3s;
    public TMP_Text c4s;
    private Node c0;
    private Node c1;
    private Node c2;
    private Node c3;
    private Node c4;

    public string c0ID;
    public string c1ID;
    public string c2ID;
    public string c3ID;
    public string c4ID;

    public UnityEngine.UI.Button Btn0;
    public UnityEngine.UI.Button Btn1;
    public UnityEngine.UI.Button Btn2;
    public UnityEngine.UI.Button Btn3;
    public UnityEngine.UI.Button Btn4;

    //public Queue UnpoppedNodes; Not actually useful yet.
    //public TextAsset graphData;

    // Start is called before the first frame update
    public void Start()
    {
        //UnityEngine.UI.Button btn1 = Btn1.GetComponent<UnityEngine.UI.Button>(); This seems unnecessary
        //UnityEngine.UI.Button btn2 = Btn2.GetComponent<UnityEngine.UI.Button>();
        //UnityEngine.UI.Button btn3 = Btn3.GetComponent<UnityEngine.UI.Button>();
        //UnityEngine.UI.Button btn4 = Btn4.GetComponent<UnityEngine.UI.Button>();
        Btn0.onClick.AddListener(TaskOnClick0);
        Btn1.onClick.AddListener(TaskOnClick1); //connects the buttons to onclick event
        Btn2.onClick.AddListener(TaskOnClick2);
        Btn3.onClick.AddListener(TaskOnClick3);
        Btn4.onClick.AddListener(TaskOnClick4);

        InitializeGame();
    }

    public void InitializeGame() 
    {
        rootNode = new Node(0);
        focusNode = rootNode;
        AssembleGraph();
        SaveData loadedData = LoadJSON();
        playerObject.GetComponent<JSONWrite>().myPlayer.mind = loadedData.mind;
        playerObject.GetComponent<JSONWrite>().myPlayer.heart = loadedData.heart;
        playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness = loadedData.sneakiness;
        playerObject.GetComponent<JSONWrite>().myPlayer.strength = loadedData.strength;
        focusNode = SearchByID(loadedData.nodeid);
        UpdateFromNode(focusNode);
    }

    void TaskOnClick0()
    {
        //PrintNode(focusNode);
        //Debug.Log(focusNode.Defnext.Id);
        Node nextnode = SearchByID(c0.Id);
        if (Btn0.interactable != false || focusNode.C0[3] != null || focusNode.C0[3] != "")
        {

            StatUpdate(focusNode.C0[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
        
    }
    void TaskOnClick1() 
    {
        Debug.Log(c1.Id);
        Node nextnode = SearchByID(c1.Id);
        if (Btn1.interactable != false || focusNode.C1[3] != "Null" || focusNode.C1[3] != "")
        {
            StatUpdate(focusNode.C1[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
    }
    void TaskOnClick2() 
    {
        Node nextnode = SearchByID(c2.Id);
        if (Btn2.interactable != false || focusNode.C2[3] != null || focusNode.C2[3] != "")
        {
            StatUpdate(focusNode.C2[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
    }
    void TaskOnClick3() 
    {
        Node nextnode = SearchByID(c3.Id);
        if (Btn3.interactable != false || focusNode.C3[3] != null || focusNode.C3[3] != "")
        {
            StatUpdate(focusNode.C3[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
    }
    void TaskOnClick4() 
    {
        Node nextnode = SearchByID(c4.Id);
        if (Btn4.interactable != false || focusNode.C4[3] != null || focusNode.C4[3] != "")
        {
            StatUpdate(focusNode.C4[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
    }

    //updates the interface with whatever Node we're going to based off the previous choice made.
    //checks the validity of each choice node as well. 
    private void UpdateFromNode(Node node) 
    {
        title.text = node.Title;
        question.text = node.Question;

        if (node.C0 != null)
        {
            c0ID = node.C0[1];
            c0s.text = node.C0[0];
            Int32.TryParse(node.C0[1], out int id);
            c0 = SearchByID(id);

        }
        if (node.C1 != null)
        {
            c1ID = node.C1[1];
            c1s.text = node.C1[0];
            Int32.TryParse(node.C1[1], out int id);
            c1 = SearchByID(id);
        }
        if (node.C2 != null)
        {
            c2ID = node.C2[1];
            c2s.text = node.C2[0];
            Int32.TryParse(node.C2[1], out int id);
            c2 = SearchByID(id);
        }
        if (node.C3 != null)
        {
            c3ID = node.C3[1];
            c3s.text = node.C3[0];
            Int32.TryParse(node.C3[1], out int id);
            c3 = SearchByID(id);
        }
        if (node.C4 != null)
        {
            c4ID = node.C4[1];
            c4s.text = node.C4[0];
            Int32.TryParse(node.C4[1], out int id);
            c4 = SearchByID(id);
        }
        Btn0.interactable = true;
        Btn1.interactable = true;
        Btn2.interactable = true;
        Btn3.interactable = true;
        Btn4.interactable = true;

        ChildValidityCheck(node);

        playerObject.GetComponent<JSONWrite>().myPlayer.nodeid = node.Id;
        playerObject.GetComponent<JSONWrite>().outputJSON();    

    }

    //This is to check both if the option is null, and if you don't meet any possible stat 
    //requirements. If either are true, then disable the button and change the color slightly.
    void ChildValidityCheck(Node parentNode) 
    {
        Node focus;

        focus = parentNode;
        if (focus == null || focus.C0 == null || focus.C0[0] == null || !StatRestrictCheck(focus.C0[2]))
        {
            Btn0.interactable = false;
            c0s.text = " ";
        }
        if (focus == null || focus.C1 == null || focus.C1[0] == null || !StatRestrictCheck(focus.C1[2])) 
        {   
            Btn1.interactable = false;
            c1s.text = " ";
        }
        if (focus == null || focus.C2 == null || focus.C2[0] == null || !StatRestrictCheck(focus.C2[2]))
        {
            Btn2.interactable = false;
            c2s.text = " ";
        }
        if (focus == null || focus.C3 == null || focus.C3[0] == null || !StatRestrictCheck(focus.C3[2]))
        {
            Btn3.interactable = false;
            c3s.text = " ";
        }
        if (focus == null || focus.C4 == null || focus.C4[0] == null || !StatRestrictCheck(focus.C4[2]))
        {
            Btn4.interactable = false;
            c4s.text = " ";
        }

    }

    //Assembles the tree of choices based off GraphData
    private void AssembleGraph()
    {
        
        Node focus;
        Node temp;
        string line;
        //c1s.text = "it gets to this at all"; - this worked so it does get here.
        string filepath = "Assets/Resources/GraphData.txt"; //this works!!!!!!
        try
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                //int i = 0;
                while ((line = sr.ReadLine()) != "endgame" || line == null)
                {
                    
                    line = line.Trim();
                    Int32.TryParse(line, out int id); //sets id from file
                    if ((focus = SearchByID(id)) == null) //checks if the node already exists and if it does, sets it, or makes it a new node.
                    {
                        focus = new Node(id);
                    }

                    // Obsolete Version
                    //line = sr.ReadLine();   //reads statRestrict
                    //line = line.Trim();
                    //focus.StatRestrict = StringNullCheck(line);
                    //line = sr.ReadLine();   //reads statChange
                    //line = line.Trim();
                    //focus.StatChange = StringNullCheck(line);
                    
                    line = sr.ReadLine();   //reads question
                    line = line.Trim();
                    focus.Question = StringNullCheck(line);

                    line = sr.ReadLine();   //reads title
                    line = line.Trim();
                    focus.Title = StringNullCheck(line);

                    line = sr.ReadLine();   //reads C0s
                    line = line.Trim();
                    focus.C0[0] = StringNullCheck(line);

                    line = sr.ReadLine();   //reads C1s
                    line = line.Trim();
                    focus.C1[0] = StringNullCheck(line);

                    line = sr.ReadLine();   //reads C2s
                    line = line.Trim();
                    focus.C2[0] = StringNullCheck(line);

                    line = sr.ReadLine();   //reads C3s
                    line = line.Trim();
                    focus.C3[0] = StringNullCheck(line);

                    line = sr.ReadLine();   //reads C4s
                    line = line.Trim();
                    focus.C4[0] = StringNullCheck(line);

                    //works properly up to this point, no stall or crash
                    
                    //checks and sets prev
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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

                    //old stuff
                    /**
                    //checks and sets c0
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)    //determine if there is no c0 defined
                    {
                        Int32.TryParse(line, out id);   //change string to int
                        if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                        {
                            focus.C0[1] = temp;
                        }
                        else
                        {
                            focus.C0[ = new Node(id);
                        }
                    }
                    else
                    {
                        focus.C0 = null;  //there is no c0 so set it to null
                    }
                    //checks and sets C0 Stat Change 
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    { 
                        focus.C0.statChanges = line; 
                    } 
                    else 
                    { 
                        focus.C0.statChanges = null; 
                    }

                    Debug.Log(line + " Fuck MEEEEEEEE");
                    //checks and sets C0 Stat Restriction
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        Debug.Log(line + "hellonotnull2");
                        focus.C0.statRestrictions = line;
                    }
                    else 
                    {
                        Debug.Log(line + "hellonull2");
                        focus.C0.statRestrictions = null; 
                    }
                    
                    
                    
                    //checks and sets c1
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)    //determine if there is no c1 defined
                    {
                        Int32.TryParse(line, out id);   //change string to int
                        if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                        {
                            focus.C1.node = temp;
                        }
                        else
                        {
                            focus.C1.node = new Node(id);
                        }
                    }
                    else
                    {
                        focus.C1 = null;  //there is no c1 so set it to null
                    }
                    //checks and sets C1 Stat Change 
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C1.statChanges = line;
                    }
                    else 
                    { 
                        focus.C1.statChanges = null; 
                    }
                    //checks and sets C1 Stat Restriction
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C1.statRestrictions = line;
                    }
                    else 
                    { 
                        focus.C1.statRestrictions = null; 
                    }
                    //checks and sets c2
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)    //determine if there is no c2 defined
                    {
                        Int32.TryParse(line, out id);   //change string to int
                        if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                        {
                            focus.C2.node = temp;
                        }
                        else
                        {
                            focus.C2.node = new Node(id);
                        }
                    }
                    else
                    {
                        focus.C2 = null;  //there is no c2 so set it to null
                    }
                    //checks and sets C2 Stat Change 
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C2.statChanges = line;
                    }
                    else 
                    { 
                        focus.C2.statChanges = null; 
                    }
                    //checks and sets C2 Stat Restriction
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C2.statRestrictions = line;
                    }
                    else 
                    { 
                        focus.C2.statRestrictions = null; 
                    }
                    //checks and sets c3
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)    //determine if there is no c3 defined
                    {
                        Int32.TryParse(line, out id);   //change string to int
                        if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                        {
                            focus.C3.node = temp;
                        }
                        else
                        {
                            focus.C3.node = new Node(id);
                        }
                    }
                    else
                    {
                        focus.C3 = null;  //there is no c3 so set it to null
                    }
                    //checks and sets C3 Stat Change 
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C3.statChanges = line;
                    }
                    else 
                    { 
                        focus.C3.statChanges = null; 
                    }
                    
                    //checks and sets C3 Stat Restriction
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C3.statRestrictions = line;
                    }
                    else 
                    { 
                        focus.C3.statRestrictions = null; 
                    }
                    
                    //checks and sets c4
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)    //determine if there is no c4 defined
                    {
                        Int32.TryParse(line, out id);   //change string to int
                        if ((temp = SearchByID(id)) != null)    //if node with id exists, set temp to it
                        {
                            focus.C4.node = temp;
                        }
                        else
                        {
                            focus.C4.node = new Node(id);
                        }
                    }
                    else
                    {
                        focus.C4 = null;  //there is no c4 so set it to null
                    }
                    
                    //checks and sets C4 Stat Change 
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C4.statChanges = line;
                    }
                    else 
                    { 
                        focus.C4.statChanges = null; 
                    }
                    
                    //checks and sets C4 Stat Restriction
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (StringNullCheck(line) != null)
                    {
                        focus.C4.statRestrictions = line;
                    }
                    else 
                    { 
                        focus.C4.statRestrictions = null; 
                    }
                    **/

                    //checks and sets c0 node id -----------------------------------------------------------------------------------------------------
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                    line = sr.ReadLine();
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
                sr.Close();
            }
            
        }
        catch (Exception e)
        {
            //so far it get here but no message is shown in the console in unity for some reason
            //c3s.text = "it does not read the file!";
            Console.WriteLine("The File could not be read:");
            Console.WriteLine(e.Message);
        }
        
        
    }

    //searches for a particular node based off it's ID or returns null if not found
    public Node SearchByID(int id) 
    {
        Node focus = rootNode;
        do
        {
            if(focus.Id == id) 
            {
                return focus;
            }
            focus = focus.Defnext;
        } while (focus != null);

        //Debug.Log("Its returning Null");
        return null;
    }

    //checks if the given string says "Null" or doesn't have any contents
    public string StringNullCheck(string text) 
    {
        if(text == "Null" || text == "" || text == "null" || text == "Null ") 
        {
            return null;
        }
        return text;
    }

    //updates the players stat based off the current node
    public void StatUpdate(string text) 
    {
        if(text == null) {return;}
        string[] words = text.Split(' ');
        Int32.TryParse(words[2],out int value);
        switch (words[0]) 
        {

            case "Mind":
                if (words[1] == "+") 
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.mind += value;
                }
                else if (words[1] == "-") 
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.mind -= value;
                }

                break;
            case "Heart":
                if (words[1] == "+")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.heart += value;
                }
                else if (words[1] == "-")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.heart -= value;
                }
                break;
            case "Sneakiness":
                if (words[1] == "+")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness += value;
                }
                else if (words[1] == "-")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness -= value;
                }
                break;
            case "Strength":
                if (words[1] == "+")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.strength += value;

                }
                else if (words[1] == "-")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.strength -= value;
                }
                break;
            default:
                Debug.Log("incorrect stat option");
                break;

        }
        
        if (words.Length > 3) 
        {
            string[] ele = new string[words.Length - 3];
            Array.Copy(words, words.GetLowerBound(0) + 3, ele, ele.GetLowerBound(0), words.Length - 3);

            if (ele[0] != null)
            {
                //Debug.Log("This happens");
                text = string.Join(" ", ele);
                StatUpdate(text);
            }
        }
    }
    
    //Checks if the player has the proper stats, either >= or < the requested stat restriction
    //For the moment this operates under the assumption that there can only be one stat restriction per option at a time. Not great. Should change later.
    public bool StatRestrictCheck(string text) 
    {
        if (text == null) 
        {
            return true;
        }
        string[] words = text.Split(' ');
        Int32.TryParse(words[2], out int value);
        switch (words[0])
        {

            case "Mind":
                if (words[1] == "+")
                {
                    if(playerObject.GetComponent<JSONWrite>().myPlayer.mind >= value) 
                    {
                        return true;
                    }
                }
                else if (words[1] == "-")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.mind < value)
                    {
                        return true;
                    }
                }

                break;
            case "Heart":
                if (words[1] == "+")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.heart >= value)
                    {
                        return true;
                    }
                }
                else if (words[1] == "-")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.heart < value)
                    {
                        return true;
                    }
                }
                break;
            case "Sneakiness":
                if (words[1] == "+")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness >= value)
                    {
                        return true;
                    }
                }
                else if (words[1] == "-")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness < value)
                    {
                        return true;
                    }
                }
                break;
            case "Strength":
                if (words[1] == "+")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.strength >= value)
                    {
                        return true;
                    }
                }
                else if (words[1] == "-")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.strength < value)
                    {
                        return true;
                    }
                }
                break;
            default:
                Debug.Log("incorrect stat option");
                break;

        }

        if (words.Length > 3)
        {
            string[] ele = new string[words.Length - 3];
            Array.Copy(words, words.GetLowerBound(0) + 3, ele, ele.GetLowerBound(0), words.Length - 3);

            if (ele[0] != null)
            {
                //Debug.Log("This happens");
                text = string.Join(" ", ele);
                StatRestrictCheck(text);
            }
        }

        return false;
    }

    public class SaveData 
    {
        public int nodeid;
        public int mind;
        public int heart;
        public int sneakiness;
        public int strength;
    }

    public SaveData LoadJSON() 
    {
        using (StreamReader r = new StreamReader("Assets/Resources/text.json")) 
        {
            string json = r.ReadToEnd();
            SaveData listdata = JsonConvert.DeserializeObject<SaveData>(json);
            //Debug.Log(listdata.nodeid);
            return listdata;
        }
    }

}
