using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEt : MonoBehaviour
{

    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Player.transform.position = new Vector3(0, 0, 0);
    }
}
