using Cysharp.Threading.Tasks;
using UnityEngine;

public class MobHolder : MonoBehaviour
{
    public bool IsFree => _mob == null;
    public BrainrotMob Mob => _mob;

    [SerializeField] private Transform _holdingPosition;

    private BrainrotMob _mob;
    private Player _owner;
    private int _earned;

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

    public void SetMobOnPosition()
    {
        _mob.Stop();
        _mob.transform.position = _holdingPosition.position;
        _mob.transform.rotation = _holdingPosition.rotation;

        StartEarning();
    }

    private async void StartEarning()
    {
        await MobEarning();
    }

    private async UniTask MobEarning()
    {
        while (_mob != null)
        {
            if(_earned < _mob.Config.BaseCost / 2)
            {
                _earned += _mob.Config.ValuePerSecond;
            }
            await UniTask.WaitForSeconds(1);
        }
    }

    private void AddEarningsToPlayer()
    {
        Debug.Log($"Add {_earned} to player");
        _owner.Wallet.AddMoney(_earned);
        _earned = 0;
    }
}
