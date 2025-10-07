using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyView;

    private UserPlayer _player;

    [Inject]
    private void Construct(UserPlayer player)
    {
        _player = player;
        _player.Wallet.MoneyCountChanged += OnMoneyCountChanged;
    }

    private void OnDisable()
    {
        _player.Wallet.MoneyCountChanged -= OnMoneyCountChanged;
    }

    private void OnMoneyCountChanged(int newValue)
    {
        _moneyView.text = newValue.ToString();
    }
}
