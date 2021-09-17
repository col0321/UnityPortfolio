using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    public GameObject markPosition;
    public GameObject nameUIPostion;
    public GameObject Gkey;
    public GameObject Player;
    public GameObject TalkImage;
    public GameObject TalkContack;
    public GameObject myCamera;
    public GameObject dissolve1;
    public GameObject dissolve2;
    public GameObject Delete_01;
    public GameObject Delete_02;
    public GameObject Delete_03;
    public GameObject Delete_04;
    public GameObject Delete_05;
    public GameObject Mission_02;
    public GameObject enemyManager;

    private int page = 0;
    private bool dissolve;
    private float temp = -1.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        markPosition.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 3, 0));
        nameUIPostion.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2.5f, 0));

        if (Vector3.Distance(this.transform.position, Player.transform.position) < 5f)
        {
            if (Gkey.activeSelf == false)
                Gkey.SetActive(true);

            if (Delete_05 != null)
                Destroy(Delete_05);

            Gkey.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0, 2.5f, 0));
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (TalkImage.activeSelf == false)
                    TalkImage.SetActive(true);
                page++;
            }
        }
        else
        {
            if (Gkey.activeSelf == true)
                Gkey.SetActive(false);
        }
        if (page == 1)
        {
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "...?";
        }

        if (page == 2)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "...? \n여기는 위험합니다. 어서 몸을 피하세요....";

        if (page == 3)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "...? \n여기는 위험합니다. 어서 몸을 피하세요....\n....... 그렇군요. 크로우즈가...";

        if (page == 4)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "여기는 곳 몬스터가 들이 닥칠겁니다.";

        if (page == 5)
        {
            myCamera.GetComponent<CameraManager>().StartShakeOnly(0.025f);
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "!!";
        }

        if (page == 6)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "어서빨리 몸을 피하세요!";

        if (page == 7)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "어서빨리 몸을 피하세요!\n저는 걱정하지마세요. 그럼 먼저 떠나겠습니다";

        if (page == 8)
        {
            TalkImage.SetActive(false);
            Gkey.SetActive(false);
            dissolve = true;
        }


        if (dissolve)
        {

            temp += Time.deltaTime * 0.3f;
            dissolve1.GetComponent<Renderer>().material.SetFloat("Dissovle", temp);
            dissolve2.GetComponent<Renderer>().material.SetFloat("Dissovle", temp);

            if (temp > 0.5f)
            {
                Mission_02.SetActive(true);
                enemyManager.GetComponent<EnemyManager>().waveOn = true;
                Destroy(Delete_01.gameObject);
                Destroy(Delete_02.gameObject);
                Destroy(Delete_03.gameObject);
                Destroy(Delete_04.gameObject);
                Destroy(this.gameObject);
                myCamera.GetComponent<CameraManager>().EndShakeOnly();
            }
        }
    }
}
