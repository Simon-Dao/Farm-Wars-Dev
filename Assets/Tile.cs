using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _plant;
    [SerializeField] private int cost;
    [SerializeField] private bool _touch;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(true);
            _touch = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(false);
            _touch = false;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && _touch)
        {
            if (Bank.money >= cost && !_plant.activeSelf)
            {
                Bank.money -= cost;
                _plant.SetActive(true);
            }
        }
    }
}