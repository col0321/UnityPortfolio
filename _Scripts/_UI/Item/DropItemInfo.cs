using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInfo : MonoBehaviour
{
    public int ItemCode;
    public LayerMask player;

    public GameObject InventoryManager;
    private GameObject trUi;
    private GameObject itemAddSyste;
    public GameObject Text;
    private Vector3 vec;
    private string str;
    private GameObject chatItem;

    private void Awake()
    {
        chatItem = GameObject.Find("ChatSystem");
        InventoryManager = GameObject.Find("Inventroy");
        itemAddSyste = GameObject.Find("ItemAddSystem");
        trUi = GameObject.Find("ItemInfo");
        Text = Instantiate(Text, trUi.transform);

        if (ItemCode == 1)
            str = "마나 포션";

        if (ItemCode == 2)
            str = "체력 포션";

        if (ItemCode == 3)
            str = "골드 주화";

        Text.GetComponent<DropInfoName>().Text.GetComponent<TMPro.TextMeshProUGUI>().text = str;
        vec = new Vector3(0, 1, 0);

    }

    void Update()
    {
        Text.transform.position = Camera.main.WorldToScreenPoint(this.transform.position+vec);
    }


    private void OnTriggerEnter(Collider other)
    {
        int temp = 1 << other.gameObject.layer;
        if ((player & temp) == temp)
        {
            InventoryManager.GetComponent<InventoryManager>().ItemAdd(ItemCode);
            itemAddSyste.GetComponent<ItemAddSystem>().AddChat(ItemCode);
            chatItem.GetComponent<ChatSystem>().AddChatString(ItemCode);
            Destroy(Text.gameObject);
            Destroy(this.gameObject);
        }
    }
}
