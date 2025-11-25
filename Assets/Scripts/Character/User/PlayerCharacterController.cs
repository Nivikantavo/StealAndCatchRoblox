using ECM.Controllers;
using ECM2;
using ECM2.Examples.ThirdPerson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : Character
{
    private CharacterAnimation _characterAnimation;

    protected override void Awake()
    {
        base.Awake();
        _characterAnimation = GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        Animate();
    }

    protected void Animate()
    {
        if (_characterAnimation == null)
            return;

        _characterAnimation.SetIsMoving(GetMovementDirection() != Vector3.zero);
        _characterAnimation.SetJumping(_isJumping);
        _characterAnimation.SetFalling(IsFalling());
    }
}
