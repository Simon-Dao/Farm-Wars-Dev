using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class Tile : MonoBehaviourPun    
{
    [SerializeField] protected Color _baseColor, _offsetColor;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected int cost;
    [SerializeField] protected bool _touch;
    [SerializeField] public Player _currPlayer = null;

    public string _tile_id;

    public bool _ePressed = false;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(true);
            _touch = true;
        }
    }
    protected void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(false);
            _touch = false;
        }
    }
    public void updateEPressed(InputManager inputManager)
    {
        if (Input.GetKey(KeyCode.E) && !_ePressed)
        {
            // Key is pressed and it was not previously pressed
            _ePressed = true;
            //actor id + tile pressed
            int id = GetComponent<Player>()._playerID;
            string nameOfTile = _tile_id;

            inputManager.CallActionTriggered($"{id};{nameOfTile};");
        }
        else
        {
            // Either key is not pressed or it was already pressed in the last frame
            if (_ePressed)
            {
                // If previously pressed, reset and send false state
                _ePressed = false;
            }
        }
    }

    public string Serialize()
    {
        // Serialize Color as RGBA float values
        return _tile_id;
    }

    public void Deserialize(string data)
    {
        _tile_id = data;
    }

}