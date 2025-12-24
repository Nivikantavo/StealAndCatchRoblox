using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;

    [SerializeField] private Sprite _inGameCoinImage;//TODO: вывести в отельный класс
    [SerializeField] private Sprite _coinImage;
    [SerializeField] private Sprite _diamondsImage;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _contentImage;
    [SerializeField] private IntValueView _priceView;

    [SerializeField] private GameObject _selectedBaner;
    [SerializeField] private GameObject _sellBaner;
    [SerializeField] private IntValueView _previousPriceView;
    [SerializeField] private GameObject _newBaner;

    public ShopItem Item { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsLocked { get; private set; }
    public int Price => Item.Price;
    public GameObject Model => Item.Skin;

    public void Initialize(ShopItem item)
    {
        _backgroundImage.sprite = _standartBackground;
        Item = item;
        _contentImage.sprite = item.Preview;
        _priceView.Show(Price, GetNeedCurrencyView(Item.CurrencyType));
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLocked = true;
        _priceView.Show(Price, GetNeedCurrencyView(Item.CurrencyType));
    }

    public void Unlock()
    {
        IsLocked = false;
        _priceView.Hide();
    }

    public void Select()
    {
        IsSelected = true;
        _selectedBaner.SetActive(true);
    }

    public void Unselect()
    {
        IsSelected = false;
        _selectedBaner.SetActive(false);
    }

    public void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public void Unhighlight() => _backgroundImage.sprite = _standartBackground;

    private Sprite GetNeedCurrencyView(Currency currency)
    {
        switch(currency)
        {
            case Currency.InGameCoins: return _inGameCoinImage;
            case Currency.Coins: return _coinImage;
            case Currency.Diamonds: return _diamondsImage;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}
