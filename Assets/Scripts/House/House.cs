using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool IsClosed => _lock.gameObject.activeSelf;
    public bool  HasFreeHolder => _mobHolders.Exists(holder => holder.IsFree);

    [SerializeField] private List<MobHolder> _mobHolders;
    [SerializeField] private HouseLockerButton _lockButton;
    [SerializeField] private GameObject _lock;

    //TO DO: вынести в Construct
    [SerializeField] private int _layerNumber;
    [SerializeField] private Player _owner;

    private void Awake()
    {
        Construct();
    }

    private void Construct()
    {
        SetLayerRecursively(_lock.transform, _layerNumber);
        _owner.gameObject.layer = _layerNumber;

        foreach (var mobHolder in _mobHolders)
        {
            mobHolder.Initialize(_owner);
        }
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
        if(player != _owner) return;

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
