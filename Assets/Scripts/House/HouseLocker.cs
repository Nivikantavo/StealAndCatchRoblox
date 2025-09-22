using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocker : MonoBehaviour
{
    public bool IsClosed => _lock.gameObject.activeSelf;

    [SerializeField] private HouseLockerButton _lockButton;
    [SerializeField] private GameObject _lock;

    private Player _owner;

    public void Initialize(Player player)
    {
        _owner = player;
        SetLayerRecursively(_lock.transform, _owner.gameObject.layer);
        _lock.SetActive(false);
    }

    private void OnEnable()
    {
        _lockButton.LockerButtonWasClicked += OnLockerButtonWasClicked;
    }

    private void OnDisable()
    {
        _lockButton.LockerButtonWasClicked -= OnLockerButtonWasClicked;
    }

    private void OnLockerButtonWasClicked(Player player)
    {
        if (IsClosed) return;
        if (player != _owner) return;

        SetClosed(60);
    }

    private async void SetClosed(float timeInSeconds)
    {
        await CloseHouseForTime(timeInSeconds);
    }

    private async UniTask CloseHouseForTime(float timeInSeconds)
    {
        _lock.SetActive(true);
        await UniTask.WaitForSeconds(timeInSeconds);
        _lock.SetActive(false);
    }

    private void SetLayerRecursively(Transform parent, int layer)
    {
        parent.gameObject.layer = layer;

        foreach (Transform child in parent)
        {
            SetLayerRecursively(child, layer);
        }
    }
}
