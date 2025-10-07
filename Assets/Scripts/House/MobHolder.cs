using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class MobHolder : MonoBehaviour
{
    public event Action<int> MoneyCountChanged;
    public int Earned => _earned;
    public bool IsFree => _mob == null;
    public int MaxValue => _mob.Config.MaxEarning;
    public Transform HoldingPosition => _holdingPosition;

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
    }

    public void ClearMob()
    {
        _mob = null;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0) return;
        _earned += amount;
        MoneyCountChanged?.Invoke(_earned);
    }

    public bool ItsMyMob(BrainrotMob mob)
    {
        return _mob == mob;
    }

    private void AddEarningsToPlayer()
    {
        Debug.Log($"Add {_earned} to player");
        _owner.Wallet.AddMoney(_earned);
        _earned = 0;
    }
}
