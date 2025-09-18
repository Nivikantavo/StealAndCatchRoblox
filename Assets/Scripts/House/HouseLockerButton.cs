using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HouseLockerButton : MonoBehaviour
{
    public Action<Player> LockerButtonWasClicked;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            LockerButtonWasClicked?.Invoke(player);
        }
    }
}
