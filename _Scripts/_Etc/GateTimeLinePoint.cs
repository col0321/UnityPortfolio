using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTimeLinePoint : MonoBehaviour
{
    public GameObject Timeline;
    public GameObject Player;

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 1.0f)
        {
            Timeline.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
