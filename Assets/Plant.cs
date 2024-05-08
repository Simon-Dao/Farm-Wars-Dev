using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    public float _value;
    public float _growTime;
    public float _decayBuffer;
    public float _decayTime;
    public bool decay = false;
    public Color _claimColor;

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
        if (Input.GetKey(KeyCode.E) && decay)
        {
            Bank.money += _copyValue;
            decay = false;
            _self.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
            _self.SetActive(false);
            Reset();
        }
        if (decay)
        {
            if (_copyDecayBuffer > 0)
            {
                _copyDecayBuffer -= Time.deltaTime;
            }
            else if (_copyDecayTime > 0)
            {
                _copyDecayTime -= Time.deltaTime;
                _copyValue -= _copyValue * Time.deltaTime;
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
    void OnMouseDown()
    {
        if (decay)
        {
            Bank.money += _copyValue;
            decay = false;
            _self.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
            _self.SetActive(false);
            Reset();
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
}