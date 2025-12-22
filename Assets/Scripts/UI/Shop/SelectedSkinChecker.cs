using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedSkinChecker : IShopItemVisiter
{
    private PersistenData _persistenData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(PersistenData persistenData) => _persistenData = persistenData;
    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkin)
    {
        IsSelected = _persistenData.UserData.CurrentCharacterSkin == characterSkin.SkinType;
    }
}
