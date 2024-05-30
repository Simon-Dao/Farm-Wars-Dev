using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviourPun
{
    private PhotonView photonView;
    private Bank bank;

    void Start() {
        photonView = GetComponent<PhotonView>();
        bank = GameObject.Find("Controller").GetComponent<Bank>();
    }
    [SerializeField] private Text _text;
    void Update()
    {
        _text.text = "$" + bank.GetBalance(PhotonNetwork.LocalPlayer.ActorNumber);
    }
}