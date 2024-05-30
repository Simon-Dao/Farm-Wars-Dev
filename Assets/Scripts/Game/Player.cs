using System;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour, ICloneable
{
    public int _playerID;
    public Color _color;
    public String _playerName;

    void Start()
    {

        if (gameObject.tag == "Player")
        {
            _playerName = GetComponent<PhotonView>().Owner.NickName;
            _playerID = GetComponent<PhotonView>().Owner.ActorNumber;
            _color = GenerateColor();
        }

    }
    private Color GenerateColor()
    {
        // Random hue from 0 to 1
        float hue = UnityEngine.Random.Range(0f, 1f);

        // Saturation between 0.4 and 0.9 for vibrancy without being overwhelming
        float saturation = UnityEngine.Random.Range(0.4f, 0.9f);

        // Lightness between 0.5 and 0.8 to avoid extremes of too dark or too light
        float lightness = UnityEngine.Random.Range(0.5f, 0.8f);

        // Convert HSL to RGB
        return Color.HSVToRGB(hue, saturation, lightness);
    }

    public int GetID()
    {
        return _playerID;
    }

    public Color GetColor()
    {
        return _color;
    }

    public static bool operator ==(Player left, Player right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return false;
        }

        return left._playerID == right._playerID;
    }

    public static bool operator !=(Player left, Player right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Player other = (Player)obj;
        return _playerID == other._playerID;
    }

    public override int GetHashCode()
    {
        return _playerID.GetHashCode();
    }

    public Player Clone()
    {
        return new Player
        {
            _playerID = this._playerID,
            _color = this._color
        };
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

}
