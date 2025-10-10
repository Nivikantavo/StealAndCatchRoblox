using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Player : MonoBehaviour
{
    public IInteractor Interactor => _interactor;
    public IWallet Wallet => _wallet;
    public IStealer Stealer => _mobStealer;

    private IInteractor _interactor;
    protected PlayerFighter _fighter;
    protected CharacterAnimation _characterAnimation;
    protected House _house;
    protected Wallet _wallet;
    protected MobStealer _mobStealer;

    protected float AttackCooldown = 2;
    protected float AttackElapsedTime = 0;

    protected virtual void Update()
    {
        if(AttackElapsedTime <  AttackCooldown)
            AttackElapsedTime += Time.deltaTime;
    }

    public MobHolder GetFreeMobHolder()
    {
        return _house.GetFreeHolder();
    }

    public virtual void Initialize(House house)
    {
        _house = house;
        _interactor = GetComponent<IInteractor>();
        _fighter = GetComponent<PlayerFighter>();
        _mobStealer = GetComponent<MobStealer>();
        _characterAnimation = GetComponent<CharacterAnimation>();

        _interactor.Initialize(this, _house.transform);
        _mobStealer.Initialize(_interactor);
        _fighter.Initialize(this);
        _wallet = new Wallet(1000);
    }

    public virtual void Attack()
    {
        if (AttackElapsedTime >= AttackCooldown)
        {
            _characterAnimation.SetAttack();
            _fighter.Attack();
            AttackElapsedTime = 0;
        }
    }

    public virtual void TakeHit()
    {
        _mobStealer.LoseMob();
    }

    public abstract void OnMobStolen(IInteractable stolenMob);
    public abstract void OnMobLost(IInteractable stolenMob);
}
