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
    public float _multiRangeMin;
    public float _multiRangeMax;
    public float _claimCooldown = 2f; // Cooldown period in seconds

    private bool _touch;
    private float _copyValue;
    private float _copyGrowTime;
    private float _copyDecayBuffer;
    private float _copyDecayTime;
    private Color _copyColor;
    [SerializeField] private float _cooldownTimer = 0f;

    void Start()
    {
        _value *= Random.Range(_multiRangeMin, _multiRangeMax);
        _copyColor = _self.GetComponent<SpriteRenderer>().color;
        Reset();
    }

    void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E) && decay && _touch && _cooldownTimer <= 0)
        {
            Bank.money += _copyValue;
            decay = false;
            _self.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
            _self.SetActive(false);
            _cooldownTimer = _claimCooldown; // Start cooldown after claiming
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
                _copyValue -= 0.1f * _copyValue * Time.deltaTime;
                float fadeAmount = Time.deltaTime / _decayTime;
                _self.GetComponent<SpriteRenderer>().color -= new Color(fadeAmount, fadeAmount, fadeAmount, 0);
            }
        }
        else if (_self.activeSelf && _self.GetComponent<SpriteRenderer>().color.a < 1f)
        {
            _self.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, Time.deltaTime / _growTime);
            if (_self.GetComponent<SpriteRenderer>().color.a >= 0.9f)
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
        _self.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _touch = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _touch = false;
        }
    }
}
