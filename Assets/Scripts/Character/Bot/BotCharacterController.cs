using Cysharp.Threading.Tasks;
using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacterController : BaseAgentController
{
    private CharacterAnimation _characterAnimation;

    public override void Awake()
    {
        base.Awake();
        _characterAnimation = GetComponent<CharacterAnimation>();
    }

    protected override void Animate()
    {
        if (_characterAnimation == null)
            return;
        base.Animate();

        _characterAnimation.SetIsMoving(moveDirection != Vector3.zero);
        _characterAnimation.SetJumping(isJumping);
        _characterAnimation.SetFalling(isFalling);
    }

    protected override void HandleInput()
    {
        
    }

    public void GoTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
