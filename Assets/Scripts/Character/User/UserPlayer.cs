using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    public override void OnMobLost(IInteractable stolenMob)
    {
        //�������� ������ � ��� ��� � ���� ������
        Debug.Log("� ���� ������!");
    }

    public override void OnMobStolen(IInteractable stolenMob)
    {
        Debug.Log("�������");
    }
}
