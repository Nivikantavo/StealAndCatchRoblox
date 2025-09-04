using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : BaseCharacterController
{
    [SerializeField] private CharacterAnimation _characterAnimation;
    protected override void Animate()
    {
        if (_characterAnimation == null)
            return;
        base.Animate();

        _characterAnimation.SetIsMoving(moveDirection != Vector3.zero);
        _characterAnimation.SetJumping(isJumping);
        _characterAnimation.SetFalling(isFalling);

        if (Input.GetMouseButtonDown(0))
        {
            _characterAnimation.SetAttack();
        }
    }
}
