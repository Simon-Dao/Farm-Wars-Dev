using System;
using UnityEngine;

public class Player : MonoBehaviour, ICloneable
{
    public int _playerID;
    public Color _color;
    public String _playerName;

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
