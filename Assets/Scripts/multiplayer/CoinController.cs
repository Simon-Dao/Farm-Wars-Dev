using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoinController : MonoBehaviour
{
    private PhotonView view;
    public GameObject sharedVariableManager;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        sharedVariableManager = GameObject.Find("SharedVariableManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (view.IsMine)
            {
                // Remove the coin from the scene for all clients
                PhotonNetwork.Destroy(gameObject);
                CoinSpawner.numCoins--;
                // sharedVariableManager.GetComponent<SharedVariableManager>().IncreaseScoreLocally();
            }
        }
    }
}
