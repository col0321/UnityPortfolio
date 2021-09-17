using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteImageItem : MonoBehaviour
{
    float time = 0;

    public GameObject sonImage;
    public GameObject sonText;

    public Sprite[] sprite;
    private string str;

    public void init(int num)
    {

        sonImage.GetComponent<Image>().sprite = sprite[num];
        if (num == 1)
            str = "���� ����";

        if (num == 2)
            str = "ü�� ����";

        if (num == 3)
            str = "��� ��ȭ";

        sonText.GetComponent<TMPro.TextMeshProUGUI>().text = str;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1.0f * Time.deltaTime;
        if (time > 2)
        {
            Vector3 temp = this.transform.localScale;
            temp.y -= 3.0f * Time.deltaTime;
            this.transform.localScale = temp;
            if (temp.y <= 0)
                Destroy(this.gameObject);

        }
    }
}
