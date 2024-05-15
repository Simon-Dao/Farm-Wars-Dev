using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text _text;
    void Start()
    {
        //_text.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

 

    void Update()
    {
        _text.text = "$" + Bank.money.ToString("0.00");
    }
}
