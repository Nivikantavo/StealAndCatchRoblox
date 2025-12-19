using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueView<T> : MonoBehaviour where T : IConvertible
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _currencyView;

    public void Show(T value)
    {
        gameObject.SetActive(true);
        _text.text = CutBack(value);
        _currencyView.gameObject.SetActive(false);
    }

    public void Show(T value, Sprite sprite)
    {
        gameObject.SetActive(true);
        _text.text = CutBack(value);

        _currencyView.gameObject.SetActive(true);
        _currencyView.sprite = sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    protected virtual string CutBack(T value)
    {
        return value.ToString();
    }
}
