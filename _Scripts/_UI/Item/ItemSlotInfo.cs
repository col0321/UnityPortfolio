using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotInfo : MonoBehaviour
{
    public Sprite[] sprite;

    public void spriteChange(int num)
    {
        this.gameObject.GetComponent<Image>().sprite = sprite[num];
    }
    public void UpdatePosition(Vector3 screenPos)
    {
        this.transform.SetAsLastSibling();
        Vector3 temp = screenPos;
        temp.x += 140f;
        temp.y -= 90;
        this.transform.position = temp;
    }
}
