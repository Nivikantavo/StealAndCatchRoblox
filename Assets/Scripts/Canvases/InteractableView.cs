using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableView : MonoBehaviour
{
    public Action OnInteract;

    [SerializeField] private TextMeshProUGUI _interactLabel;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Image _interactIcon;

    private IInteractable _interactable;
    private IInteractor _interactor;

    public void Initialize(IInteractor interactor)
    {
        _interactor = interactor;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SetInteractable(IInteractable interactable)
    {
        _interactable = interactable;

        transform.localScale = interactable != null ? Vector3.one : Vector3.zero;
    }

    private void Update()
    {
        if (_interactable == null)
            return;

        transform.position = Vector3.Lerp(_interactor.SelfTransform.position, _interactable.SelfTransform.position, 0.5f) + Vector3.up * 2;

        var direction = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        //transform.LookAt(direction);
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
        Debug.Log("Interact button clicked");
        OnInteract?.Invoke();
    }
}
