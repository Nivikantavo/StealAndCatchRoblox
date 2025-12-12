using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private CharacterAnimation _characterAnimation;

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _characterAnimation.SetIsMoving(true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _characterAnimation.SetIsMoving(false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _characterAnimation.SetJumping(true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _characterAnimation.SetJumping(false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            _characterAnimation.SetFalling(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _characterAnimation.SetFalling(false);
        }
        if (Input.GetKey(KeyCode.R))
        {
            _characterAnimation.SetIsKnoked(true);
        }
        if (Input.GetKey(KeyCode.F))
        {
            _characterAnimation.SetIsKnoked(false);
        }
        if (Input.GetKey(KeyCode.T))
        {
            _characterAnimation.SetAttack();
        }
    }
}
