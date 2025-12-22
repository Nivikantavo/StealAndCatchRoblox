using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenSkinChecker : IShopItemVisiter
{
    private PersistenData _persistenData;

    public bool IsOpened { get; private set; }

    public OpenSkinChecker(PersistenData persistenData) => _persistenData = persistenData;
    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkin)
    {
        IsOpened = _persistenData.UserData.OpenCharacterSkins.Contains(characterSkin.SkinType);
    }
}
