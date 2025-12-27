using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _viewTemplate;
    //[SerializeField] private ShopItemView _otherViewTemplate;

    public ShopItemView Get(ShopItem shopItem, Transform transform)
    {
        ShopItemView spawned;
        Debug.Log($"{_viewTemplate == null} - параметр фабрики null");
        //ShopItemVisiter visiter = new ShopItemVisiter(_viewTemplate);
        //visiter.Visit(shopItem);

        //spawned = Instantiate(visiter.Prefab, transform);
        spawned = Instantiate(_viewTemplate, transform);
        spawned.Initialize(shopItem);
        return spawned;
    }

    private class ShopItemVisiter : IShopItemVisiter
    {
        private ShopItemView _characterSkinTemplate;
        //private ShopItemView _otherViewTemplate;

        public ShopItemVisiter(ShopItemView skinView)
        {
            _characterSkinTemplate = skinView;
            Debug.Log($"{skinView} - передали {_characterSkinTemplate} - сохранили");
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkin)
        {
            Prefab = _characterSkinTemplate;
            Debug.Log(Prefab);
        }
        
    }
}
