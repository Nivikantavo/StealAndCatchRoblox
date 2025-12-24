using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _coinCurrencyImage;
    [SerializeField] private Image _diamondCurrencyImage;

    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unlockColor;

    [SerializeField] private float _lockAnimationDuration = 0.4f;
    [SerializeField] private float _lockAnimationStrength = 2f;

    private bool _locked = false;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void UpdateText(int price, Currency currency)
    {
        _text.text = price.ToString();
        SwitchCurrency(currency);
    }

    public void Lock()
    {
        _locked = true;
        _text.color = _lockColor;
    }

    public void Unlock()
    {
        _locked = false;
        _text.color = _unlockColor;
    }

    private void OnButtonClick()
    {
        if (_locked)
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            return;
        }
        Click?.Invoke();
    }

    private void SwitchCurrency(Currency currency)
    {
        switch (currency)
        {
            case Currency.Coins:
                _coinCurrencyImage.gameObject.SetActive(true);
                _diamondCurrencyImage.gameObject.SetActive(false);
                break;
            case Currency.Diamonds:
                _coinCurrencyImage.gameObject.SetActive(false);
                _diamondCurrencyImage.gameObject.SetActive(true);
                break;
        }
    }
}
