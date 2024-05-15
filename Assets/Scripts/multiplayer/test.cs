using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class test : MonoBehaviourPun, IPunObservable
{
    int testVar;
    bool keydown;

    public TMP_Text test_text;

    // Start is called before the first frame update
    void Start()
    {
        testVar = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if(stream.IsWriting) {
            stream.SendNext(testVar);

        } else if(stream.IsReading) {
            testVar = (int)stream.ReceiveNext();
            Debug.Log("testvar set to "+ testVar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(!keydown) {
                keydown = true;
                CallIncreasedTestVar(testVar + 1);
            }
        } else {
            keydown = false;
        }

        test_text.text = testVar.ToString();
    }

    [PunRPC]
    void IncreasedTestVar(int newTestVar) 
    {
        testVar = newTestVar;
    }

    // Example method to call RPC to increase player score
    public void CallIncreasedTestVar(int newTestVar) 
    {
        photonView.RPC("IncreasedTestVar", RpcTarget.AllBuffered, newTestVar);
    }
}
