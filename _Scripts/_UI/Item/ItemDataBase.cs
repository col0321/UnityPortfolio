using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ItemInfo
{
    public int itemCode;
    public Sprite image;
    public bool overLeap;
    public bool potion;
    public int Hp;
    public int Mp;
    public int value;

    public ItemInfo() { }
    public ItemInfo(int _itemCode, Sprite _image, bool _overLeap, bool _potion, int _value, int _Hp, int _Mp)
    {
        itemCode = _itemCode;
        image = _image;
        overLeap = _overLeap;
        potion = _potion;
        value = _value;
        Hp = _Hp;
        Mp = _Mp;
    }
}


public class ItemDataBase : MonoBehaviour
{

    public Sprite[] itemIcons;        
    private List<ItemInfo> itemInfos; 

    private void Awake()
    {
        //아이템 리스트 셋팅
        //ItemInfo(int _itemCode, Sprite _image, bool _overLeap, bool _potion, int _value, int _Hp, int _Mp)
        itemInfos = new List<ItemInfo>();
        itemInfos.Add(new ItemInfo(0,itemIcons[0],true,true,200,300,0));
        itemInfos.Add(new ItemInfo(1,itemIcons[1],true,true,500,0,1000));
        itemInfos.Add(new ItemInfo(2,itemIcons[2],true,true,700,1000,0));
        itemInfos.Add(new ItemInfo(3,itemIcons[3],true,false,100000,0,0));

    }

    public ItemInfo GetItem(int num)
    {
        return itemInfos[num];
    }

    public int ItemCount
    {
        get
        {
            return itemInfos.Count;
        }
    }

}
