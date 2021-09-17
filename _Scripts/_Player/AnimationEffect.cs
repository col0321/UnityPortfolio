using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEffect : AnimationEventUtill
{

    public Transform EffectParent;
    public GameObject[] weapon;
    public LayerMask Monster;
    private float color = 0;
    #region 
    #endregion
    // ====== private ===========//
    #region 기본공격
    bool comboPossible = false;
    int comboStep = 0;
    #endregion
    #region 스킬1
    private bool gauge = false;
    private bool gauge_E = false;
    #region 스킬E 
    private bool E_Zoom1 = false;
    private bool E_Zoom2 = false;
    #endregion

    #endregion
    // ====== public ===========//
    #region 기본공격
    public bool underAttack = false;
    #endregion
    #region 스킬1 
    public float gaugePower = 0.0f;
    #endregion

    #region 스킬E 
    public float gaugePower_E = 0.0f;
    public GameObject E_Impact;
    #endregion

    // ============== 공용 ======================//
    public GameObject managerUI;
    public GameObject[] GaugeEffect;
    public GameObject Smoke;
    public EnemyManager enemyManager;
    Coroutine weaponColor;

    // 스킬 1번사용중
    private void Update()
    {
        if (gauge)
        {
            if (Input.GetKey(KeyCode.A))
            {
                GaugeEffect[0].SetActive(true);
                gaugePower += 1.0f * Time.deltaTime;
                if (gaugePower > 1.0f)
                    gaugePower = 1;

                managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = gaugePower;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                myAnim.Play("Skill_01_Finish");
                E_InGaugeSet(0);
            }
            else
            {
                myAnim.Play("Skill_01_Finish");
                E_InGaugeSet(0);
            }
        }

        GameObject temp;
        if (gauge_E)
        { 
            if (Input.GetKey(KeyCode.D))
            {
                GaugeEffect[0].SetActive(true);
                if(gaugePower_E < 2.0f)
                    gaugePower_E += 1.5f * Time.deltaTime;

                if (gaugePower_E > 2.0f)
                {
                    gaugePower_E = 2;
                }                    
                managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = (gaugePower_E % 1.01f);

                if (gaugePower_E > 0.7f && !E_Zoom1)
                {
                    E_Zoom1 = true;
                    CameraZoomIn(53f);
                    camera.AwhileShake(0.25f);
                    temp = Instantiate(E_Impact,this.transform.position,Quaternion.identity, EffectParent);
                    
                }
                if (gaugePower_E > 1.5f && !E_Zoom2)
                {
                    CameraShakeTrue(0.025f);
                }

                if (gaugePower_E > 1.7f && !E_Zoom2)
                {
                    E_Zoom2 = true;
                    temp = Instantiate(E_Impact, this.transform.position, Quaternion.identity, EffectParent);
                    CameraZoomIn(47f);
                }

            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                E_InGaugeSet(3);
            }
            else
            {
                E_InGaugeSet(3);
            }
        }
        
    }


    // 기본 공격 이팩트 및 에니메이션 모션 //
    #region 기본공격이팩트 

    public void E_BasicAttack()
    {
        if (comboStep == 0)
        {
            myAnim.SetBool("Move", false);
            myAnim.SetBool("ComboAttack", true);
            comboStep = 1;
            U_TurnbeforeSkill();
            underAttack = true;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }
    public void E_BasicAttackEffect() { }//basicEffect.SetActive(true); }
    public void E_ComboPossible() { comboPossible = true; }

    public void E_ChackCombo(int num)
    {
        if (comboStep == num)
            E_ComboReSet();
        else
            U_TurnbeforeSkill();
    }
    public void E_ComboReSet()
    {
        myAnim.SetBool("Move", false);
        comboStep = 0;
        comboPossible = false;
        myAnim.SetBool("ComboAttack", false);
        underAttack = false;
        //basicEffect.SetActive(false);
    }

    #endregion
    // ===================================== 스킬 이팩트 관련 ===================================== //
    #region 공용 이팩트 
    private void E_Effect(GameObject obj)
    {
        
        GameObject temp;
        if (obj.name == "W_Effect")
            temp = Instantiate(obj, this.transform.position, this.transform.rotation, this.transform);
        else if(obj.name == "DivineJudgment")
            temp = Instantiate(obj, this.transform.position, Quaternion.identity, this.transform);
        else if (obj.name == "SmokeSkill")
        {
            Vector3 pos = this.transform.position;
            pos.y += 1.0f;
            temp = Instantiate(obj, pos, Quaternion.Euler(90, 0, 0), this.transform);
        }
        else
            temp = Instantiate(obj,this.transform.position,this.transform.rotation,EffectParent);        
    }
    #endregion
    // ===================================== 스킬 이팩트 관련 ===================================== //

    // ===================================== 스킬 1번  ============================================ //
    #region 스킬1관련
    public void E_InGaugeSet(int num)
    {
        if (num == 0)
        {
            gauge = false;
            GaugeEffect[0].SetActive(false);
            managerUI.GetComponent<UIManager>().SetActiveObject("GaugeSlider", false);
            managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = 0.0f;
        }
        else if (num == 1)
        {
            gaugePower = 0;
            gauge = true;
            managerUI.GetComponent<UIManager>().SetActiveObject("GaugeSlider", true);
            managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = 0.0f;
            GameObject temp = Instantiate(Smoke, this.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)), EffectParent);
        }

        if (num == 2)
        {
            gaugePower_E = 0;
            A_AnimationSpeed(0.07f);
            gauge_E = true;
            GaugeEffect[0].SetActive(true);
            managerUI.GetComponent<UIManager>().SetActiveObject("GaugeSlider", true);
            managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = 0.0f;
            CameraZoomInSpeed(50);
        }

        else if (num == 3)
        {
            E_Zoom1 = false;
            E_Zoom2 = false;
            CameraZoomOut();
            CameraShakeFalsd();
            A_AnimationSpeed(1f);
            gauge_E = false;
            GaugeEffect[0].SetActive(false);
            managerUI.GetComponent<UIManager>().SetActiveObject("GaugeSlider", false);
            managerUI.GetComponent<UIManager>().GaugeSlider.GetComponent<Slider>().value = 0.0f;
        }
    }
    #endregion
    // ===================================== 스킬 4번  ============================================ //
    #region 스킬4번
    public void E_Effect_SKill_R(GameObject obj)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 7.0f);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                { 
                    
                    GameObject dust = Instantiate(obj);
                    dust.GetComponent<ChainEffect>().Monster = hitColliders[i].transform.gameObject;
                    dust.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.78f, this.transform.position.z);
                    dust.transform.LookAt(hitColliders[i].transform);
                }
                i++;
            }
        }

    }
    #endregion

    // ===================================== 무기 색상 바꾸기 =====================================//
    #region 무기색상
    public void E_WeaponColorTrue(){ if (weaponColor != null) StopCoroutine(weaponColor); weaponColor = StartCoroutine(X_WeaponTrue());}
    public void E_WeaponColorFalse(){ if (weaponColor != null) StopCoroutine(weaponColor); weaponColor = StartCoroutine(X_WeaponFalse()); }

    private IEnumerator X_WeaponTrue()
    {
        int num = 0;
        while (true)
        {
            color += 0.5f * Time.deltaTime;
            weapon[num].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 255, 255) * color);
            num++;
            num %= 21;
            yield return null;
        }
    }
    private IEnumerator X_WeaponFalse()
    {
        while (color >= 0)
        {
            color -= 3.0f * Time.deltaTime;
            for (int i = 0; i < 21; i++)
            {
                weapon[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 255, 255) * color);
            }
            yield return null;
        }
        color = 0;
        for (int i = 0; i < 21; i++)
        {
            weapon[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 255, 255) * 0);
        }
    }
    #endregion
}
