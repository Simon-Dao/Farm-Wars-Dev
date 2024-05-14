using UnityEngine;
using Photon.Pun;
using System.Collections;
using TMPro;

public class SharedVariableManager : MonoBehaviourPunCallbacks
{
    private int totalCoinsCollected;
    public TMP_Text testField;

    public PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        // Ensure that this GameObject is controlled by the local player
        if (photonView.IsMine)
        {
            totalCoinsCollected = 0;
        }
    }

    void Update()
    {
        testField.text = totalCoinsCollected.ToString();
    }

    [PunRPC]
    void SyncTotal(int newTotal)
    {
        totalCoinsCollected = newTotal;
    }

    public void IncreaseScoreLocally()
    {
        totalCoinsCollected++;

        photonView.RPC("SyncTotal", RpcTarget.Others, totalCoinsCollected);
    }

    public int GetTotalCoinsCollected()
    {
        return totalCoinsCollected;
    }
}