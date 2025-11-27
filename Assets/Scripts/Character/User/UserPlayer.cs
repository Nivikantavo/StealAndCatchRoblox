using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private PlayerCharacterController _characterController;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _carringSpeed;

    public Transform GetCameraPivot()
    {
        return _cameraPivot;
    }

    public override void OnMobLost(IInteractable stolenMob)
    {
        //—ообщить игроку о том что у него воруют

    }

    public override void OnMobStolen(IInteractable stolenMob)
    {

    }

    public override void TakeKnokout()
    {
        _characterController.Pause(true);
        StartCoroutine(Stunned());
    }

    private IEnumerator Stunned()
    {
        yield return new WaitForSeconds(3);
        _characterController.Pause(false);
        _characterAnimation.SetIsKnoked(false);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public override void OnMobTaken()
    {
        _characterController.maxWalkSpeed = _carringSpeed;
    }

    public override void OnMobReleased()
    {
        _characterController.maxWalkSpeed = _defaultSpeed;
    }
}
