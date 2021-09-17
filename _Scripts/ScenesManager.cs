using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public GameObject Canvas;
    public GameObject Delete;

    private void Awake()
    {
        Player.SetActive(true);
        Camera.SetActive(true);
        Canvas.SetActive(true);
        Player.GetComponent<PlayerControl>().StopMove();
        Player.GetComponent<PlayerControl>().GetHight = false;
        Player.transform.position = new Vector3(0, 0, 0);
        Destroy(Delete);
    }

    private void Update()
    {
        GameObject.Find("Minimap_image").SetActive(false);
        SceneManager.LoadScene("EndScenes");
    }
}
