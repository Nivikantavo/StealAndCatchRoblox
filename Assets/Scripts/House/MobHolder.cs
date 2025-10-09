using System;
using UnityEngine;

public class MobHolder : MonoBehaviour
{
    public Action<IInteractable> MobWasStolen;
    public Action<IInteractable> StolenMobLost;

    public event Action<int> MoneyCountChanged;
    public int Earned => _earned;
    public bool IsFree => _mob == null;
    public int MaxValue => _mob.Config.MaxEarning;
    public Transform HoldingPosition => _holdingPosition;
    public IInteractor Owner => _owner.Interactor;

    [SerializeField] private Transform _holdingPosition;

    private Player _owner;
    private int _earned;
    private BrainrotMob _mob;

    public void Initialize(Player player)
    {
        _owner = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            if(player == _owner)
            {
                AddEarningsToPlayer();
            }
        }
    }
    
    public void SetMob(BrainrotMob mob)
    {
        _mob = mob;
        _mob.Taked += OnMobWasStolen;
        _mob.Stolen += MobWasLost;
    }

    public void ClearMob()
    {
        _mob.Taked -= OnMobWasStolen;
        _mob.Stolen -= MobWasLost;
        _mob = null;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0 || _earned >= MaxValue) return;
        _earned += amount;
        _earned = Mathf.Clamp(_earned, 0, MaxValue);
        MoneyCountChanged?.Invoke(_earned);
    }

    public bool ItsMyMob(BrainrotMob mob)
    {
        return _mob == mob;
    }

    private void AddEarningsToPlayer()
    {
        _owner.Wallet.AddMoney(_earned);
        _earned = 0;
    }

    private void OnMobWasStolen()
    {
        MobWasStolen?.Invoke(_mob);
    }

    private void MobWasLost()
    {
        StolenMobLost?.Invoke(_mob);
    }
}
