using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    //======================== Public  ========================//
    public Transform LeftHitPoint;
    public Transform RightHitPoint;
    public Transform LeftFanHitPoint;
    public Transform RightFanHitPoint;
    public GameObject playerObj;
    public LayerMask Player;
    public CameraManager cm;
    public GameObject Stone;
    public GameObject LeftStone;
    public GameObject RightStone;
    public GameObject Smoke;
    public GameObject Revive_01_Text;
    public GameObject Revive_02_Text;
    public GameObject Revive_03_Text;
    public GameObject Revive_04_Text;
    public GameObject NearBomb;
    public GameObject FarBomb;
    public GameObject FarBomb2;
    public GameObject Meteor;
    public GameObject Well;
    public GameObject Fire;
    public AudioClip[] Clip;

    //======================== Private ========================//
    private GameObject BossUI;
    private AudioSource Ad;

    //======================== Function =======================//
    public void cmZoomInSpeed(float f) { cm.zoomSpeed = f; }
    public void cmZoomIn(float f) { cm.ZoomIn(f); }
    public void cmZoomOut() { cm.ZoomOut(); }
    public void cmShakeTrue(float f) { cm.StartShakeOnly(f); }
    public void cmShakeFalsd() { cm.EndShakeOnly(); }
    public void cmDfStart(float f) { cm.StartDf(f); }
    public void cmDfEnd() { cm.EndDf(); }

    public void Awake()
    {
        playerObj = GameObject.Find("Player");
        cm = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        BossUI = GameObject.Find("BossUI_EndGame");
        Ad = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Revive_01_Text = GameObject.Find("Patton_05_Text");
        Revive_02_Text = GameObject.Find("Patton_07_Text");
        Revive_03_Text = GameObject.Find("Patton_08_Text");
        Revive_04_Text = GameObject.Find("Patton_09_Text");
    }

    public void Normal_Left(GameObject obj)
    {
        Collider[] hitColliders = Physics.OverlapSphere(LeftHitPoint.position, 3.5f);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Player & temp) == temp)
                {
                    float num = Random.Range(500.0f, 600.0f);
                    hitColliders[i].transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
                    cm.AwhileShake(0.2f);
                }
                i++;
            }
        }
        GameObject temp_obj;
        temp_obj = Instantiate(obj, LeftHitPoint.position,this.transform.rotation,this.transform);
    }
    public void Normal_Right(GameObject obj)
    {
        Collider[] hitColliders = Physics.OverlapSphere(RightHitPoint.position, 3.5f);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Player & temp) == temp)
                {
                    float num = Random.Range(500.0f, 600.0f);
                    hitColliders[i].transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
                }
                i++;
            }
        }
        GameObject temp_obj;
        temp_obj = Instantiate(obj, LeftHitPoint.position, this.transform.rotation, this.transform);
    }
    public void Normal_Left_Fun(GameObject obj)
    {
        Vector3 dust = playerObj.transform.position - this.transform.position;
        dust.Normalize();
        float temp = Vector3.Dot((-this.transform.right), dust);

        if (temp >= 0 && Vector3.Distance(this.transform.position, playerObj.transform.position) < 6)
        {
            float num = Random.Range(1200.0f, 1500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
        }
        GameObject temp_obj;
        temp_obj = Instantiate(obj, this.transform.position, this.transform.rotation, this.transform);

    }
    public void Normal_Right_Fun(GameObject obj)
    {
        Vector3 dust = playerObj.transform.position - this.transform.position;
        dust.Normalize();
        float temp = Vector3.Dot(this.transform.right, dust);

        if (temp >= 0 && Vector3.Distance(this.transform.position, playerObj.transform.position) < 6)
        {
            float num = Random.Range(1200.0f, 1500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
        }
        GameObject temp_obj;
        temp_obj = Instantiate(obj, this.transform.position, this.transform.rotation, this.transform);

    }
    public void Effect(string str)
    {
        if (str == "Left_Stone")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(Stone, LeftHitPoint.position, LeftHitPoint.rotation, this.transform);
            cm.AwhileShake(0.2f);
            Ad.clip = Clip[0];
            Ad.Play();
            return;
        }
        if (str == "Right_Stone")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(Stone, RightHitPoint.position, RightHitPoint.rotation, this.transform);
            cm.AwhileShake(0.2f);
            Ad.clip = Clip[0];
            Ad.Play();
            return;

        }
        else if (str == "Left_Stone_Fun")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(LeftStone, LeftFanHitPoint.position, LeftFanHitPoint.rotation, this.transform);
            cm.AwhileShake(0.3f);
            Ad.clip = Clip[1];
            Ad.Play();
            return;
        }
        else if (str == "Right_Stone_Fun")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(RightStone, RightFanHitPoint.position, RightFanHitPoint.rotation, this.transform);
            cm.AwhileShake(0.3f);
            Ad.clip = Clip[1];
            Ad.Play();
            return;
        }
        else if (str == "Smoke")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(Smoke, this.transform.position, Smoke.transform.rotation, this.transform);
        }
        else if (str == "Near_Bomb")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(NearBomb, this.transform.position, this.transform.rotation, null);
        }
        else if (str == "Far_Bomb")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(FarBomb, this.transform.position, this.transform.rotation, null);
        }
        else if (str == "Far_Bomb2")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(FarBomb2, this.transform.position, this.transform.rotation, null);
        }
        else if (str == "MeteorStrike")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(Meteor, playerObj.transform.position, Quaternion.identity, null);
        }
        else if (str == "Fire")
        {
            GameObject temp_obj;
            Vector3 temp = this.transform.position;
            temp.y = this.transform.position.y + 1.5f;
            temp_obj = Instantiate(Fire,temp , Quaternion.identity, null);
        }
        else if (str == "Well")
        {
            GameObject temp_obj;
            temp_obj = Instantiate(Well, this.transform.position, Quaternion.identity, null);
            playerObj.GetComponent<PlayerMovement>().cage = true;
            playerObj.GetComponent<PlayerMovement>().bossPos = this.transform.position;
        }
        else if (str == "Random")
        {
            Vector3 temp = playerObj.transform.position;
            temp.x = Random.Range(temp.x-10f, temp.x + 10f);
            temp.z = Random.Range(temp.z- 10f, temp.z+10f);
            GameObject temp_obj;
            temp_obj = Instantiate(Meteor, temp, Quaternion.identity, null);
            temp = playerObj.transform.position;
            temp.x = Random.Range(temp.x - 10f, temp.x + 10f);
            temp.z = Random.Range(temp.z - 10f, temp.z + 10f);
            temp_obj = Instantiate(Meteor, temp, Quaternion.identity, null);
            temp = playerObj.transform.position;
            temp.x = Random.Range(temp.x - 10f, temp.x + 10f);
            temp.z = Random.Range(temp.z - 10f, temp.z + 10f);
            temp_obj = Instantiate(Meteor, temp, Quaternion.identity, null);
            temp = playerObj.transform.position;
            temp.x = Random.Range(temp.x - 10f, temp.x + 10f);
            temp.z = Random.Range(temp.z - 10f, temp.z + 10f);
            temp_obj = Instantiate(Meteor, temp, Quaternion.identity, null);
        }

    }
    public void Revive(int num)
    {
        if (num == 1)
            Revive_01_Text.GetComponent<BossTextSet>().Seton = true;
        if (num == 3)
            Revive_02_Text.GetComponent<BossTextSet>().Seton = true;
        if (num == 5)
            Revive_03_Text.GetComponent<BossTextSet>().Seton = true;
        if (num == 7)
            Revive_04_Text.GetComponent<BossTextSet>().Seton = true;
    }
    public void Revive_Skill01_Near_Bomb(GameObject obj)
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) <= 7)
        {
            float num = Random.Range(1700.0f, 2500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().AirBoneChagne();            
            cm.AwhileShake(0.2f);
        }
        Ad.clip = Clip[2];
        Ad.Play();
    }
    public void Revive_Skill01_Far_Bomb(GameObject obj)
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) >= 7 && Vector3.Distance(this.transform.position, playerObj.transform.position) < 8.1)
        {
            float num = Random.Range(1700.0f, 2500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().AirBoneChagne();
            cm.AwhileShake(0.2f);
        }
        Ad.clip = Clip[2];
        Ad.Play();
    }
    public void Near_Far_Two()
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) <= 7)
        {
            float num = Random.Range(1700.0f, 2500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().AirBoneChagne();
            cm.AwhileShake(0.2f);
        }

        if (Vector3.Distance(this.transform.position, playerObj.transform.position) >= 9 && Vector3.Distance(this.transform.position, playerObj.transform.position) <= 12)
        {
            float num = Random.Range(1700.0f, 2500.0f);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
            playerObj.transform.gameObject.GetComponent<PlayerControl>().AirBoneChagne();
            cm.AwhileShake(0.2f);
        }
        Ad.clip = Clip[2];
        Ad.Play();
    }

    public void Fire_Damage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 12);
        int i = 0;
        if (hitColliders.Length != 0)
        {
            while (i < hitColliders.Length)
            {
                int temp = 1 << hitColliders[i].gameObject.layer;
                if ((Player & temp) == temp)
                {
                    float num = Random.Range(1500, 1800);
                    hitColliders[i].transform.gameObject.GetComponent<PlayerControl>().GetDamage(num);
                   
                }
                i++;
            }
        }
        cm.AwhileShake(0.2f);
    }
}
