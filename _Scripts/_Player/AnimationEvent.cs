using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : AnimationEventUtill
{
    public EnemyManager enemyManager;
    //====================================================================================//
    //================================ 스킬 관련 ==================================//
    public AudioClip[] Ac;

    private AudioSource Ad;

    private void Awake()
    {
        Ad = this.gameObject.GetComponent<AudioSource>();
    }

    public void Audio(string str)
    {
        if (str == "D_1")
            Ad.clip = Ac[0];
        if (str == "D_2")
            Ad.clip = Ac[1];
        if (str == "Q_1")
            Ad.clip = Ac[2];
        if (str == "Q_2")
            Ad.clip = Ac[3];

        Ad.Play();
    }


    public void A_SkillWMoveSet(int num)
    {

        num %= 2;

        if (num == 0)
        {
            PlayerControl.GetComponent<PlayerControl>().windmill = false;
            PlayerControl.GetComponent<PlayerControl>().StopMove();
        }
        if (num == 1)
            PlayerControl.GetComponent<PlayerControl>().windmill = true;
    }
}
