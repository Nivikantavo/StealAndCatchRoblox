using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string IsMoving = "IsMoving";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";
    private const string Attack = "Attack";

    [SerializeField] private Animator _animator;

    public void SetIsMoving(bool isMoving)
    {
        _animator.SetBool(IsMoving, isMoving);
    }

    public void SetJumping(bool isJumping)
    {
        _animator.SetBool(IsJumping, isJumping);
    }

    public void SetFalling(bool isFalling)
    {
        _animator.SetBool(IsFalling, isFalling);
    }

    public void SetAttack()
    {
        _animator.SetTrigger(Attack);
    }
}
