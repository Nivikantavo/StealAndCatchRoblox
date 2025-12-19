using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameObject Skin {  get; private set; }
    [field: SerializeField] public Sprite Preview { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Currency CurrencyType { get; private set; }
}
