using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class InputManager : MonoBehaviour, IPunObservable
{
    [SerializeField] private TMP_Text debugText; // Consider removing if not used
    private PhotonView photonView;

    private string actionTriggered = "";

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        actionTriggered = "";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Implement your code here if needed
    }

    public void DestroyNetworkObject(string objToDestroy)
    {
        photonView.RPC("DestroyObj", RpcTarget.AllBuffered, objToDestroy);
    }

    [PunRPC]
    private void DestroyObj(string objToDestroy)
    {
        var obj = GameObject.Find(objToDestroy);
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    [PunRPC]
    private void SetActionTriggered(string newAction)
    {
        actionTriggered = newAction;
    }

    public void CallActionTriggered(string newAction)
    {
        if (newAction != actionTriggered)
        {
            photonView.RPC("SetActionTriggered", RpcTarget.AllBuffered, newAction);
        }
    }

    public void ResetActionTriggered()
    {
        actionTriggered = "";
    }

    public bool IsTileTriggered(string tileName, int playerID)
    {
        if (string.IsNullOrEmpty(actionTriggered)) return false;

        string[] parts = actionTriggered.Split(';');
        if (parts.Length < 2) return false; // Ensure there are enough parts to compare

        bool playerMatch = parts[0] == playerID.ToString();
        bool tileMatch = parts[1] == tileName;

        return playerMatch && tileMatch;
    }
}
