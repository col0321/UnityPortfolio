using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_02 : MonoBehaviour
{
    public GameObject EnemyManager;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "WAVE (" + (EnemyManager.GetComponent<EnemyManager>().waveCount) + "/3)";
    }
}
