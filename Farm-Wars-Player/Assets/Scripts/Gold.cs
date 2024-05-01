using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    [SerializeField] private int _dollarsPerSecond;

    void Update()
    {
        if (_self.activeSelf == true)
        {
            Bank.money += _dollarsPerSecond * Time.deltaTime;
        }
    }
}
