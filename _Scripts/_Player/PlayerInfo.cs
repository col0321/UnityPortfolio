using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    //======================================//

    //private :
    [SerializeField]
    private int Lv;
    [SerializeField]
    private float Hp;
    private float HeadHp;
    [SerializeField]
    private float Mp;
    private float HeadMp;
    [SerializeField]
    private float offensePower;
    [SerializeField]
    private float HeadExp;
    [SerializeField]
    private float deffensePower;
    public int money;

    public GameObject MoneyText;

    //======================================//
    //======================================//
    //public : 
    public GameObject HpBar;
    public GameObject MpBar;
    public GameObject UI;
    public float Exp;
    //======================================//

    //======================================//

    //======================================//
    //GetSet :
    public float HP { get {return Hp;} set { Hp = value; } }
    public float MP { get {return Mp;} set { Mp = value; } }
    public float EXP { get {return Exp; } set { Exp = value; } }
    public float HeadHP { get { return HeadHp; } set { HeadHp = value; } }
    public float HeadMP { get { return HeadMp; } set { HeadMp = value; } }
    public float OffensePower { get {return offensePower; } set { offensePower = value; } }
    public float DeffensePower { get {return deffensePower; } set { deffensePower = value; } }
    //======================================//

    private void Start()
    {
        HeadHp = Hp;
        HeadMp = Mp;
        UI.GetComponent<UIManager>().LvText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "LV." + Lv.ToString();
    }

    private void Update()
    {
        MoneyText.GetComponent<TMPro.TextMeshProUGUI>().text = money.ToString();
        HpBar.GetComponent<Slider>().value = (float)(Hp) / (float)(HeadHp);
        HpBar.GetComponent<SlotImage>().childText.GetComponent<TMPro.TextMeshProUGUI>().text = Hp.ToString("N0") + " / " + HeadHP.ToString("N0");
        MpBar.GetComponent<Slider>().value = (float)(Mp) / (float)(HeadMp);
        MpBar.GetComponent<SlotImage>().childText.GetComponent<TMPro.TextMeshProUGUI>().text = Mp.ToString("N0") + " / " + HeadMP.ToString("N0");
        UI.GetComponent<UIManager>().ExpGauge.GetComponent<Slider>().value = Exp / HeadExp;
        if (Exp > HeadExp)
        {
            Lv++;
            UI.GetComponent<UIManager>().LvText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "LV." + Lv.ToString();
            Exp = 0.0f;
            HeadExp = 10000 + (Lv * 3000);
        }
    }
}
