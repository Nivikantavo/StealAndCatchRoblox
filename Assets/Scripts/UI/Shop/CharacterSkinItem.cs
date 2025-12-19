using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkin", menuName = "Shop/CharacterSkin")]
public class CharacterSkinItem : ShopItem
{
    [field: SerializeField] public CharacterSkin SkinType { get; private set; }
}
