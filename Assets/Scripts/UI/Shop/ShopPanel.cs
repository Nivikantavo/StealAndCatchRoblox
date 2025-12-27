using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    [SerializeField] private Transform _itemsContainer;
    private ShopItemViewFactory _shopItemViewFactory;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    private OpenSkinChecker _openSkinChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    [Inject]
    private void Construct(ShopItemViewFactory factory)
    {
        _shopItemViewFactory = factory;
    }

    public void Initialize(IEnumerable<CharacterSkinItem> content, OpenSkinChecker openSkinChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinChecker = openSkinChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    public void Show(IEnumerable<ShopItem> content)
    {
        Clear();
        foreach (var item in content)
        {
            ShopItemView spawnedItemView = _shopItemViewFactory.Get(item, _itemsContainer);
            spawnedItemView.Click += OnShopItemViewClicked;

            spawnedItemView.Unselect();
            spawnedItemView.Unhighlight();

            _openSkinChecker.Visit(spawnedItemView.Item);
            if (_openSkinChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItemView.Item);
                if (_selectedSkinChecker.IsSelected)
                {
                    spawnedItemView.Select();
                    spawnedItemView.Highlight();
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

    public void Select(ShopItemView itemView)
    {
        foreach(var item in _shopItems)
        {
            item.Unselect();
        }
        itemView.Select();
    }

    private void OnDestroy()
    {
        foreach(var item in _shopItems)
        {
            item.Click -= OnShopItemViewClicked;
        }
    }

    private void OnShopItemViewClicked(ShopItemView view)
    {
        Highlight(view);
        ItemViewClicked?.Invoke(view);
    }

    private void Highlight(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
        {
            item.Unhighlight();
        }
        itemView.Highlight();
    }

    private void Clear()
    {
        foreach (var item in _shopItems)
        {
            item.Click -= OnShopItemViewClicked;
            Destroy(item.gameObject);
        }
        _shopItems.Clear();
    }
}
