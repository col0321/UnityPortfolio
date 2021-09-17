using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{

    public GameObject Obj;
    public GameObject son;
    private Vector3 vec;
    private GameObject parent;
    private Vector3 StartPos;
    private bool scale = false;
    private float size = 0.0f;
    private float colorB = 1.0f;
    private bool alpha = false;
    private float alphaSet = 1.0f;


    public Vector2 minMaxX;
    public Vector2 minMaxY;

    // Start is called before the first frame update
    public void Init()
    {
        parent = GameObject.Find("DamageUI");
        this.transform.SetParent(parent.transform);
        float x = Random.Range(minMaxX.x, minMaxX.y);
        float y = Random.Range(minMaxY.x, minMaxY.y);
        vec = new Vector3(x, y, 0);
        this.transform.localScale = new Vector3(0, 0, 0);
        son.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, colorB, 1);
        son.GetComponent<TMPro.TextMeshProUGUI>().material.SetFloat("_UnderlayDilate", 0.0f);
        StartPos = Obj.transform.position;
        son.GetComponent<TMPro.TextMeshProUGUI>().text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void Update()
    {
        vec.y += 0.1f * Time.deltaTime;
        this.transform.position = Camera.main.WorldToScreenPoint(StartPos + vec);
        if (!scale)
        {
            size += 10.0f * Time.deltaTime;
            this.transform.localScale = new Vector3(size, size, 0);
            if (size >= 1.5f)
                scale = true;
        }
        else if (!alpha || scale)
        {
            if(size >= 0.7f)
                size -= 5f * Time.deltaTime;
            this.transform.localScale = new Vector3(size, size, 0);

            colorB -= 1.0f * Time.deltaTime;
            son.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, colorB, 1);
            if (colorB <= 0)
            {
                alpha = true;
                son.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0, 1);
            }

        }

        if (alpha)
        { 
            alphaSet -= 0.5f * Time.deltaTime;
            son.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0, alphaSet);
            Color temp = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().color;
            this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(temp.r, temp.g, temp.b, alphaSet);
            if (alphaSet < 0)
                Destroy(this.gameObject);
        }
    }
}
