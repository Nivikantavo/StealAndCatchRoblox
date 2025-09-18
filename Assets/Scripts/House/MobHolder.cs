using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHolder : MonoBehaviour
{
    public bool IsFree => _mob != null;

    [SerializeField] private Transform _holdingPosition;

    private BrainrotMob _mob;
    private Player _owner;
    private float _earned;

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
        _mob.Stop();
        _mob.transform.position = _holdingPosition.position;
        StartEarning();
    }

    private async void StartEarning()
    {
        await MobEarning();
    }

    private async UniTask MobEarning()
    {
        while (_mob != null && _earned < _mob.Config.BaseCost / 2)
        {
            await UniTask.WaitForSeconds(1);
            _earned += _mob.Config.ValuePerSecond;
        }
    }

    private void AddEarningsToPlayer()
    {
        //начислить заработанное
        Debug.Log($"Add {_earned} to player");
        _earned = 0;
    }
}
