using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
    public TMPro.TMP_Text myText;    

    public void SetText(string text,int type)
    {   
        switch(type)
        {
            case 0:
                break;
            case 1:
                myText.color = Color.red;
                break;
            case 2:
                myText.color = Color.blue;
                break;
            case 3:
                myText.color = Color.yellow;
                break;
        }
                
        Vector2 size = this.transform.GetComponent<RectTransform>().sizeDelta;
        string temp = "";
        string result = "";
        int line = 1;
        for(int i=0;i<text.Length;++i)
        {
            Vector2 textsize = myText.GetPreferredValues(temp + text[i]);
            if(textsize.x >= size.x)
            {
                result += temp + '\n';
                temp = "";
                line++;
            }
            temp += text[i];
        }
        result += temp;
        myText.text = result;
        size.y = myText.preferredHeight;
        this.transform.GetComponent<RectTransform>().sizeDelta = size;
        Debug.Log(myText.preferredHeight);
    }
}
