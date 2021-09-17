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
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "...? \n����� �����մϴ�. � ���� ���ϼ���....";

        if (page == 3)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "...? \n����� �����մϴ�. � ���� ���ϼ���....\n....... �׷�����. ũ�ο��...";

        if (page == 4)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "����� �� ���Ͱ� ���� ��ĥ�̴ϴ�.";

        if (page == 5)
        {
            myCamera.GetComponent<CameraManager>().StartShakeOnly(0.025f);
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "!!";
        }

        if (page == 6)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "����� ���� ���ϼ���!";

        if (page == 7)
            TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "����� ���� ���ϼ���!\n���� ��������������. �׷� ���� �����ڽ��ϴ�";

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
