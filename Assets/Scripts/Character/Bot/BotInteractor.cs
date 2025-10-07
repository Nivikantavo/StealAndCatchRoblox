using System.Collections.Generic;
using UnityEngine;

public class BotInteractor : MonoBehaviour, IInteractor
{
    public Transform SelfTransform { get; private set; }
    public Transform HouseTransform { get; private set; }
    public IWallet Wallet => _player.Wallet;
    public MobHolder MobHolder => _player.GetFreeMobHolder();
    public Transform CarryPosition => _carryMobHolder;
    public float InteractionRange => _interactionRange;

    private Player _player;
    [SerializeField] private float _interactionRange = 3f;
    [SerializeField] private Transform _carryMobHolder;


    public void Initialize(Player player, Transform houseTransform)
    {
        _player = player;
        HouseTransform = houseTransform;
        SelfTransform = _player.transform;
    }

    public List<IInteractable> FindClosestInteractables(float findDistance)
    {
        Collider[] hitColliders = Physics.OverlapSphere(SelfTransform.position, findDistance);
        List<IInteractable> colsestInteractable = new List<IInteractable>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(SelfTransform.position, hitCollider.transform.position);
                if (colsestInteractable.Contains(interactable) == false)
                {
                    colsestInteractable.Add(interactable);
                }
            }
        }
        return colsestInteractable;
    }
}
