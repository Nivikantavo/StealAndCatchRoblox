using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : IShopItemVisiter
{
    private PersistenData _persistenData;
    public SkinSelector(PersistenData persistenData) => _persistenData = persistenData;
    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);
    public void Visit(CharacterSkinItem characterSkin)
    {
        _persistenData.UserData.CurrentCharacterSkin = characterSkin.SkinType;
    }
}
