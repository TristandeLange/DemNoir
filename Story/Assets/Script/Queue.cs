using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Queue
{
    private Node head;
    private Node tail; 

    public Queue() 
    {
        head = null;
        tail = null;
    }

    public void Enqueue(Node data)
    { 
        if (IsEmpty())
        {
            head = data;
        }
        else if (tail == null) //only one node 
        {
            tail = data;
            tail.Defnext = head;
        }
        else 
        {
            Node temp = tail;                //I hope this works but it might be misreferencing
            tail = data;
            tail.Defnext = temp;
        }

        return;
    }

    public Node Dequeue() 
    {
        if(!IsEmpty()) 
        {
            throw new InvalidOperationException("The Queue Is Empty");
        }

        Node d = head;

        if(tail == null) //aka there is only the head
        {
            head = null;
            return d;
        }

        Node temp = tail;

        while( temp.Defnext != head ) 
        {
            temp = temp.Defnext;
        }

        temp.Defnext = null;
        head = temp;

        if(tail == head) 
        {
            tail = null;
        }

        return d;
    }

    public Node Head() 
    {
        if (IsEmpty())
            throw new InvalidOperationException("The queue is empty");
        return head; 
    }
    public bool IsEmpty() 
    {
        return head == null;
    }

    public Node SearchForNodeID(int id) 
    {
        Node temp = tail;
        while(temp.Defnext != null) 
        {
            if(temp.Id == id) { return temp; }
        }

        return null;
    }

}
