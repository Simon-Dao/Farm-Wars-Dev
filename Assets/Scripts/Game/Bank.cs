using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEditor;
public class Bank : MonoBehaviour
{
    //public static float money = 0;
    public Dictionary<int, float> _accounts;

    private int main_player_id;

    private float _elapsedTime;
    private float _grant_interval;
    private PhotonView photonView;
    void Start()
    {
        _elapsedTime = 0f;
        _grant_interval = 1f;
        //money = 100;
        //call
        main_player_id = PhotonNetwork.LocalPlayer.ActorNumber;
        _accounts = new Dictionary<int, float>();
        photonView = GetComponent<PhotonView>();

        CallAddAccount(main_player_id);
    }
    void Update()
    {
        // money += Time.deltaTime * 2;
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _grant_interval)
        {
            //call
            Deposit(main_player_id, 2);
            _elapsedTime = 0;
        }

    }

    [PunRPC]
    void Deposit(int playerID, float money)
    {
        if(!_accounts.ContainsKey(playerID)) return;
        _accounts[playerID] += money;
    }

    [PunRPC]
    void AddAccount(int playerID)
    {
        _accounts.Add(playerID, 100);
    }

    void CallAddAccount(int playerID)
    {
        photonView.RPC("AddAccount", RpcTarget.AllBuffered, playerID);
    }

    public void CallDeposit(int playerID, float money)
    {
        photonView.RPC("Deposit", RpcTarget.AllBuffered, playerID, money);
    }

    public float GetBalance(int playerID) {

        if(!_accounts.ContainsKey(playerID)) {
            return 0;
        }
        return _accounts[playerID];
    }

    public Player GetWinner()
    {
        int maxID = 0;
        float maxVal = 0;

        foreach (KeyValuePair<int, float> pair in _accounts)
        {
            if (maxVal <= pair.Value)
            {
                maxID = pair.Key;
                maxVal = pair.Value;
            }
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            Player cur = players[i].GetComponent<Player>();
            if (cur._playerID == maxID)
            {
                return cur;
            }
        }

        return null;
    }
}
