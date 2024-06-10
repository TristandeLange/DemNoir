using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.IO;
using System;
//using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEditor;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Linq;
using static Node;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Text.RegularExpressions;
using static UnityEditor.Progress;
//using static JSONWrite;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    public Node rootNode;
    public Node focusNode;
    public TMP_Text title;
    public TMP_Text question;

    [SerializeField]
    private TextAsset graphText;
    [SerializeField]
    private TextAsset saveDataText;

    public InventoryScript inventory;

    private ImageManager imageManagerScript;
    private QuestionManager textScrollerScript;
    private QuestionManager questionScript;

    public Scrollbar scrollbar;

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

    private string c0ID;
    private string c1ID;
    private string c2ID;
    private string c3ID;
    private string c4ID;

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
        Btn0.onClick.AddListener(TaskOnClick0); //connects the buttons to onclick event
        Btn1.onClick.AddListener(TaskOnClick1); 
        Btn2.onClick.AddListener(TaskOnClick2);
        Btn3.onClick.AddListener(TaskOnClick3);
        Btn4.onClick.AddListener(TaskOnClick4);

        imageManagerScript = GameObject.FindGameObjectWithTag("MainImage").GetComponent<ImageManager>();
        textScrollerScript = GameObject.FindGameObjectWithTag("TextScroller").GetComponent<QuestionManager>();
        questionScript = GameObject.FindGameObjectWithTag("QuestionTage").GetComponent<QuestionManager>();



        InitializeGame();
    }

    //separate from start so that it can be restarted more easily
    public void InitializeGame() 
    {
        imageManagerScript.toggleActive(false);
        textScrollerScript.toggleScrollerAnchorSize(true);
        //textScrollerContentScript.toggleScrollerAnchorSize(true);

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
        if (focusNode.ImageTitle != null)
        {
            imageManagerScript.toggleActive(true);
            textScrollerScript.toggleScrollerAnchorSize(false);
        }
    }

    void TaskOnClick0()
    {
        Node nextnode = SearchByID(c0.Id);

        if (nextnode.ImageTitle != null)
        {

            Sprite newimage = Resources.Load<Sprite>("Images/" + nextnode.ImageTitle);
            if (newimage != null)
            {
                imageManagerScript.toggleActive(true);
                textScrollerScript.toggleScrollerAnchorSize(false);
                imageManagerScript.changeImage(newimage);
            }
        }
        else
        {
            imageManagerScript.toggleActive(false);
            textScrollerScript.toggleScrollerAnchorSize(true);

        }


        if (Btn0.interactable != false || focusNode.C0[3] != null || focusNode.C0[3] != "")
        {

            StatUpdate(focusNode.C0[3]);
        }
        focusNode = nextnode;
        UpdateFromNode(nextnode);
        
    }
    void TaskOnClick1() 
    {
        Node nextnode = SearchByID(c1.Id);

        if (nextnode.ImageTitle != null)
        {
            
            Sprite newimage = Resources.Load<Sprite>("Images/" +nextnode.ImageTitle);
            if(newimage != null) 
            {
                imageManagerScript.toggleActive(true);
                textScrollerScript.toggleScrollerAnchorSize(false);
                imageManagerScript.changeImage(newimage);
            }
        }
        else
        {
            imageManagerScript.toggleActive(false);
            textScrollerScript.toggleScrollerAnchorSize(true);
        }

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

        if (nextnode.ImageTitle != null)
        {

            Sprite newimage = Resources.Load<Sprite>("Images/" + nextnode.ImageTitle);
            if (newimage != null)
            {
                imageManagerScript.toggleActive(true);
                textScrollerScript.toggleScrollerAnchorSize(false);
                imageManagerScript.changeImage(newimage);
            }
        }
        else
        {
            imageManagerScript.toggleActive(false);
            textScrollerScript.toggleScrollerAnchorSize(true);

        }

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

        if (nextnode.ImageTitle != null)
        {

            Sprite newimage = Resources.Load<Sprite>("Images/" + nextnode.ImageTitle);
            if (newimage != null)
            {
                imageManagerScript.toggleActive(true);
                textScrollerScript.toggleScrollerAnchorSize(false);
                imageManagerScript.changeImage(newimage);
            }
        }
        else
        {
            imageManagerScript.toggleActive(false);
            textScrollerScript.toggleScrollerAnchorSize(true);
        }

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

        if (nextnode.ImageTitle != null)
        {

            Sprite newimage = Resources.Load<Sprite>("Images/" + nextnode.ImageTitle);
            if (newimage != null)
            {
                imageManagerScript.toggleActive(true);
                textScrollerScript.toggleScrollerAnchorSize(false);
                imageManagerScript.changeImage(newimage);
            }
        }
        else
        {
            imageManagerScript.toggleActive(false);
            textScrollerScript.toggleScrollerAnchorSize(true);
        }

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

        CheckAndAdjustTextSize(question.text);

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
        Sprite closedBox = Resources.Load<Sprite>("Images/UIImages/DN_Choice_Box_Closed");
        Sprite openBox = Resources.Load<Sprite>("Images/UIImages/DN_Choice_Box_Open");
        Btn0.GetComponentInChildren<UnityEngine.UI.Image>().sprite = openBox;
        Btn1.GetComponentInChildren<UnityEngine.UI.Image>().sprite = openBox;
        Btn2.GetComponentInChildren<UnityEngine.UI.Image>().sprite = openBox;
        Btn3.GetComponentInChildren<UnityEngine.UI.Image>().sprite = openBox;
        Btn4.GetComponentInChildren<UnityEngine.UI.Image>().sprite = openBox;

        focus = parentNode;
        if (focus == null || focus.C0 == null || focus.C0[0] == null || !StatRestrictCheck(focus.C0[2]))
        {
            Btn0.interactable = false;
            Btn0.GetComponent<UnityEngine.UI.Image>().sprite = closedBox;
            c0s.text = " ";
        }
        if (focus == null || focus.C1 == null || focus.C1[0] == null || !StatRestrictCheck(focus.C1[2])) 
        {   
            Btn1.interactable = false;
            Btn1.GetComponent<UnityEngine.UI.Image>().sprite = closedBox;
            c1s.text = " ";
        }
        if (focus == null || focus.C2 == null || focus.C2[0] == null || !StatRestrictCheck(focus.C2[2]))
        {
            Btn2.interactable = false;
            Btn2.GetComponent<UnityEngine.UI.Image>().sprite = closedBox;
            c2s.text = " ";
        }
        if (focus == null || focus.C3 == null || focus.C3[0] == null || !StatRestrictCheck(focus.C3[2]))
        {
            Btn3.interactable = false;
            Btn3.GetComponent<UnityEngine.UI.Image>().sprite = closedBox;
            c3s.text = " ";
        }
        if (focus == null || focus.C4 == null || focus.C4[0] == null || !StatRestrictCheck(focus.C4[2]))
        {
            Btn4.interactable = false;
            Btn4.GetComponent<UnityEngine.UI.Image>().sprite = closedBox;
            c4s.text = " ";
        }

    }

    //searches for a particular node based off it's ID or returns null if not found
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

    //checks if the given string says "Null" or doesn't have any contents
    public string StringNullCheck(string text)
    {
        if (text == "Null" || text == "" || text == "null" || text == "Null ")
        {
            return null;
        }
        return text;
    }

    //updates the players stat based off the current node
    public void StatUpdate(string text)
    {
        if (text == null) { return; }
        string[] words = text.Split(' ');
        Int32.TryParse(words[2], out int value);

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
                else if (words[1] == "e")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.mind = value;
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
                else if (words[1] == "e")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.heart = value;
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
                else if (words[1] == "e")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness = value;
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
                else if (words[1] == "e")
                {
                    playerObject.GetComponent<JSONWrite>().myPlayer.strength = value;
                }

                break;
            case "Item":

                adjustInv(words);

                //This is just to make the fact that item data inputs are 4 strings long not actually matter
                string[] temp = new string[words.Length - 1];
                Array.Copy(words, words.GetLowerBound(0) + 1, temp, temp.GetLowerBound(0), words.Length - 1);
                words = temp;
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

        return;

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
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.mind >= value)
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
                else if (words[1] == "=")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.mind == value)
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
                else if (words[1] == "=")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.heart == value)
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
                else if (words[1] == "=")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.sneakiness == value)
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
                else if (words[1] == "=")
                {
                    if (playerObject.GetComponent<JSONWrite>().myPlayer.strength == value)
                    {
                        return true;
                    }
                }

                break;

            case "Item":

                if (itemHeld(words[1]) >= value) 
                {
                    return true;
                }

                break;
            default:
                Debug.Log("incorrect stat option : " + words[0]);
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

    public int itemHeld(string itemID) 
    {
        List<GameObject> items = inventory.items;
        foreach(GameObject item in items) 
        {
            if (item.GetComponent<ItemScript>().getId() == Convert.ToInt32(itemID)) 
            {
                return item.GetComponent<ItemScript>().getCount();
            }
        }

        return 0;
    }

    public void adjustInv(string[] itemData) 
    {
        List<GameObject> items = inventory.items;
        foreach (GameObject item in items) 
        {
            if(item.GetComponent<ItemScript>().getId() == Convert.ToInt32(itemData[1])) 
            {   //If the player already has the item in their inventory

                if (itemData[2] == "+")
                {
                    item.GetComponent<ItemScript>().setCount(item.GetComponent<ItemScript>().getCount() + Convert.ToInt32(itemData[3]));
                }
                else
                if (itemData[2] == "-")
                {
                    
                    item.GetComponent<ItemScript>().setCount(item.GetComponent<ItemScript>().getCount() - Convert.ToInt32(itemData[3]));
                    
                }
                return;
            }
        }
        //The object doesn't exist in the inventory so either add it, or do nothing because I'm not having negative items
        if (itemData[2] == "+") 
        {
            GameObject newitem = Resources.Load<GameObject>("ItemDefaults/Item" + itemData[1]);
            newitem.GetComponent<ItemScript>().setCount(itemData[3]);
            inventory.items.Add(newitem);
        }
        
        return;
    }



    public void CheckAndAdjustTextSize(string input)
    {
        if (input == null)
        { return; }
        if (input.Length > 900)
        {
            questionScript.adjustTextboxHeight(3);
            scrollbar.transform.localScale = new Vector2(1, 1);
        }
        else
        if (input.Length > 700)
        {
            if (focusNode.ImageTitle == null)
            {
                questionScript.adjustTextboxHeight(2);
            }
            else
            {
                questionScript.adjustTextboxHeight(3);
            }
            scrollbar.transform.localScale = new Vector2(1, 1);
        }
        else
        if (input.Length > 500 || focusNode.ImageTitle != null)
        {
            questionScript.adjustTextboxHeight(1);
            scrollbar.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            questionScript.adjustTextboxHeight(0);
            scrollbar.transform.localScale = new Vector2(0, 0);
        }
    }

    //Assembles the tree of choices based off GraphData
    private void AssembleGraph()
    {
        Node focus;
        Node temp;
        string line;
        string data = graphText.text.Trim();
        string[] dataSet = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < dataSet.Length; i++)
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

        /*string filepath = "Assets/Resources/GraphData.txt"; //this works!!!!!!
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
                    
                    line = sr.ReadLine();   //reads question
                    line = line.Trim();
                    focus.Question = StringNullCheck(line);

                    line = sr.ReadLine();   //reads title
                    line = line.Trim();
                    focus.Title = StringNullCheck(line);

                    line = sr.ReadLine();   //reads imagetitle
                    line = line.Trim();
                    focus.ImageTitle = StringNullCheck(line);

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
        }*/


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
        
        string json = saveDataText.text;
        SaveData listdata = JsonConvert.DeserializeObject<SaveData>(json);
        return listdata;
        
    }

}
