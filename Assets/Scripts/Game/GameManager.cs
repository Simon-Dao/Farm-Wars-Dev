using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _waitingText;
    [SerializeField] private int _minPlayersToStart = 0;
    [SerializeField] GameObject button;
    private int _gameLength = 180;  // Length in seconds
    private int _timeLeft;
    private float _secondCount = 0f;
    private bool _gameStarted;
    private Bank _bank;

    [SerializeField] private TMP_Text timeLeftText;

    void Start()
    {
        _gameStarted = false;
        _timeLeft = _gameLength;
        _bank = GetComponent<Bank>();
    }

    void Update()
    {
        
        CheckPlayerCount();
        if (_timeLeft <= 0)
        {
            EndGame();
        } else 
        if (_gameStarted)
        {
            UpdateTimer();
        }


    }

    private void CheckPlayerCount()
    {
        int numPlayers = PhotonNetwork.PlayerList.Length;
        if (numPlayers <= _minPlayersToStart)
        {
            Time.timeScale = 0;
            _waitingText.text = $"Waiting for players {numPlayers}/{_minPlayersToStart}";
        }
        else if (!_gameStarted)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _gameStarted = true;
        _waitingText.gameObject.SetActive(false);
        Time.timeScale = 1;
        _timeLeft = _gameLength;
    }

    [PunRPC]
    private void SyncStartTime(int startTime)
    {
        _timeLeft = startTime;
    }

    private void UpdateTimer()
    {
        _secondCount += Time.deltaTime;
        if (_secondCount >= 1f)
        {
            _secondCount = 0;
            GetComponent<PhotonView>().RPC("SyncStartTime", RpcTarget.AllBuffered, _timeLeft - 1);
            timeLeftText.text = "Time Left: " + _timeLeft.ToString();
        }
    }

    private void EndGame()
    {
        button.SetActive(true);
        Time.timeScale = 0;
        // _gameStarted = false;
        Player winner = _bank.GetWinner();
        timeLeftText.gameObject.SetActive(false);
        _waitingText.gameObject.SetActive(true);
        Debug.Log("wtf is going on????");
        _waitingText.text = $"Game Over, Player {winner._playerName} with ${_bank._accounts[winner._playerID]} wins!";
    }

    public void GoBackToMainMenu()
    {
        PhotonNetwork.LoadLevel("Loading");
    }
}
