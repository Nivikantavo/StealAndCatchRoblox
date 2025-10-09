using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStealer : MonoBehaviour, IStealer
{
    public bool IsCarries => _stolenMob != null;

    [SerializeField] private Transform _carryPoint;

    private BrainrotMob _stolenMob;
    private IInteractor _interactor;

    public void Initialize(IInteractor interactor)
    {
        _interactor = interactor;
    }

    public void GrabMob(BrainrotMob mob)
    {
        _stolenMob = mob;
        _stolenMob.Stop();
        mob.Steal(_interactor);
        mob.transform.position = _carryPoint.position;
        mob.transform.rotation = _carryPoint.rotation;
        mob.transform.SetParent(_carryPoint);
    }

    public void LoseMob()
    {
        if( _stolenMob != null )
        {
            _stolenMob.Drop();
            _stolenMob = null;
        }
    }
}
