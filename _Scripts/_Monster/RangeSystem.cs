using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSystem : MonoBehaviour
{
    public LayerMask EnemyMask;
    public GameObject Target = null;

    public GameObject FindTarget()
    {
        if (Target == null) return null;
        return Target;
    }

    private void OnTriggerEnter(Collider other)
    {
        int temp = 1 << other.gameObject.layer;
        if ((EnemyMask & temp) == temp)
            Target = other.gameObject;
    }
}
