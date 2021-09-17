using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChatSystem : MonoBehaviour
{

    public readonly string[] Modelist = { "일반", "전체", "파티", "채널" };
    public Transform trContent;
    public Scrollbar myScrollbar;
    Coroutine scrollbarzerovalue = null;
    public GameObject cheatItem;


    private List<GameObject> list = new List<GameObject>();

    void Start()
    {
        foreach (string mode in Modelist)
        {
            TMPro.TMP_Dropdown.OptionData item = new TMPro.TMP_Dropdown.OptionData(mode);
        }
    }

    private void Update()
    {
         if (list.Count > 50)
         {
            Destroy(list[0]);
         }
    }


    public void AddChatString(int num)
    {
        GameObject obj = Instantiate(cheatItem, trContent) as GameObject;
        if(num == 3)
            obj.GetComponent<TMPro.TextMeshProUGUI>().text = " [시스템] 골드 주화를 획득하였습니다.";
        if (num == 2)
            obj.GetComponent<TMPro.TextMeshProUGUI>().text = " [시스템] 체력 물약을 획득하였습니다.";
        if (num == 1)
            obj.GetComponent<TMPro.TextMeshProUGUI>().text = " [시스템] 마나 물약을 획득하였습니다.";
       // if (scrollbarzerovalue != null) StopCoroutine(scrollbarzerovalue);
        scrollbarzerovalue = StartCoroutine(SetScrollZeroValue(3f));
        list.Add(obj);
    }
    public void AddChatStringDamage(int num)
    {
        GameObject obj = Instantiate(cheatItem, trContent) as GameObject;
        obj.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
        obj.GetComponent<TMPro.TextMeshProUGUI>().text = " [시스템] " + num.ToString() + " 데미지를 입혔습니다."; 

        // if (scrollbarzerovalue != null) StopCoroutine(scrollbarzerovalue);
        scrollbarzerovalue = StartCoroutine(SetScrollZeroValue(3f));
        list.Add(obj);
    }

    IEnumerator SetScrollZeroValue(float speed)
    {
        while (myScrollbar.value > 0f)
        {
            myScrollbar.value -= Time.deltaTime * speed;
            yield return null;
        }
        myScrollbar.value = 0;
    }
}
