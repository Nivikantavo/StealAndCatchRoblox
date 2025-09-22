using UnityEngine;
using Zenject;

public class House : MonoBehaviour
{
    public bool IsClosed => _locker.IsClosed;
    public bool HasFreeHolder => _mobCatcher.HasFreeHolder;

    [SerializeField] private MobsCatcher _mobCatcher;
    [SerializeField] private HouseLocker _locker;

    //TO DO: вынести в Construct
    [SerializeField] private int _layerNumber;
    private Player _owner;

    public MobHolder GetFreeHolder()
    {
        return _mobCatcher.GetFreeHolder();
    }

    [Inject]
    private void Construct(Player owner)
    {
        _owner = owner;
        _owner.Initialize(this);
        _owner.gameObject.layer = _layerNumber;
        _mobCatcher.Initialize(_owner);
        _locker.Initialize(_owner);
    }
}
