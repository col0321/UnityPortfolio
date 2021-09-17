using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public GameObject Canvas;
    public GameObject EffectTemp;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Camera);
        DontDestroyOnLoad(Canvas);
        DontDestroyOnLoad(EffectTemp);
    }
}
