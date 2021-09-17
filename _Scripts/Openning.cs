using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Openning : MonoBehaviour
{
    public GameObject materials;
    void Start()
    {
        SceneManager.LoadScene("GameScenes");
    }
}
