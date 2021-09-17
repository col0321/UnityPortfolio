using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    public Material[] material;
    int num = 0;
    Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[num];
    }
    public void MaterialChage(int num)
    {
        rend.sharedMaterial = material[num];
    }

    public void dissolveShader()
    {
        rend.sharedMaterial = material[2];
    }

}
