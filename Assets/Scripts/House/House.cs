using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool IsClosed => Locker.IsClosed;
    public bool HasFreeHolder => MobCatcher.HasFreeHolder;
    public bool HasMobs => MobCatcher.HasMobs;
    public Transform OwnerSpawnPosition => _ownerSpawnPosition;

    [SerializeField] protected MobsCatcher MobCatcher;
    [SerializeField] protected HouseLocker Locker;
    [SerializeField] protected List<MobHolder> Holders;
    [SerializeField] protected SecuritySystem SecuritySystem;
    [SerializeField] private Transform _ownerSpawnPosition;

    protected int LayerNumber;
    protected Player Owner;

    public MobHolder GetFreeHolder()
    {
        return MobCatcher.GetFreeHolder();
    }

    public void Restart()
    {
        MobCatcher.Restart();
        Locker.Restart();
    }
}
