using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HousePlace : MonoBehaviour
{
    public bool HasHouse => _house != null;
    private House _house;

    public void SetHouse(House house)
    {
        if (_house == null)
        {
            _house = house;
        }
        else
        {
            throw new System.Exception("place already busy");
        }
    }

    public void Clear()
    {
        if (_house != null)
        {
            _house = null;
        }
    }
}
