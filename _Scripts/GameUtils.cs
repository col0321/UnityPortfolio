using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameUtils
{
    static public float CalculateAngle(Vector3 a, Vector3 b)
    {
        float rot = Vector3.Dot(a, b);
        rot = Mathf.Acos(rot);
        rot = (rot * 180.0f) / Mathf.PI;
        Vector3 right = Vector3.Cross(Vector3.up, a);
        right.Normalize();
        if (Vector3.Dot(right, b) < 0.0f)
        {
            rot *= -1f;
        }
        return rot;
    }

    public static IEnumerator DelayMethod(UnityAction method, float t)
    {
        yield return new WaitForSeconds(t);
        method?.Invoke();
    }

}
