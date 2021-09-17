using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Player;
    public GameObject NPC;
    public GameObject Text;
    public GameObject Gkey;

    Vector3 startposition;
    Vector3 EndPosition;
    Vector3 vec;

    private bool on = false;

    private void Awake()
    {
        startposition = this.transform.position;
        EndPosition = startposition;
        EndPosition.x += 1500;
        this.transform.position = EndPosition;
        vec = new Vector3(0, 3.5f, 0);
    }

    private void Update()
    {
        if (NPC != null)
        {
            Text.transform.position = Camera.main.WorldToScreenPoint(NPC.transform.position + vec);

            if (Vector3.Distance(NPC.transform.position, Player.transform.position) < 5.0f)
            {
                if (Gkey.activeSelf == false && on == false)
                    Gkey.SetActive(true);
                if (on == true)
                    Gkey.SetActive(false);

                Gkey.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + vec);


                if (Input.GetKeyDown(KeyCode.G))
                {
                    this.transform.position = startposition;
                    on = true;
                }
            }
            else
            {
                if (Gkey.activeSelf == true)
                    Gkey.SetActive(false);

                on = false;
                this.transform.position = EndPosition;
            }
        }
    }

    public void BuyHpPotion()
    {
        Player.GetComponent<PlayerInfo>().money -= 1000;
        Inventory.GetComponent<InventoryManager>().ItemAdd(2);
    }

    public void BuyMpPotion()
    {
        Player.GetComponent<PlayerInfo>().money -= 1000;
        Inventory.GetComponent<InventoryManager>().ItemAdd(1);
    }

}
