using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    [SerializeField] 
    private string itemName;
    [SerializeField]
    private string itemDesc;
    [SerializeField]
    private int id;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int count;


    ItemScript(string Name, string Desc, int ID, Sprite Icon, int Count) 
    {
        this.itemName = Name;
        this.itemDesc = Desc;
        this.id = ID;
        this.icon = Icon;
        this.count = Count;
    }

    private void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = icon;
    }

    public string getName() 
    {
        return itemName;
    }

    public string getDescription() 
    {
        return itemDesc;
    }

    public int getId() 
    { 
        return id;
    }

    public int getCount()
    {
        return count;
    }

    public void setCount(int i)
    {
        this.count = i;
        return;
    }

    public void setCount(string i)
    {
        this.count = Convert.ToInt32(i);
        return;
    }

}
