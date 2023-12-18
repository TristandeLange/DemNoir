using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Node
{

    public int Id { get; set; }
    public string Question { get; set; }
    public string Title { get;  set; }
    public string ImageTitle { get; set; }
    public string[] C0 { get; set; }
    public string[] C1 { get; set; }
    public string[] C2 { get; set; }
    public string[] C3 { get; set; }
    public string[] C4 { get; set; }
    /** Previous Version
    public string C0s { get; set; }
    public string C1s { get; set; }
    public string C2s { get; set; }
    public string C3s { get; set; }
    public string C4s { get; set; }
    **/
    public Node Prev { get; set; }
    public Node Defnext { get; set; }
    /** Previous Version
    public Node C0 { get; set; }
    public Node C1 { get; set; }
    public Node C2 { get; set; }
    public Node C3 { get; set; }
    public Node C4 { get; set; }
    **/
    
    //I'm changing it up again. All the data for each choice is going to be stored in a string array. Nice and simple, no confusion with the god damn choice class
    //each C(0-4) is going to have the info in the order of (text, nodeid (node it points to), stat restrictions, and stat changes)
    public Node()
    {
        this.C0 = new string[4];
        this.C1 = new string[4];
        this.C2 = new string[4];
        this.C3 = new string[4];
        this.C4 = new string[4];
    }

    public Node(int id) 
    {
        this.Id = id;
        this.C0 = new string[4];
        this.C1 = new string[4];
        this.C2 = new string[4];
        this.C3 = new string[4];
        this.C4 = new string[4];
        
    }

    public Node(int id,string question, string title, string imageTitle, string[] c0, string[] c1, string[] c2, string[] c3, string[] c4)
    {
        this.Id = id;
        this.Question = question;
        this.Title = title;
        this.ImageTitle = imageTitle;
        this.C0 = c0;
        this.C1 = c1;
        this.C2 = c2;
        this.C3 = c3;
        this.C4 = c4;
    }

    public Node(int id, string question, string title, string imageTitle, string[] c0, string[] c1, string[] c2, string[] c3, string[] c4, Node prev, Node defnext)
    {
        this.Id = id;
        this.Question = question;
        this.Title = title;
        this.ImageTitle = imageTitle;
        this.C0 = c0;
        this.C1 = c1;
        this.C2 = c2;
        this.C3 = c3;
        this.C4 = c4;
        this.Prev = prev;
        this.Defnext = defnext;
    }

    //copy all information from n1 to n2
    public void copyNode(Node n1, Node n2)
    {
        n2.Id = n1.Id;
        n2.Question = n1.Question;
        n2.Title = n1.Title;
        n2.ImageTitle = n1.ImageTitle;
        n2.C0 = n1.C0;
        n2.C1 = n1.C1;
        n2.C2 = n1.C2;
        n2.C3 = n1.C3;
        n2.C4 = n1.C4;
        n2.Prev = n1.Prev;
        n2.Defnext = n1.Defnext;

        return;
    }

    public static void Main(string[] args)
    {
        //int id = 0;
        //string question = "questooon?";
        //string title = "Tittttle";
        //Node node = null;
        //string c1s = "ahh";
        //string c2s = "booo";
        //string c3s = "wooo";
        //string c4s = "eeehhh";
        //Node root = new Node { Id = id, Question = question, Title = title, C1 = node, C2 = node, C3 = node, C4 = node, C1s = c1s, C2s = c2s, C3s = c3s, C4s = c4s, Prev = node, Defnext = node };
        //root.Defnext = new Node { Id = id, Question = question, Title = title, C1 = node, C2 = node, C3 = node, C4 = node, C1s = c1s, C2s = c2s, C3s = c3s, C4s = c4s, Prev = node, Defnext = node };
        //PrintNode(root);
        //PrintNode(root.Defnext);
    }

    

}