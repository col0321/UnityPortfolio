using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTextSet : MonoBehaviour
{
    public bool Seton = false;
    private float time = 0.32f;
    private Vector3 startpo;
    // Start is called before the first frame update
    void Start()
    {
        startpo = this.transform.position;
        this.transform.position = new Vector3(0, -5000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Seton)
        {
            this.transform.position = startpo;
            time -= 0.25f * Time.deltaTime;
            this.transform.GetComponent<TMPro.TextMeshProUGUI>().fontMaterial.SetFloat("_FaceDilate", time);
            Destroy(this.gameObject, 10f);
        }
    }
}
