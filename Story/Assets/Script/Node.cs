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
    public Choice C0 { get; set; }
    public Choice C1 { get; set; }
    public Choice C2 { get; set; }
    public Choice C3 { get; set; }
    public Choice C4 { get; set; }
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
    public Node()
    {
        this.C0 = new Choice();
        this.C1 = new Choice();
        this.C2 = new Choice();
        this.C3 = new Choice();
        this.C4 = new Choice();
    }

    public Node(int id) 
    {
        this.Id = id;
        this.C0 = new Choice();
        this.C1 = new Choice();
        this.C2 = new Choice();
        this.C3 = new Choice();
        this.C4 = new Choice();
    }

    public Node(int id,string question, string title, Choice c0, Choice c1, Choice c2, Choice c3, Choice c4)
    {
        this.Id = id;
        this.Question = question;
        this.Title = title;
        this.C0 = c0;
        this.C1 = c1;
        this.C2 = c2;
        this.C3 = c3;
        this.C4 = c4;
    }

    //public Node(int id, string question, string title, string c0, string c1, string c2, string c3, string c4)
    //{
    //   this.Id = id;
    //    this.Question = question;
    //    this.Title = title;
    //    this.C0 = new Choice(c0);
    //    this.C1 = new Choice(c1);
    //   this.C2 = new Choice(c2);
    //    this.C3 = new Choice(c3);
    //    this.C4 = new Choice(c4);
    //}
    public Node(int id, string question, string title, Choice c0, Choice c1, Choice c2, Choice c3, Choice c4, Node prev, Node defnext)
    {
        this.Id = id;
        this.Question = question;
        this.Title = title;
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
        n2.C0 = n1.C0;
        n2.C1 = n1.C1;
        n2.C2 = n1.C2;
        n2.C3 = n1.C3;
        n2.C4 = n1.C4;
        n2.Prev = n1.Prev;
        n2.Defnext = n1.Defnext;

        return;
    }


    public static void PrintNode(Node node)
    {
        Debug.Log("Node info is ");
        Debug.Log(node.Id);
        Debug.Log(node.Question);
        Debug.Log(node.Title);
        Debug.Log(node.C0.text);
        Debug.Log(node.C0.node.Id);
        Debug.Log(node.C0.statChanges);
        Debug.Log(node.C0.statRestrictions);
        Debug.Log(node.C1.text);
        Debug.Log(node.C1.node.Id);
        Debug.Log(node.C1.statChanges);
        Debug.Log(node.C1.statRestrictions);
        Debug.Log(node.C2.text);
        Debug.Log(node.C2.node.Id);
        Debug.Log(node.C2.statChanges);
        Debug.Log(node.C2.statRestrictions);
        Debug.Log(node.C3.text);
        Debug.Log(node.C3.node.Id);
        Debug.Log(node.C3.statChanges);
        Debug.Log(node.C3.statRestrictions);
        if (node.C4 != null)
        {
            Debug.Log(node.C4.text);
            Debug.Log(node.C4.node.Id);
            Debug.Log(node.C4.statChanges);
            Debug.Log(node.C4.statRestrictions);
        }
        //Console.WriteLine(node.Prev);
        //Console.WriteLine(node.Defnext);
        //Console.WriteLine(node.C1);
        //Console.WriteLine(node.C2);
        //Console.WriteLine(node.C3);
        //Console.WriteLine(node.C4);
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

    //alright so I made this choice class to use in Node. This should allow .
    //Ideally I will be able to use this to streamline the above code and make statchanges easier
    

}

public class Choice
{
    public string text { get; set; }
    public Node node { get; set; }
    public string statChanges { get; set; }
    public string statRestrictions { get; set; }


    public Choice()
    {

    }
    public Choice(string text)
    {
        this.text = text;
    }
    public Choice(string text, Node node)
    {
        this.text = text;
        this.node = node;
    }
    public Choice(string text, Node node, string statChanges, string statRestrictions)
    {
        this.text = text;
        this.node = node;
        this.statChanges = statChanges;
        this.statRestrictions = statRestrictions;
    }

}