using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : AnimationEventUtill
{

    //기본공격 관련 // 
    public Transform basicPosition;
    public Transform skillDPosition;
    public AnimationEffect an;

    // ============= 전함수 공용 ============= //
    private bool airBone = false;
    public LayerMask Monster;
    public LayerMask Well;


    public void H_AirBone(int num)
    {
        if (num == 0)
            airBone = false;
        else if (num == 1)
            airBone = true;
    }

    public void H_3_1(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(basicPosition.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 1.5f), (int)(damage * 2.0f));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 1.5f), (int)(damage * 2.0f));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_3_2()
    {
        Collider[] hitColliders = Physics.OverlapBox(skillDPosition.position,new Vector3(1,1,5), skillDPosition.rotation);

        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower_E;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (3.5f + (10.0f * dust))), (int)(damage * (4.0f + (10.0f * dust))));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower_E;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (3.5f + (10.0f * dust))), (int)(damage * (4.0f + (10.0f * dust))));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_2()
    {
        Collider[] hitColliders = Physics.OverlapBox(basicPosition.position, new Vector3(0.5f, 1, 2), skillDPosition.rotation);

        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower_E;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 4.5f), (int)(damage * 5.0f));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower_E;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 4.5f), (int)(damage * 5.0f));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_1_1(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(basicPosition.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (1.5f + (3.0f * dust))), (int)(damage * (2.0f + (3.0f * dust))));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (1.5f + (3.0f * dust))), (int)(damage * (2.0f + (3.0f * dust))));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_1_2(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(basicPosition.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (3.5f + (10.0f * dust))), (int)(damage * ( 4.0f + (10.0f * dust))));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float dust = an.GetComponent<AnimationEffect>().gaugePower;
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * (3.5f + (10.0f * dust))), (int)(damage * (4.0f + (10.0f * dust))));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_Q(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(basicPosition.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 2.5f), (int)(damage * 3.0f));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 2.5f), (int)(damage * 3.0f));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_W(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 1.2f), (int)(damage * 1.5f));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 1.2f), (int)(damage * 1.5f));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }

    public void H_BasicAttackCheck(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(basicPosition.position, radius);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Monster & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 0.9f), (int)(damage * 1.1f));
                    hitColliders[i].GetComponent<MonsterAi>().GetDamage(rand, airBone, false);
                    camera.AwhileShake(0.2f);
                    if (hitColliders[i].GetComponent<MonsterAi>().myType == MonsterAi.TYPE.BOSS)
                    {
                        break;
                    }
                }
                else if ((Well & temp) == temp)
                {
                    float damage = PlayerControl.GetComponent<PlayerInfo>().OffensePower;
                    int rand = Random.Range((int)(damage * 0.9f), (int)(damage * 1.1f));
                    hitColliders[i].GetComponent<Well>().GetDamage(rand);
                    camera.AwhileShake(0.2f);
                }
                i++;
            }
        }

    }
}
