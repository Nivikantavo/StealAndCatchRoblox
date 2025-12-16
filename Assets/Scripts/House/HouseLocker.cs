using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocker : MonoBehaviour
{
    public bool IsClosed => _lock.gameObject.activeSelf;
    public Transform LockButtonPosition => _lockButton.transform;

    [SerializeField] private HouseLockerButton _lockButton;
    [SerializeField] private GameObject _lock;
    [SerializeField] private float _lockTime = 10;

    private Player _owner;
    private Coroutine _closedCoroutine;

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

    public void Restart()
    {
        _lock.SetActive(false);
        if(_closedCoroutine != null)
        {
            StopCoroutine(_closedCoroutine);
        }
    }

    private void OnLockerButtonWasClicked(Player player)
    {
        if (IsClosed) return;
        if (player != _owner) return;

        SetClosed(_lockTime);
    }

    private void SetClosed(float timeInSeconds)
    {
        _closedCoroutine = StartCoroutine(CloseHouseForTime(timeInSeconds));
    }

    private IEnumerator CloseHouseForTime(float timeInSeconds)
    {
        _lock.SetActive(true);
        yield return new WaitForSeconds(timeInSeconds);
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
