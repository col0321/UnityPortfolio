using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    public GameObject Somke_Left;
    public GameObject Somke_Right;
    private float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        time += 1.0f * Time.deltaTime;
        if (time > 0.3f)
        {
            GameObject obj;
            obj = Instantiate(Somke_Left, this.transform.position, Somke_Left.transform.rotation, null);
            obj = Instantiate(Somke_Right, this.transform.position, Somke_Right.transform.rotation, null);

            time = 0.0f;
        }
    }
}
