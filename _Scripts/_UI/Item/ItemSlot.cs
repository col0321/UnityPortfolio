using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private ItemInfo itemInfo = null;
    private int amount;
    public bool useSlot;
    public bool Shop;

    public GameObject m_Image;
    public GameObject m_text;
    public GameObject SlotDrag;
    public GameObject SlotInfoImage;
    private bool isPick;

    public int UseItem(int num)
    {
        if (itemInfo != null)
        {
            if (itemInfo.Hp > 0 && num ==0)
            {
                amount--;
                return itemInfo.Hp;
            }
            if (itemInfo.Mp > 0 && num == 1)
            {
                amount--;
                return itemInfo.Mp;
            }
          
            if (amount == 0)
                SetInfo(null);

            if (amount > 1)
                m_text.GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
            else
                m_text.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        return 0;
    }

    public ItemInfo GetItemInfo()
    {
        return itemInfo;
    }
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (amount > 1)
                m_text.GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
            else
                m_text.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
    }
    public void ChangeInfo(ItemInfo newItem)
    {
        m_Image.SetActive(true);
        m_text.SetActive(true);
        itemInfo = newItem;
        m_Image.GetComponent<Image>().sprite = newItem.image;
    }

    public bool SetInfo(ItemInfo newItem)
    {
        if (itemInfo == null && newItem != null)
        {
            m_Image.SetActive(true);
            m_text.SetActive(true);
            itemInfo = newItem;
            m_Image.GetComponent<Image>().sprite = newItem.image;
            if (amount <= 0)
                amount = 1;
            return true;
        }
        if (newItem == null)
        {
            m_Image.SetActive(false);
            m_text.SetActive(false);
            amount = 0;
            m_Image.GetComponent<Image>().sprite = null;
            itemInfo = null;
            return true;
        }
        return false;
    }
    public bool OverLeapSlot(ItemInfo newItem)
    {
        if (itemInfo == null) return false;

        if (itemInfo.itemCode == newItem.itemCode)
        {
            if (newItem.overLeap)
            {
                amount++;
                m_text.GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
                return true;
            }
            else
                return false;
        }
        return false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 픽업중인 아이템이 없다면 넘겨라
        if (isPick == false) return;
        SlotDrag.GetComponent<ItemSlot_Drag>().Drop();

        isPick = false;
        SlotDrag.SetActive(false);
        this.gameObject.transform.parent.GetComponent<InventoryManager>().isPicking = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 빈칸 클릭은 걍 넘겨라
        if (itemInfo == null) return;
        SlotDrag.SetActive(true);
        isPick = true;
        SlotDrag.GetComponent<ItemSlot_Drag>().PickUp(itemInfo, this.transform.gameObject, amount, eventData.position);
        SetInfo(null);
        this.gameObject.transform.parent.GetComponent<InventoryManager>().isPicking = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SlotDrag.GetComponent<ItemSlot_Drag>().Drag(eventData.position);
    }

    public void OnMouseEnter()
    {
        if (this.gameObject.transform.parent.GetComponent<InventoryManager>().isPicking == false && itemInfo != null)
        {
            if (SlotInfoImage.activeSelf == false)
            {
                SlotInfoImage.SetActive(true);
                SlotInfoImage.GetComponent<ItemSlotInfo>().spriteChange(itemInfo.itemCode);
            }
            SlotInfoImage.GetComponent<ItemSlotInfo>().UpdatePosition(this.transform.position);
        }
    }

    public void OnMouseExit()
    {
        SlotInfoImage.SetActive(false);
    }
}
