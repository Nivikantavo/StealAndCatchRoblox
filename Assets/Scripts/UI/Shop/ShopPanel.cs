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
        Initialize(_shopContent.CharacterSkinItems);
    }

    public void Initialize(IEnumerable<CharacterSkinItem> content)
    {
        foreach (var item in content)
        {
            ShopItemView spawnedItemView = _shopItemViewFactory.Get(item, _itemsContainer);
            spawnedItemView.ShopItemViewClick += OnShopItemViewClicked;
            
            spawnedItemView.Unselect();

            //TODO: ƒобавить проверку открытости скина.

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
        throw new System.NotImplementedException();
    }
}
