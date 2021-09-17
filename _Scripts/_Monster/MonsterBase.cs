using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterBase : MonoBehaviour
{
    protected bool Hit =false;
    protected bool Fly =false;
    protected bool dieAnim = false;
    protected bool aroundMove;
    protected bool Endone = false;
    protected bool boss = false;
    protected bool isAttack = false;
    protected bool dissolveSet = false;
    protected float Playtime = 0f;
    protected int bossAttack = 0;
    protected Animator myAnim = null;
    protected GameObject myTarget = null;
    protected NavMeshPath path;

    public bool dieSelect = false;
    public float Speed;
    public float RotSpeed = 360.0f;
    public float Exp;
    public GameObject outline;
    public GameObject outline2; 

    Coroutine move = null;
    Coroutine HitCoroutine = null;
    Coroutine FlyCoroutine = null;
    Coroutine Attackcorutine01 = null;
    Coroutine Attackcorutine02 = null;

    public Transform myModel;
    public UIManager UI;
    public GameObject Player;
    public GameObject Text;
    public GameObject[] Item;

    private int hitNum = 0;
    private int hp;
    private int Maxhp;
    private bool Under70 = false;
    private bool Under70_Skill = false;
    private bool Under55 = false;
    private bool Under55_Skill = false;
    private bool Under30 = false;
    private bool Under30_Skill = false;
    private bool Under05 = false;
    private bool Under05_Skill = false;


    // Start is called before the first frame update

    //========================== Monster ================================//
    protected void Initialize(int temp)
    {
        myAnim = this.GetComponentInChildren<Animator>();
        path = new NavMeshPath();
        UI = GameObject.Find("UI").GetComponent<UIManager>();
        Player = GameObject.Find("Player");
        Maxhp = temp;
    }
    public void MoveToPosition(NavMeshPath path)
    {
        if (move != null) StopCoroutine(move);
        move = StartCoroutine(Moving(path));
    }
    public void MoveStop()
    {
        if (move != null) StopCoroutine(move);
    }
    public void Attack()
    {

        int num = Random.Range(0,2);
        num %= 2;

        if (Attackcorutine01 != null) StopCoroutine(Attackcorutine01); 
        if (Attackcorutine02 != null) StopCoroutine(Attackcorutine02); 

        if (num == 0)
            Attackcorutine01 = StartCoroutine(Attack01());
        if (num == 1)
            Attackcorutine02 = StartCoroutine(Attack02());

    }
    public void HitFuntion()
    {
        if (HitCoroutine != null) StopCoroutine(HitCoroutine);
        HitCoroutine = StartCoroutine(HitAni());
    }
    public void HitDuring()
    {
        if (HitCoroutine == null) HitCoroutine = StartCoroutine(HitAni());
    }
    public void StopHitFuntion()
    {
        if (HitCoroutine != null) StopCoroutine(HitCoroutine);
    }
    public void DieFuntion()
    {
        if (dieAnim == false)
            StartCoroutine(DieAni());
    }
    public void FlyFuntion()
    {
        if (FlyCoroutine != null) StopCoroutine(FlyCoroutine);
        FlyCoroutine = StartCoroutine(FlyAni());
    }
    IEnumerator DieAni()
    {

        int num = Random.Range(0, 3);
        if (boss == false)
        {
            GameObject obj;
            obj = Instantiate(Item[num], this.transform.position, Quaternion.identity, null);
            UI.GetComponent<UIManager>().ExpPlus(Exp);
            Player.GetComponent<PlayerInfo>().Exp += Exp;
        }
        MoveStop();
        StopHitFuntion();
        dieAnim = true;
        myAnim.SetBool("Move", false);
        myAnim.Play("Die");
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.70f)
        {
            yield return null;
        }
        outline.GetComponent<MonsterHit>().MaterialChage(2);
        dissolveSet = true;
    }
    IEnumerator FlyAni()
    {
        Fly = true;
        myAnim.Play("Airborne_Start");

        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Airborne_End"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.80f)
        {
            yield return null;
        }
        Fly = false;
    }
    IEnumerator HitAni()
    {
        Hit = true;
        hitNum++;
        hitNum %= 2;
        if(hitNum == 0)
            myAnim.Play("Hit 1");
        if(hitNum == 1)
            myAnim.Play("Hit");

        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.80f)
        {
            yield return null;
        }
        Hit = false;
    }
    IEnumerator Attack01()
    {
        isAttack = true;
        Playtime = 0;
        myAnim.Play("Attack_01");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            yield return null;
        }
        isAttack = false;
    }
    IEnumerator Attack02()
    {
        isAttack = true;
        Playtime = 0;
        myAnim.Play("Attack_02");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack_02"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator Moving(NavMeshPath path)
    {
        int nextpos = 1;
        if (path.corners[nextpos] != null)
        {
            Vector3 dir;
            dir = path.corners[nextpos] - path.corners[0];

            float dist = dir.magnitude;
            dir.Normalize();

            float rot = GameUtils.CalculateAngle(this.transform.forward, dir);
            float rotdir = rot >= 0.0f ? 1f : -1f;
            rot = Mathf.Abs(rot);


            while (nextpos < path.corners.Length)
            {
                myAnim.SetBool("Move", true);
                float delta = Time.deltaTime * (Speed * myAnim.GetFloat("AnimationSpeed"));
                if (dist - delta <= Mathf.Epsilon)
                {
                    delta = dist;
                    if (++nextpos == path.corners.Length)
                    {
                        this.transform.Translate(dir * delta, Space.World);
                        myAnim.SetBool("Move", false);
                        continue;
                    }

                    this.transform.Translate(dir * delta, Space.World);

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

                //delta = RotSpeed * Time.smoothDeltaTime * myAnim.GetFloat("AnimationSpeed");
                //if (rot != 0)
                //{
                //    if (rot - delta <= Mathf.Epsilon)
                //    {
                //        delta = rot;
                //        rot = 0;
                //    }
                //    rot -= delta;
                //    this.transform.Rotate(this.transform.up * delta * rotdir, Space.World);
                //}
                yield return null;
            }
            myAnim.SetBool("Move", false);
        }
        aroundMove = false;
    }
    //========================== Monster ================================//

    //========================== Boss ================================//

    protected void Boss(int temp)
    {
         BossAttack(temp);
    }

    private void ChackHp(int temp)
    {
        float CurrentHp = (float)(temp) / (float)(Maxhp);

        if (CurrentHp < 0.7f && Under70_Skill == false)
            Under70 = true;

        if (CurrentHp < 0.55f && Under55_Skill == false)
            Under55 = true;

        if (CurrentHp < 0.30f && Under30_Skill == false)
            Under30 = true;

        if (CurrentHp < 0.05f && Under05_Skill == false)
            Under05 = true;
    }

    private void BossAttack(int temp)
    {
        ChackHp(temp);
        if (Under70 == true && Under70_Skill == false)
        {
            Under70_Skill = true;
            StartCoroutine(BossPatten_05_Near_Far_Bomb());
            return;
        }

        if (Under55 == true && Under55_Skill == false)
        {
            Under55_Skill = true;
            StartCoroutine(MeteorFollow());
            return;
        }

        if (Under30 == true && Under30_Skill == false)
        {
            Under30_Skill = true;
            StartCoroutine(MeteorRandom());
            return;
        }

        if (Under05 == true && Under05_Skill == false)
        {
            Under05_Skill = true;
            StartCoroutine(EndOne());
            return;
        }





        // =================================== // 베이스 기본 공격
        int BaseAttack = Random.Range(0, 4);
        if(Under70_Skill == true)
            BaseAttack = Random.Range(0, 8);

        switch (BaseAttack)
        {
            case 0:
                StartCoroutine(BossPatten_01_Left_Attack());
                break;
            case 1:
                StartCoroutine(BossPatten_02_Right_Attack());
                break;
            case 2:
                StartCoroutine(BossPatten_03_Left_Fan_Attack());
                break;
            case 3:
                StartCoroutine(BossPatten_04_Right_Fan_Attack());
                break;
            default:
                StartCoroutine(BossPatten_06_Near_Far_Bomb_TwoCircle());
                break;
        }

    }

    IEnumerator BossPatten_01_Left_Attack()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Normal_Left_Attack");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Normal_Left_Attack"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator BossPatten_02_Right_Attack()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Normal_Right_Attack");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Normal_Right_Attack"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator BossPatten_03_Left_Fan_Attack()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Normal_Left_Attack_Fan");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Normal_Left_Attack_Fan"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator BossPatten_04_Right_Fan_Attack()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Normal_Right_Attack_Fan");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Normal_Right_Attack_Fan"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator BossPatten_05_Near_Far_Bomb()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Revive_Skill_01");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Typhon_Skill01_End"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator BossPatten_06_Near_Far_Bomb_TwoCircle()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("Skill02");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill02"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator MeteorFollow()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("MeteorFollow");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("MeteorFollow"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator MeteorRandom()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("MeteorRandom");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("MeteorRandom"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;

    }
    IEnumerator EndOne()
    {
        isAttack = true;
        myAnim.SetBool("Move", true);


        Vector3 dir;
        dir = Player.transform.position - this.transform.position;
        dir.Normalize();
        float temp = 0.0f;

        Playtime = 0;

        while (temp <= 0.99f)
        {
            temp = Vector3.Dot(this.transform.forward, dir);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 5.0f * Time.deltaTime);
            yield return null;
        }

        myAnim.SetBool("Move", false);
        myAnim.SetTrigger("EndOne");
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Skill02_Fire"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        isAttack = false;
        Endone = true;
        myAnim.SetBool("Die", true);
    }

    //========================== Boss ================================//
}
