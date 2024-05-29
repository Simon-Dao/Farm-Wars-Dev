using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class InputManager : MonoBehaviour, IPunObservable
{
    public string _action_triggered = "";

    public TMP_Text _debug_text;
    private PhotonView photonView;

    public void DestroyNetworkObject(string objToDestroy)
    {
        photonView.RPC("DestroyObj", RpcTarget.AllBuffered, objToDestroy);
    }

    [PunRPC]
    public void DestroyObj(string objToDestroy)
    {
        Destroy(GameObject.Find(objToDestroy));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    [PunRPC]
    void SetActionTriggered(string newAction)
    {
        _action_triggered = newAction;
    }
    public void CallActionTriggered(string newAction)
    {
        if(newAction != _action_triggered) {
            photonView.RPC("SetActionTriggered", RpcTarget.AllBuffered, newAction);
        }
    }

    public void ResetActionTriggered() {
        _action_triggered = "";
    }

    public bool IsTileTriggered(string tileName, int playerID) {
        if(_action_triggered == "") return false;


        if(tileName == "" && _action_triggered.Split(";")[1] == "") return false;
        if(playerID == 0 && _action_triggered.Split(";")[0] == "") return false;

        return tileName == _action_triggered.Split(";")[1] && _action_triggered.Split(";")[0] == playerID.ToString();        
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        _action_triggered = "";
    }
}
