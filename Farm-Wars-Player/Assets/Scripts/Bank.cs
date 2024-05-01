using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static float money = 0;

    void Start()
    {
        money += 100;
    }
}
