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
        _house = house;
    }
}
