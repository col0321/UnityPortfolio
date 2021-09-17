using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnddingSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        obj.transform.position = new Vector3(0, 0, 0);
    }

}
