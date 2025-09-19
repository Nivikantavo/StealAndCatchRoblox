using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    public Transform SelfTransform { get; private set; }
    public Transform HouseTransform { get; private set; }

    [SerializeField] private float _interactionRange = 3f;
    [SerializeField] private InteractableView _interactableView;

    private IInteractable _currentInteractable;
    private Player _player;

    public void Initialize(Player player, Transform houseTransform)
    {
        _player = player;
        SelfTransform = _player.transform;
        HouseTransform = houseTransform;
        _interactableView.Initialize(this);
    }

    private void Update()
    {
        _currentInteractable = FindClosestInteractable();

        if (_currentInteractable != null)
        {
            ShowInteractable(_currentInteractable);
        }
        else
        {
            ShowInteractable(null);
        }
    }

    private void OnEnable()
    {
        _interactableView.OnInteract += OnTryInteract;
    }

    private void OnDisable()
    {
        _interactableView.OnInteract -= OnTryInteract;
    }

    private IInteractable FindClosestInteractable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(SelfTransform.position, _interactionRange);
        float minDistance = float.MaxValue;
        IInteractable colsestInteractable = null;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(SelfTransform.position, hitCollider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    colsestInteractable = interactable;
                }
            }
        }
        return colsestInteractable;
    }

    private void ShowInteractable(IInteractable interactable)
    {
        _interactableView.SetInteractable(interactable);
    }

    private void OnTryInteract()
    {
        switch (_currentInteractable.InteractAction.ActionType)
        {
            case InteractActionType.Buy:
                var sellAction = (PurchasableMob)_currentInteractable.InteractAction;
                sellAction.ExecuteAction(this);
                break;
        }
    }
}
