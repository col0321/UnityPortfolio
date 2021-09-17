using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : PlayerMovement
{
    //======================================//
    //private :

    //======================================//
    //======================================//
    //public : 
    public LayerMask LayerMove;
    public LayerMask Monster;
    public GameObject Mark;
    public GameObject[] hit;
    public bool GetHight = true;
    //======================================//
    //======================================//
    //protected : 
    //======================================//




    Coroutine Hit;
    public enum STATE
    {
        IDLE, MOVE, BATTLE,AIRBONE, BATTLEMOVE, DIE
    }
    public STATE myState = STATE.IDLE;

    void Start()
    {
        path = new NavMeshPath();
        Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        StateUpdate();
        UpdateMovement();
        Debug.Log(windmill);
    }

    protected void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.IDLE:
                myAnim.SetBool("Move", false);
                break;
            case STATE.AIRBONE:
                AirBoneFuntion();
                break;

            case STATE.MOVE:

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, LayerMove))
                {
                    if (NavMesh.CalculatePath(this.transform.position, hit.point, NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            Move(path);
                        }
                        if (path.status == NavMeshPathStatus.PathPartial)
                        {

                        }
                    }
                }

                break;
            case STATE.BATTLE:
                myAnim.SetBool("Move", false);
                StopMove();
                break;
        }
    }

    void StateUpdate()
    {
        switch (myState)
        {
            case STATE.IDLE:

                if (Input.GetMouseButtonDown(1))
                    ChangeState(STATE.MOVE);

                if (skillOn || attackOn)
                    ChangeState(STATE.BATTLE);

                break;
            case STATE.MOVE:

                if (myAnim.GetBool("Move") == false)
                    ChangeState(STATE.IDLE);

                if (skillOn || attackOn)
                {
                    StopMove();
                    ChangeState(STATE.BATTLE);
                }
                break;

            case STATE.AIRBONE:

                if (AirBone == false)
                    ChangeState(STATE.IDLE);

                break;


            case STATE.BATTLEMOVE:
                if (Vector3.Distance(this.transform.position, target.transform.position) < target.GetComponentInParent<MonsterAi>().distance)
                    ChangeState(STATE.BATTLE);
                break;

            case STATE.BATTLE:
                if (skillOn == false && attackOn == false)
                    ChangeState(STATE.IDLE);
                break;
        }
        MoveClick();
        BattleMode();

        if (GetHight)
        {
            Vector3 temp = this.transform.position;
            temp.y += 1f;

            Ray ray = new Ray();
            ray.origin = temp;
            ray.direction = -Vector3.up;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f, Ground))
            {
                temp = this.transform.position;
                temp.y = hit.point.y;
                this.transform.position = temp;
            }
        }

    }

    private void ResetPosition()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }

    public void MoveClick()
    {
        if (myState != STATE.AIRBONE)
        {
            if (myState == STATE.BATTLE)
            {
                if (windmill)
                    MouseRight();
                else if (attackOn)
                    MouseLeft();
            }
            else
            {
                MouseRight();
                MouseLeft();
            }
        }
    }

    private void MouseRight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMove))
            {
                if (NavMesh.CalculatePath(this.transform.position, hit.point, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        Move(path);
                        if (myState == STATE.BATTLEMOVE)
                            myState = STATE.MOVE;
                    }
                    GameObject obj;
                    obj = Instantiate(Mark, hit.point, Mark.transform.rotation, null);
                }
            }
        }
    }
    private void MouseLeft()
    {
        if (!invenOnOff)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 1000.0f, Monster))
                {
                    target = hit.transform.gameObject;
                    if (Vector3.Distance(this.transform.position, target.transform.position) > target.GetComponentInParent<MonsterAi>().distance)
                    {
                        if (!attackOn)
                        {

                            if (NavMesh.CalculatePath(this.transform.position, hit.point, NavMesh.AllAreas, path))
                            {
                                if (path.status == NavMeshPathStatus.PathComplete)
                                {
                                    target = hit.transform.gameObject;
                                    Move(path);
                                    ChangeState(STATE.BATTLEMOVE);
                                }
                            }

                        }
                    }
                    else
                        myEvent.GetComponent<AnimationEffect>().E_BasicAttack();
                }
                else
                {
                    ChangeState(STATE.BATTLE);
                    myEvent.GetComponent<AnimationEffect>().E_BasicAttack();
                }
            }
        }
    }

    public void AirBoneChagne()
    {
        if (myState != STATE.AIRBONE)
        {
            ChangeState(STATE.AIRBONE);
        }
    }

    public void GetDamage(float num)
    {
        this.transform.GetComponent<PlayerInfo>().HP -= num;
        if (Hit != null) StopCoroutine(Hit);
        Hit = StartCoroutine(HitFuntion());
    }

    IEnumerator HitFuntion()
    {
        hit[0].GetComponent<PlayerHit>().MaterialChage(1);
        hit[1].GetComponent<PlayerHit>().MaterialChage(1);
        
        yield return new WaitForSeconds(0.2f);

        hit[0].GetComponent<PlayerHit>().MaterialChage(0);
        hit[1].GetComponent<PlayerHit>().MaterialChage(0);

    }
}
