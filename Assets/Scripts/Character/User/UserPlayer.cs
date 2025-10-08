using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    public override void OnMobLost(IInteractable stolenMob)
    {
        //Сообщить игроку о том что у него воруют
        Debug.Log("У тебя воруют!");
    }

    public override void OnMobStolen(IInteractable stolenMob)
    {
        Debug.Log("Опаздал");
    }
}
