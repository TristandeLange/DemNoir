using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    private int maxInv = 25;
    [SerializeField]
    public List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (this.isActiveAndEnabled)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if () //HEREEEE WORK HERE NEXT -------------------------------------------------------------
                {
                
                }
            }
            refreshInventory();

        }
    }

    IEnumerator setChildPos() 
    {
        RectTransform rTrans;
        foreach (Transform child in this.transform)
        {
            rTrans = child.GetComponent<RectTransform>();
            rTrans.localPosition = Vector2.zero;
            rTrans.localScale = Vector2.one;
            rTrans.offsetMax = Vector2.zero;
            rTrans.offsetMin = Vector2.zero;
        }
        yield return new WaitForSeconds(.1f);
    }
    public void refreshInventory()
    {

        float size = 1 / Mathf.Sqrt(maxInv);
        float xanch = 0;
        float yanch = 1;
        RectTransform rTrans;

        foreach (var item in items)
        {
            if(item.GetComponent<ItemScript>().getCount() <= 0)
            {
                item.GetComponent<ItemScript>().setCount(1);
                this.items.Remove(item);
                refreshInventory();
                
                break;
            }
            GameObject focus = Instantiate(item);
            focus.transform.SetParent(this.transform, false);
            rTrans = focus.GetComponent<RectTransform>();
            rTrans.anchorMin = new Vector2(xanch, yanch - size);
            rTrans.anchorMax = new Vector2(xanch + size, yanch);

            xanch += size;
            if (xanch == 1)
            {
                xanch = 0;
                yanch -= size;
            }
            if (yanch == 0)
            {
                break;
            }
        }

        StartCoroutine(setChildPos());

    }




}
