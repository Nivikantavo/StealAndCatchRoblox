using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactLabel;
    [SerializeField] private Button _interactButton;

    private IInteractable _interactable;

    public void Initialize(IInteractable interactable)
    {
        _interactable = interactable;
    }

    private void OnEnable()
    {
        _interactButton.onClick.AddListener(OnInteractButtonClicked);
    }

    private void OnDisable()
    {
        _interactButton.onClick.RemoveListener(OnInteractButtonClicked);
    }

    private void OnInteractButtonClicked()
    {
        _interactable.Interact();
    }
}
