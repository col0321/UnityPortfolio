using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot_Drag : MonoBehaviour
{
    public ItemInfo itemInfo;
    private int amount;

    public GameObject m_Image;
    public GameObject m_text;

    private ItemSlot targetSlot = null;

    private GameObject pickUpObj;
    // private GameObject pickUpObj;

    private List<ItemSlot> lstTargets = new List<ItemSlot>();

    public void PickUp(ItemInfo newItem, GameObject obj, int _amount, Vector2 screenPos)
    {
        pickUpObj = obj;
        itemInfo = newItem;
        itemInfo = newItem;
        m_Image.GetComponent<Image>().sprite = newItem.image;
        amount = _amount;
        this.transform.SetAsLastSibling();
        this.transform.position = new Vector3(screenPos.x, screenPos.y, 0.0f);
    }

    public void Drag(Vector2 screenPos)
    {
        this.transform.position = new Vector3(screenPos.x, screenPos.y, 0.0f);
    }

    private void FindSlot()
    {
        float temp = 100;
        for (int i = 0; i < lstTargets.Count; i++)
        {
            if (temp > Vector3.Distance(lstTargets[i].transform.position, this.transform.position))
            {
                temp = Vector3.Distance(lstTargets[i].transform.position, this.transform.position);
                targetSlot = lstTargets[i];
            }
        }
    }

    public void Drop()
    {
        if (itemInfo == null) return;
        FindSlot();
        if (targetSlot == null)
        {
            pickUpObj.GetComponent<ItemSlot>().SetInfo(itemInfo);
            pickUpObj.GetComponent<ItemSlot>().Amount = amount;
            return;
        }
        if (targetSlot.GetComponent<ItemSlot>().Shop == false)
        {
            pickUpObj.GetComponent<ItemSlot>().SetInfo(targetSlot.GetComponent<ItemSlot>().GetItemInfo());
            pickUpObj.GetComponent<ItemSlot>().Amount = targetSlot.GetComponent<ItemSlot>().Amount;
            pickUpObj = null;
            targetSlot.GetComponent<ItemSlot>().ChangeInfo(itemInfo);
            targetSlot.GetComponent<ItemSlot>().Amount = amount;
            targetSlot = null;
        }
        else if (targetSlot.GetComponent<ItemSlot>().Shop == true)
        {
            Debug.Log("µé¾î¿È");
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerInfo>().money += itemInfo.value * amount;
            pickUpObj = null;
            targetSlot = null;
            amount = 0;
            itemInfo = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemSlot itemSlot = collision.GetComponent<ItemSlot>();
        if (itemSlot != null)
        {
            lstTargets.Add(itemSlot);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemSlot itemSlot = collision.GetComponent<ItemSlot>();
        if (itemSlot != null)
        {
            lstTargets.Remove(itemSlot);
        }
    }
}
