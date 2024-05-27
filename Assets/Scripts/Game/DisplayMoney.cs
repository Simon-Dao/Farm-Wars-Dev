using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private Text _text;
    void Update()
    {
        _text.text = "$" + Bank.money.ToString("0");
    }
}