using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject M1;
    public GameObject M2;
    public GameObject M3;
    public GameObject M4;
    public LayerMask Monster;
    public GameObject Mission_02;
    public GameObject Mission_03;
    public GameObject MonsterInfo;
    public GameObject WavePoint;
    public GameObject Player;
    public bool waveOn = false;
    public int waveCount;

    private float time = 0.0f;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, Monster))
        {
            GameObject temp = hit.transform.parent.gameObject;
            foreach (Transform child in transform)
            {
                if (temp == child.transform.gameObject)
                {
                    if (MonsterInfo.activeSelf == false)
                        MonsterInfo.SetActive(true);

                    MonsterInfo.GetComponent<SlotImage>().childImage.GetComponent<Slider>().value = (float)(temp.transform.gameObject.GetComponent<MonsterAi>().hp) / (float)(temp.transform.gameObject.GetComponent<MonsterAi>().GetHeadHp());
                    MonsterInfo.GetComponent<SlotImage>().childText.GetComponent<TMPro.TextMeshProUGUI>().text = temp.transform.gameObject.GetComponent<MonsterAi>().hp.ToString() + " / " + temp.transform.gameObject.GetComponent<MonsterAi>().GetHeadHp().ToString();
                    return;
                }
            }

        }
        else if (MonsterInfo.activeSelf == true)
            MonsterInfo.SetActive(false);


        if (waveOn == true && waveCount < 4)
        {
            GameObject temp;
            Vector3 dust;
            time += 1.0f * Time.deltaTime;
            if (time > 0 && waveCount == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M1, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 5; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M2, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 4; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M3, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 2; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M4, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }

                waveCount++;
            }

            if (time > 3 && waveCount == 1)
            {
                for (int i = 0; i < 15; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M1, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 5; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M2, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 4; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M3, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 2; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M4, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }

                waveCount++;
            }

            if (time > 6 && waveCount == 2)
            {
                for (int i = 0; i < 15; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M1, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 5; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M2, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 4; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M3, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }
                for (int i = 0; i < 2; i++)
                {
                    dust = WavePoint.transform.position;
                    float x = Random.Range(-5.0f, 5.0f);
                    float z = Random.Range(-5.0f, 5.0f);
                    dust.x += x;
                    dust.z += z;
                    float rotationy = Random.Range(0f, 360f);
                    temp = Instantiate(M4, dust, Quaternion.Euler(0, rotationy, 0), this.transform);
                }

                waveCount++;
            }
        }

        if (time > 9    && waveOn ==true)
        {
            waveOn = false;
            Destroy(Mission_02);
            Mission_03.SetActive(true);
        }
    }
}
