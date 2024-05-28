using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class SetName : MonoBehaviourPunCallbacks
{
    public TMP_InputField  createInput;    

    public void Setname() {
        PhotonNetwork.NickName = createInput.text;
    }
}
