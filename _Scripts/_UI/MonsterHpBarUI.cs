using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBarUI : MonoBehaviour
{
    public GameObject Obj;
    private Vector3 vec;
    private GameObject parent;
    public Vector2 xyVertex;

    // Start is called before the first frame update
    public void init()
    {
        parent = GameObject.Find("MonsterHpBarUI");
        this.transform.SetParent(parent.transform);
        vec = new Vector3(xyVertex.x, xyVertex.y, 0);
        this.transform.localScale = new Vector3(0.4f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(Obj.transform.position + vec);
            this.GetComponent<Slider>().value = (float)((float)(Obj.GetComponent<MonsterAi>().hp / (float)(Obj.GetComponent<MonsterAi>().GetHeadHp())));
        }
    }
}
