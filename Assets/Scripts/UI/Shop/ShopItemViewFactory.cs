using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _viewTemplate;

    public ShopItemView Get(ShopItem shopItem, Transform transform)
    {
        ShopItemView spawned;
        spawned = Instantiate(_viewTemplate, transform);
        spawned.Initialize(shopItem);
        return spawned;
    }
}
