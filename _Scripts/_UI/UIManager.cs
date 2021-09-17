using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject GaugeSlider;
    public GameObject[] slotImage;
    public GameObject Exp;
    public GameObject ExpGauge;
    public GameObject LvText;
    private bool ExpSet;
    private float time;

    
    public void SetActiveObject(string str, bool set)
    {
        if (str == "GaugeSlider")
            GaugeSlider.SetActive(set);
    }
    public void ExpPlus(float f)
    {
        ExpSet = true;
        Exp.GetComponent<ExpUI>().HeadExp += f;
        time = 0.0f;
    }
    private void Update()
    {
        if (ExpSet)
        {
            if (!Exp.activeSelf)
                Exp.SetActive(true);

            if (Exp.GetComponent<ExpUI>().HeadExp > Exp.GetComponent<ExpUI>().currentExp)
            {
                Exp.GetComponent<ExpUI>().currentExp += 1000 * Time.deltaTime;
                Exp.GetComponent<ExpUI>().Text.GetComponent<TMPro.TextMeshProUGUI>().text = "Exp + " + Exp.GetComponent<ExpUI>().currentExp.ToString("N0");
            }
            else
                time += 1.0f * Time.deltaTime;

            if (time > 3)
            {
                Exp.GetComponent<ExpUI>().currentExp = 0.0f;
                Exp.GetComponent<ExpUI>().HeadExp = 0.0f;
                Exp.SetActive(false);
                ExpSet = false;
            }
        }
    }
}
