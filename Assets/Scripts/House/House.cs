using UnityEngine;
using Zenject;

public class House : MonoBehaviour
{
    public bool IsClosed => Locker.IsClosed;
    public bool HasFreeHolder => MobCatcher.HasFreeHolder;

    [SerializeField] protected MobsCatcher MobCatcher;
    [SerializeField] protected HouseLocker Locker;

    protected int LayerNumber;
    protected Player Owner;

    public MobHolder GetFreeHolder()
    {
        return MobCatcher.GetFreeHolder();
    }
}
