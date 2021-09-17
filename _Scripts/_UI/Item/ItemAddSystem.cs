using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
     

public class ItemAddSystem : MonoBehaviour
{
    
    public Transform trContent;
    public Scrollbar myScrollbar;
    public GameObject Image;
    // Start is called before the first frame update

    public void AddChat(int num)
    {

        GameObject temp = Instantiate(Image, trContent) as GameObject;
        temp.GetComponent<DeleteImageItem>().init(num);
    }
}
