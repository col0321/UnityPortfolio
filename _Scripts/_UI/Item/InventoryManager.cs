using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] Slot;
    public GameObject ItemDataBase;
    public bool isPicking = false;
    public bool InventoryOnoff = false;
    private Vector3 startPosition;
    private Vector3 EndPosition;
    
    private void Start()
    {
        startPosition = EndPosition = this.transform.position;
        EndPosition.x -= 1500;
        this.transform.position = EndPosition;
    }

    public bool OnOff()
    {
        InventoryOnoff = !InventoryOnoff;
        if (InventoryOnoff)
            this.transform.position = startPosition;
        else
            this.transform.position = EndPosition;

        return InventoryOnoff;
    }

    public void ItemAdd(int num)
    {
        bool chack = false;
        if (ItemDataBase.GetComponent<ItemDataBase>().GetItem(num).overLeap)
        {
            for (int i = 0; i < Slot.Length; i++)
            {
                chack = Slot[i].GetComponent<ItemSlot>().OverLeapSlot(ItemDataBase.GetComponent<ItemDataBase>().GetItem(num));
                if (chack)
                    break;
            }
        }

        if (chack == false)
        {
            for (int i = 0; i < Slot.Length; i++)
            {
                if (Slot[i].GetComponent<ItemSlot>().SetInfo(ItemDataBase.GetComponent<ItemDataBase>().GetItem(num)))
                    break;
            }
        }
    }
}
