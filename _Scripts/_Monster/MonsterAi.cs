using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonsterBase
{
    //======================================//
    //private :
    private float dissolveValue = -1.0f;
    private GameObject HpbarObj = null;
    private Vector3 ChainPos;
    private float IdleMoveTime = 0.0f;
    private RangeSystem myRange;
    //======================================//
    //======================================//
    //public : 
    public float distance;
    public Animator myAni;
    public float attackTime;
    public LayerMask mySelf;
    public GameObject burnParticle;
    public bool chainMove = false;
    public GameObject[] chainObj;
    public GameObject text;
    public GameObject Hpbar;
    public Vector2 minMaxX;
    public Vector2 minMaxY;
    public Vector2 HpbarXYVertex;
    public LayerMask Ground;
    public float setMoveTime;

    private ChatSystem chatSystemDamage;
    //======================================//
    //INFO
    public int hp;
    private int HeadHp;
    public int GetHeadHp(){return HeadHp;}
    //======================================//
    //FSM
    public enum STATE
    {
        IDLE, MOVE, HIT, AIRBONE , BATTLEMOVE , BATTLE ,DIE ,CHAIN
    }
    public STATE myState = STATE.IDLE;

    public enum TYPE
    {
        NONE,BOSS
    }public TYPE myType;

    Vector3 StartPos = Vector3.zero;

    public float AttackRange;

    public bool waitState = false;

    //======================================//
    Coroutine Wait;
    Coroutine Magnet;
    private void Start()
    {
        myRange = this.gameObject.GetComponentInChildren<RangeSystem>();
        chatSystemDamage = GameObject.Find("ChatSystem").GetComponent<ChatSystem>();
        outline.GetComponent<Outline>().enabled = false;
        HeadHp = hp;
        Initialize(HeadHp);
        if(myType == TYPE.BOSS)
            myTarget = Player;
    }

    private void Update()
    {
        StateUpdate();
        OutLiner();
    }


    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.IDLE:
                MoveStop();
                Playtime = 0.0f;
                break;
            case STATE.MOVE:
                if (!waitState)
                {
                    float x = Random.Range(-5.0f, 5.0f) + this.transform.position.x;
                    float z = Random.Range(-5.0f, 5.0f) + this.transform.position.z;
                    if (NavMesh.CalculatePath(this.transform.position, new Vector3(x, this.transform.position.y, z), NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            aroundMove = true;
                            MoveToPosition(path);
                        }
                    }
                }
                break;
            case STATE.HIT:
                if(myTarget != null)
                    this.transform.LookAt(myTarget.transform);
                MoveStop();
                HitFuntion();
                break;
            case STATE.AIRBONE:
                if (myTarget != null)
                    this.transform.LookAt(myTarget.transform);
                MoveStop();
                StopHitFuntion();
                FlyFuntion();
                break;
            case STATE.BATTLEMOVE:
                MoveStop();
                myAni.SetBool("Move", false);
                break;
            case STATE.BATTLE:
                MoveStop();
                myAni.SetBool("Move", false);
                break;
            case STATE.CHAIN:
                HitFuntion();
                MoveStop();
                for (int i = 0; i < 4; i++)
                {
                    chainObj[i].SetActive(true);
                }
                break;
            case STATE.DIE:
                break;

        }
    }


    void StateUpdate()
    {
        switch (myState)
        {
            case STATE.IDLE:
                SeachPlyer();
                IdleMoveTime += 1.0f * Time.deltaTime;
                if (IdleMoveTime > setMoveTime)
                {
                    IdleMoveTime = 0.0f;
                    ChangeState(STATE.MOVE);
                }
                break;
            case STATE.MOVE:
                SeachPlyer();
                if (!aroundMove)
                    ChangeState(STATE.IDLE);
                break;
            case STATE.BATTLEMOVE:
                if (!isAttack)
                {
                    if (NavMesh.CalculatePath(this.transform.position, myTarget.transform.position, NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            MoveToPosition(path);
                        }
                    }

                    if (Vector3.Distance(myTarget.transform.position, this.transform.position) <= AttackRange)
                    {
                        ChangeState(STATE.BATTLE);
                    }
                }
                break;


            case STATE.HIT:
                if(Hit == false)
                    ChangeState(STATE.BATTLE);
                if (myTarget == null)
                    myTarget = Player;
                break;

            case STATE.AIRBONE:
                if (Fly == false)
                    ChangeState(STATE.BATTLE);

                break;

            case STATE.BATTLE:
                if (myType == TYPE.NONE)
                {
                    if (myTarget == null)
                        SeachPlyer();

                    else
                    {
                        if (Vector3.Distance(myTarget.transform.position, this.transform.position) >= AttackRange && isAttack == false)
                        {
                            ChangeState(STATE.BATTLEMOVE);
                        }

                        if (Playtime > attackTime && Vector3.Distance(myTarget.transform.position, this.transform.position) <= AttackRange)
                        {
                            this.transform.LookAt(myTarget.transform);
                            Attack();
                        }
                    }
                }
                else if (myType == TYPE.BOSS)
                {
                    boss = true;
                    if (Vector3.Distance(myTarget.transform.position, this.transform.position) >= AttackRange && isAttack == false)
                    {
                        ChangeState(STATE.BATTLEMOVE);
                    }

                    if ((Playtime > attackTime && Vector3.Distance(myTarget.transform.position, this.transform.position) <= AttackRange))
                    {
                        Boss(hp);
                    }
                }
                break;

            case STATE.CHAIN:
                HitDuring();
                break;

            case STATE.DIE:
                if(HpbarObj != null)
                    Destroy(HpbarObj);
                DieFuntion();
                break;
        }

        if (Endone == true)
        {
            myAni.SetBool("Die", true);
            DieFuntion();
        }
        if (!isAttack)
            Playtime += 1.0f * Time.deltaTime * myAni.GetFloat("AnimationSpeed");

        InfoUpdate();

        if (dissolveSet)
            DissolveSet();

        if (hp < 0)
            hp = 0;


        Vector3 temp = this.transform.position;
        temp.y += 0.1f;

        Ray ray = new Ray();
        ray.origin = temp;
        ray.direction = -Vector3.up;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, Ground))
        {
            this.transform.position = hit.point;
        }
    }

    void DissolveSet()
    {
        outline.GetComponent<MonsterHit>().MaterialChage(2);
        MoveStop();
        StopHitFuntion();
        DieFuntion();
        burnParticle.SetActive(true);
        dissolveValue += Time.deltaTime * 0.3f;
        outline.GetComponent<Renderer>().material.SetFloat("Dissovle", dissolveValue);
        if (dissolveValue > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void InfoUpdate()
    {
        if (hp <= 0)
        {
            this.gameObject.layer = 0;
            ChangeState(STATE.DIE);
            dieSelect = true;
        }
    }

    void SeachPlyer()
    {
        myTarget = myRange.FindTarget();
        if (myTarget != null)
        {
            MoveStop();
            ChangeState(STATE.BATTLEMOVE);
        }
    }

    public void FinishChain()
    {
        for (int i = 0; i < 4; i++)
        {
            chainObj[i].SetActive(false);
        }
        ChangeState(STATE.HIT);
    }

    public void GetDamage(int damage, bool AirBone, bool skill)
    {
        if (myState != STATE.DIE)
        {
            AudioSource ad = this.gameObject.GetComponent<AudioSource>();

            if (ad != null)
                ad.Play();

            chatSystemDamage.AddChatStringDamage(damage);
            hp -= damage;
            CreateDamageText(damage);
            if (myType == TYPE.NONE)
            {
                watHitFuntion(skill);
                if (AirBone)
                {
                    ChangeState(STATE.AIRBONE);
                }
                else
                {
                    if (myState != STATE.AIRBONE)
                    {
                        if (myState != STATE.HIT)
                            ChangeState(STATE.HIT);
                        else
                            HitFuntion();
                    }
                }

                if (HpbarObj == null && myType == TYPE.NONE)
                {
                    HpbarObj = Instantiate(Hpbar);
                    HpbarObj.GetComponent<MonsterHpBarUI>().Obj = this.transform.gameObject;
                    HpbarObj.GetComponent<MonsterHpBarUI>().xyVertex = HpbarXYVertex;
                    HpbarObj.GetComponent<MonsterHpBarUI>().init();
                }
            }
        }
    }

    private void CreateDamageText(int Damage)
    {
        GameObject obj;
        obj = Instantiate(text);
        obj.GetComponent<TMPro.TextMeshProUGUI>().text = Damage.ToString();
        obj.GetComponent<DamageUI>().Obj = this.transform.gameObject;
        obj.GetComponent<DamageUI>().minMaxX = minMaxX;        
        obj.GetComponent<DamageUI>().minMaxY = minMaxY;        
        obj.GetComponent<DamageUI>().Init();        
    }

    private void watHitFuntion(bool skill)
    {
        if (Wait != null) StopCoroutine(Wait);
        Wait = StartCoroutine(wait(skill));
        
    }
    public void AnimationSetSpeed(float speed)
    {
        myAni.SetFloat("AnimationSpeed", speed);
    }

    IEnumerator wait(bool skill)
    {
        if (skill == false)
        {
            yield return new WaitForSeconds(0.1f);
            myAni.SetFloat("AnimationSpeed", 0);
            outline.GetComponent<MonsterHit>().MaterialChage(1);
            myAni.SetFloat("AnimationSpeed", 0.0f);

            yield return new WaitForSeconds(0.15f);
            outline.GetComponent<MonsterHit>().MaterialChage(0);
            myAni.SetFloat("AnimationSpeed", 1);
        }
        else
        {
      
             StopHitFuntion();
             FlyFuntion();
             outline.GetComponent<MonsterHit>().MaterialChage(1);
             yield return new WaitForSeconds(0.15f);
             outline.GetComponent<MonsterHit>().MaterialChage(0);
        }

    }


    public void OutlineEnable(bool temp)
    {
        outline.GetComponent<Outline>().enabled = temp;
    }

    private void OutLiner()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, mySelf))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                if (dieSelect == false)
                    OutlineEnable(true);
                else
                    OutlineEnable(false);
            }
        }
        else
            OutlineEnable(false);
    }

}
