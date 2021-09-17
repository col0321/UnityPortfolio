using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commander : MonoBehaviour
{
    private int page = 0;

    public GameObject markPosition;
    public GameObject nameUIPostion;
    public GameObject markPosition2;
    public GameObject nameUIPostion2;
    public GameObject Gkey;
    public GameObject Player;
    public GameObject TalkImage;
    public GameObject TalkContack;
    public GameObject TimeLine;
    public GameObject Mission_01;
    public GameObject Delete_01;
    public GameObject Delete_02;
    public GameObject Delete_03;
    public GameObject Mission_04;
    public GameObject MapObject_02;
    public GameObject[] obj;
    public int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num == 0)
        {
            markPosition.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 3, 0));
            nameUIPostion.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2.5f, 0));

            if (Vector3.Distance(this.transform.position, Player.transform.position) < 5f)
            {
                if (Gkey.activeSelf == false)
                    Gkey.SetActive(true);

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
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "드디어 오셧군요!!";

            if (page == 2)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "드디어 오셧군요!! \n현 상황 말씀이십니까?";

            if (page == 3)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "드디어 오셧군요!! \n현 상황 말씀이십니까? \n좋지 않습니다. 다리를 막는 몬스터들 때문에 마을로 진입하기가 힘듭니다.";
            if (page == 4)
            {
                TimeLine.SetActive(true);
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "반드시 마을진입에 성공해야합니다!";
            }
            if (page == 5)
            {
                TalkImage.SetActive(false);
                TimeLine.SetActive(false);
                Mission_01.SetActive(true);
            }

            if (Vector3.Distance(this.transform.position, Player.transform.position) > 82.0f)
            {
                Destroy(Delete_02.gameObject);
                Destroy(Delete_01.gameObject);
                Destroy(this.gameObject);
            }
        }
        if (num == 1)
        {
            markPosition2.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 3, 0));
            nameUIPostion2.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2.5f, 0));

            if (Vector3.Distance(this.transform.position, Player.transform.position) < 10f)
            {
                if (MapObject_02.activeSelf == false)
                    MapObject_02.SetActive(true);

                for(int i = 0; i< obj.Length; i++)
                {
                    if (obj[i].activeSelf == false)
                        obj[i].SetActive(true);
                }
            }
            if (Vector3.Distance(this.transform.position, Player.transform.position) < 5f)
            {


                if (Gkey.activeSelf == false)
                    Gkey.SetActive(true);

                if (Delete_03 != null)
                    Destroy(Delete_03);

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
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "감사합니다. 덕분에 마을 진입까지 성공했습니다.";

            if (page == 2)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "감사합니다. 덕분에 마을 진입까지 성공했습니다. \n다만...";

            if (page == 3)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "감사합니다. 덕분에 마을 진입까지 성공했습니다. \n다만... \n마을 곳곳에 몬스터와 마을 시장에 알수없는 포탈이 생겼습니다.";
            if (page == 4)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "!!..";
            if (page == 5)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "!!.. \n혼자 가시겠다는 말씀이십니까?";
            if (page == 6)
                TalkContack.GetComponent<TMPro.TextMeshProUGUI>().text = "!!.. \n혼자 가시겠다는 말씀이십니까? \n알겠습니다. 마을내에 몬스터 처리 후에 최대한 빨리 지원병을 이끌고 합류하겠습니다.";
            if (page == 7)
            {
                Mission_04.SetActive(true);
                TalkImage.SetActive(false);
            }
        }

    }
}
