using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    [SerializeField] private Transform _cameraPivot;

    public Transform GetCameraPivot()
    {
        return _cameraPivot;
    }

    public override void OnMobLost(IInteractable stolenMob)
    {
        //Сообщить игроку о том что у него воруют
        Debug.Log("У тебя воруют!");
    }

    public override void OnMobStolen(IInteractable stolenMob)
    {
        Debug.Log("Опаздал");
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
}
