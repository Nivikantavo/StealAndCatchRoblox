using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : IShopItemVisiter
{
    private IPersistenData _persistenData;

    public SkinUnlocker(IPersistenData persistenData) => _persistenData = persistenData;

    public void Visit(ShopItem shopItem)
    {
        Visit((dynamic)shopItem);
    }

    public void Visit(CharacterSkinItem characterSkin)
    {
        _persistenData.UserData.OpenCharacterSkin(characterSkin.SkinType);
    }
}
