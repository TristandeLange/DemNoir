using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinkedListNode : MonoBehaviour
{
    public int id;
    public string data;
    public LinkedListNode next;
    public LinkedListNode prev;

    public LinkedListNode() { }

    public LinkedListNode(int id, string data, LinkedListNode next)
    {
        this.id = id;
        this.data = data;
        this.next = next;
    }

    public LinkedListNode(int id, string data, LinkedListNode next, LinkedListNode prev)
    {
        this.id = id;
        this.data = data;
        this.next = next;
        this.prev = prev;
    }
}
