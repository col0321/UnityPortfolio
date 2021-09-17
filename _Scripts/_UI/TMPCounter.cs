using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPCounter : MonoBehaviour
{
    public GameObject[] counter;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        int temp = 0;

        for (int i = 0; i < counter.Length; i++)
        {
            if (counter[i]== null)
                temp++;
        }


        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "(" + temp.ToString() + " / 23)";

    }
}
