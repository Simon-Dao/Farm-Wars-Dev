using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class Bank : MonoBehaviourPun, IPunObservable
{
    public static float money = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if(stream.IsWriting) {
            stream.SendNext(money);

        } else if(stream.IsReading) {
            money = (float)stream.ReceiveNext();
        }
    }

    void Start()
    {
        money += 100;
    }
}
