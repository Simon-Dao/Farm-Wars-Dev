using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile parentTile;
    public float _value;
    public float _growTime;
    public float _decayBuffer;
    public float _decayTime;
    public bool decay = false;
    public Color _claimColor;

    private bool _touch;
    private float _copyValue;
    private float _copyGrowTime;
    private float _copyDecayBuffer;
    private float _copyDecayTime;
    private Color _copyColor;
    void Start()
    {
        _copyColor = _self.GetComponent<SpriteRenderer>().color;
        Reset();
    }
    void Update()
    {
        if (parentTile._ePressed && decay) //&& _touch)
        {
            Bank.money += _copyValue;
            decay = false;
            _self.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
            // _self.SetActive(false);
            parentTile.CallPlantChange(false);
            Reset();
        }
    }
    void FixedUpdate()
    {
        if (decay)
        {
            if (_copyDecayBuffer > 0)
            {
                _copyDecayBuffer -= Time.deltaTime;
            }
            else if (_copyDecayTime > 0)
            {
                _copyDecayTime -= Time.deltaTime;
                _copyValue -= (float)0.1 * _copyValue * Time.deltaTime;
                float f = 1 * Time.deltaTime / _decayTime;
                _self.GetComponent<SpriteRenderer>().color -= new Color(f, f, f, 0);
            }
        }
        else if (_self.activeSelf && _self.GetComponent<SpriteRenderer>().color.a < 255)
        {
            _self.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, (1 * Time.deltaTime) / _growTime);
            if (_self.GetComponent<SpriteRenderer>().color.a >= 0.9)
            {
                decay = true;
                _self.GetComponent<SpriteRenderer>().color = _claimColor;
            }
        }
    }
    void Reset()
    {
        _copyDecayBuffer = _decayBuffer;
        _copyDecayTime = _decayTime;
        _copyGrowTime = _growTime;
        _copyValue = _value;
        _self.GetComponent<SpriteRenderer>().color = _copyColor;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _touch = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _touch = false;
        }
    }
}