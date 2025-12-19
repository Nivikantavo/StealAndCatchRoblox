using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;
    [SerializeField] private ShopContent _shopContent;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    private void Start()
    {
        Initialize(_shopContent, _shopItemViewFactory);
    }

    public void Initialize(ShopContent content, ShopItemViewFactory factory)
    {
        _shopItemViewFactory = factory;
        foreach (var item in content.CharacterSkinItems)
        {
            _shopItems.Add(_shopItemViewFactory.Get(item, _itemsContainer));
        }
    }
}
