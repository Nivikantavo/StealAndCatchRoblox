using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;
    [SerializeField] private ShopContent _shopContent;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    private OpenSkinChecker _openSkinChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    private void Start()
    {
        Initialize(_shopContent.CharacterSkinItems);
    }

    public void Initialize(IEnumerable<CharacterSkinItem> content, OpenSkinChecker openSkinChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinChecker = openSkinChecker;
        _selectedSkinChecker = selectedSkinChecker;

        foreach (var item in content)
        {
            ShopItemView spawnedItemView = _shopItemViewFactory.Get(item, _itemsContainer);
            spawnedItemView.ShopItemViewClick += OnShopItemViewClicked;
            
            spawnedItemView.Unselect();

            //TODO: ƒобавить проверку открытости скина.

            _openSkinChecker.Visit(spawnedItemView.Item);
            if (_openSkinChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItemView.Item);
                if (_selectedSkinChecker.IsSelected)
                {
                    spawnedItemView.Select();
                    ItemViewClicked?.Invoke(spawnedItemView);
                }
                spawnedItemView.Unlock();
            }
            else
            {
                spawnedItemView.Lock();
            }

            _shopItems.Add(spawnedItemView);
        }
    }

    private void OnDestroy()
    {
        foreach(var item in _shopItems)
        {
            item.ShopItemViewClick -= OnShopItemViewClicked;
        }
    }

    private void OnShopItemViewClicked(ShopItemView view)
    {
        ItemViewClicked?.Invoke(view);
    }
}
