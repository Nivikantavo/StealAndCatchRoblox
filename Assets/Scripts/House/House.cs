using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class House : MonoBehaviour
{
    public bool IsClosed => Locker.IsClosed;
    public bool HasFreeHolder => MobCatcher.HasFreeHolder;
    

    [SerializeField] protected MobsCatcher MobCatcher;
    [SerializeField] protected HouseLocker Locker;
    [SerializeField] protected List<MobHolder> Holders;
    [SerializeField] protected SecuritySystem SecuritySystem;

    protected int LayerNumber;
    protected Player Owner;

    public MobHolder GetFreeHolder()
    {
        return MobCatcher.GetFreeHolder();
    }
}
