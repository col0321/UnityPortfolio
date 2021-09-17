using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public Transform player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = player.position;
        this.transform.position = temp;
    }
}
