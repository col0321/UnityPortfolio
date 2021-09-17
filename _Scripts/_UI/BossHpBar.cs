using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{

    public GameObject Boss;
    public GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAi ai = Boss.GetComponent<MonsterAi>();
        this.gameObject.GetComponent<Slider>().value = (float)(ai.hp) / (float)(ai.GetHeadHp());
        float a = (float)(ai.hp) / (float)(ai.GetHeadHp());
        a *= 100f;
        Text.GetComponent<TMPro.TextMeshProUGUI>().text = a.ToString("F0") + "%";
    }
}
