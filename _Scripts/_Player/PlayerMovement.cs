using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //======================================//
    //private :
    private float[] SkillCoolTime = new float[8];
    private float[] SkillCurrentTime = new float[8];
    private bool[] Skill = new bool[8];
    Coroutine move = null;
    Coroutine SkillCoroutine = null;
    //======================================//
    //======================================//
    //public : 
    public NavMeshPath path;
    public Animator myAnim = null;
    public GameObject myEvent;
    public UIManager UI;
    public float RotSpeed = 360.0f;
    public float MoveSpeed = 2.0f;
    public float Speed = 2.0f;
    public bool skillOn = false;
    public bool attackOn = false;
    public bool windmill;
    public bool cage = false;
    //======================================//
    //======================================//
    //protected : 
    //======================================//
    protected GameObject target;
    protected bool AirBone = false;
    protected bool invenOnOff;

    public GameObject Slot_01;
    public GameObject Slot_02;
    public Vector3 bossPos;
    public GameObject inven;
    public LayerMask Ground;
    
    protected void Initialize()
    {
        myAnim = this.GetComponentInChildren<Animator>();
        for (int i = 0; i < 8; i++)
        {
            Skill[i] = true;
            SkillCurrentTime[i] = 0.0f;
        }
        SkillCoolTime[0] = 5.0f;
        SkillCoolTime[1] = 10.0f;
        SkillCoolTime[2] = 12.0f;
        SkillCoolTime[3] = 14.0f;
        SkillCoolTime[4] = 12.0f;
        SkillCoolTime[5] = 5.0f;
        SkillCoolTime[6] = 13.0f;
        
    }

    public void UpdateMovement()
    {
        SkillCoolTimeUpdate();
        attackOn = myEvent.GetComponent<AnimationEffect>().underAttack;
        if (this.transform.GetComponent<PlayerInfo>().HeadMP > this.transform.GetComponent<PlayerInfo>().MP)
        {
            this.transform.GetComponent<PlayerInfo>().MP += 100.0f * Time.deltaTime;
        }
        else if(this.transform.GetComponent<PlayerInfo>().HeadMP <= this.transform.GetComponent<PlayerInfo>().MP)
        {
            this.transform.GetComponent<PlayerInfo>().MP = this.transform.GetComponent<PlayerInfo>().HeadMP;
        }

        if (this.transform.GetComponent<PlayerInfo>().HeadHP <= this.transform.GetComponent<PlayerInfo>().HP)
        {
            this.transform.GetComponent<PlayerInfo>().HP = this.transform.GetComponent<PlayerInfo>().HeadHP;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.gameObject.GetComponent<PlayerInfo>().HP += Slot_01.GetComponent<ItemSlot>().UseItem(0);
            this.gameObject.GetComponent<PlayerInfo>().MP += Slot_01.GetComponent<ItemSlot>().UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.gameObject.GetComponent<PlayerInfo>().HP += Slot_02.GetComponent<ItemSlot>().UseItem(0);
            this.gameObject.GetComponent<PlayerInfo>().MP += Slot_02.GetComponent<ItemSlot>().UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            invenOnOff = inven.GetComponent<InventoryManager>().OnOff();
        }

    }
    private void SkillCoolTimeUpdate()
    {
        for (int i = 0; i < 8; i++)
        {
            if (Skill[i] == false)
            {
                SkillCurrentTime[i] += 1.0f * Time.deltaTime;
                UI.GetComponent<UIManager>().slotImage[i].GetComponent<SlotImage>().childImage.GetComponent<Image>().fillAmount = SkillCurrentTime[i] / SkillCoolTime[i];
                UI.GetComponent<UIManager>().slotImage[i].GetComponent<SlotImage>().childText.GetComponent<TMPro.TextMeshProUGUI>().text = (SkillCoolTime[i] - SkillCurrentTime[i]).ToString("F1");
                if (SkillCurrentTime[i] >= SkillCoolTime[i])
                {
                    SkillCurrentTime[i] = 0.0f;
                    UI.GetComponent<UIManager>().slotImage[i].GetComponent<SlotImage>().childText.SetActive(false);
                    Skill[i] = true;
                }
            }
        }
    }

    public void BattleMode()
    {
        if (skillOn == false)
        {

            if (Input.GetKey(KeyCode.Q) && Skill[0])
                SkillCoroutine = StartCoroutine(Skill_Q());

            if (Input.GetKey(KeyCode.W) && Skill[1])
                SkillCoroutine = StartCoroutine(Skill_W());

            if (Input.GetKey(KeyCode.E) && Skill[2])
                SkillCoroutine = StartCoroutine(Skill_E());

            if (Input.GetKey(KeyCode.R) && Skill[3])
                SkillCoroutine = StartCoroutine(Skill_R());

            if (Input.GetKey(KeyCode.A) && Skill[4])
                SkillCoroutine = StartCoroutine(Skill_A());

            if (Input.GetKey(KeyCode.S) && Skill[5])
                SkillCoroutine = StartCoroutine(Skill_S());

            if (Input.GetKey(KeyCode.D) && Skill[6])
                SkillCoroutine = StartCoroutine(Skill_D());
        }
    }

    IEnumerator Skill_A()
    {
        skillOn = true;
        ActionStop();
        SkillCoolTimeSet(4);
        myAnim.Play("Skill_01_Start");
        TurnbeforeSkill();
        minusMp(750);

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_01_Finish"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.90f)
        {
            yield return null;
        }
        skillOn = false;
    }
    IEnumerator Skill_S()
    {
        skillOn = true;
        ActionStop();
        SkillCoolTimeSet(5);
        myAnim.Play("Skill_02");
        TurnbeforeSkill();
        minusMp(300);

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_02"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            yield return null;
        }
        skillOn = false;
    }
    IEnumerator Skill_D()
    {
        skillOn = true;
        ActionStop();
        SkillCoolTimeSet(6);
        myAnim.Play("Skill_03");
        TurnbeforeSkill();
        minusMp(1200);

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_03"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            yield return null;
        }
        skillOn = false;
    }
    IEnumerator Skill_Q()
    {
        this.gameObject.GetComponent<SoundManager>().PlayAudio(0);
        skillOn = true;
        SkillCoolTimeSet(0);
        ActionStop();
        myAnim.Play("Skill_Q01");
        minusMp(300);

        TurnbeforeSkill();

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_Q02"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.90f)
        {
            yield return null;
        }
        skillOn = false;
    }
    IEnumerator Skill_W()
    {
        this.gameObject.GetComponent<SoundManager>().PlayAudio(1);
        skillOn = true;
        SkillCoolTimeSet(1);
        ActionStop();
        myAnim.Play("Skill_W");
        minusMp(500);

        TurnbeforeSkill();

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_W"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            yield return null;
        }
        StopMove();
        skillOn = false;
    }
    IEnumerator Skill_E()
    {
        this.gameObject.GetComponent<SoundManager>().PlayAudio(2);
        skillOn = true;
        SkillCoolTimeSet(2);
        ActionStop();
        myAnim.Play("Skill_E_Start");
        minusMp(700);

        TurnbeforeSkill();

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_E_End"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            yield return null;
        }
        skillOn = false;
    }
    IEnumerator Skill_R()
    {
        this.gameObject.GetComponent<SoundManager>().PlayAudio(3);
        skillOn = true;
        SkillCoolTimeSet(3);
        ActionStop();
        minusMp(1000);
        myAnim.Play("Skill_R_Start");

        TurnbeforeSkill();

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill_R_End"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.48f)
        {
            yield return null;
        }
        skillOn = false;
    }

    private void ActionStop()
    {
        myAnim.SetBool("Move", false);
        myEvent.GetComponent<AnimationEffect>().E_ComboReSet();
    }

    private void SkillCoolTimeSet(int num)
    {
        Skill[num] = false;
        UI.GetComponent<UIManager>().slotImage[num].GetComponent<SlotImage>().childText.SetActive(true);
    }

    private void minusMp(int num)
    {
        this.transform.GetComponent<PlayerInfo>().MP -= num;
    }

    public void TurnbeforeSkill()
    {

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {

            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }
    }

    public void StopMove()
    {
        myAnim.SetBool("Move", false);
        if (move != null) StopCoroutine(move);
    }

    public void Move(NavMeshPath path)
    {
        if (move != null) StopCoroutine(move);
        move = StartCoroutine(Moving(path));
    }



    IEnumerator Moving(NavMeshPath path)
    {
        int nextpos = 1;
        Vector3 dir = path.corners[nextpos] - path.corners[0];
        float dist = dir.magnitude;
        dir.Normalize();

        float rot = GameUtils.CalculateAngle(this.transform.forward, dir);
        float rotdir = rot >= 0.0f ? 1f : -1f;
        rot = Mathf.Abs(rot);


        while (nextpos < path.corners.Length)
        {
            myAnim.SetBool("Move", true);
            float delta = Time.deltaTime * MoveSpeed;
            if (dist - delta <= Mathf.Epsilon)
            {
                delta = dist;
                if (++nextpos == path.corners.Length)
                {
                    this.transform.Translate(dir * delta, Space.World);
                    myAnim.SetBool("Move", false);
                    continue;
                }

                dir = path.corners[nextpos] - path.corners[nextpos - 1];
                dist = dir.magnitude + delta;
                dir.Normalize();

                rot = GameUtils.CalculateAngle(this.transform.forward, dir);
                rotdir = rot >= 0.0f ? 1f : -1f;
                rot = Mathf.Abs(rot);

                continue;
            }
            dist -= delta;
            this.transform.Translate(dir * delta, Space.World);
            dir = path.corners[nextpos] - this.transform.position;
            dir.Normalize();
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }
        myAnim.SetBool("Move", false);
    }

    public void AirBoneFuntion()
    {
        skillOn = false;
        attackOn = false;
        AirBone = true;
        myAnim.SetBool("Move", false);
        if(SkillCoroutine != null) StopCoroutine(SkillCoroutine);
        if(move != null) StopCoroutine(move);
        myEvent.GetComponent<AnimationEffect>().underAttack = false;
        myEvent.GetComponent<AnimationEffect>().E_WeaponColorFalse();
        myEvent.GetComponent<AnimationEffect>().CameraShakeFalsd();
        myEvent.GetComponent<AnimationEffect>().CameraZoomOut();
        windmill = false;
        StartCoroutine(AirBoneCoroutine());
        
    }

    IEnumerator AirBoneCoroutine()
    {
        ActionStop();
        myAnim.Play("KnockDown_Start");

        TurnbeforeSkill();

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("KnockDown_End"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            yield return null;
        }
        AirBone = false;
    }
}
