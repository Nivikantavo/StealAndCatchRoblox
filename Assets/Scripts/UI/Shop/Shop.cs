using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _characterSkinsButton;
    [SerializeField] private ShopCategoryButton _otherButton;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Image _selectedImage;

    [SerializeField] private ShopPanel _shopPanel;

    private IDataProvider _dataProvider;
    private ShopItemView _previewedItem;

    private CoinsWallet _coinsWallet;
    private DiamondsWallet _diamondsWallet;

    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinChecker _openSkinChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    private Wallet _targetWallet;

    [Inject]
    public void Construct(IDataProvider dataProvider, CoinsWallet coinsWallet, DiamondsWallet diamondsWallet,
                            OpenSkinChecker openSkinChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _dataProvider = dataProvider;
        _coinsWallet = coinsWallet;
        _diamondsWallet = diamondsWallet;
        _openSkinChecker = openSkinChecker;
        _selectedSkinChecker = selectedSkinChecker;
        _skinSelector = skinSelector;
        _skinUnlocker = skinUnlocker;

        _shopPanel.Initialize(_contentItems.CharacterSkinItems, _openSkinChecker, _selectedSkinChecker);

        OnCharacterSkinsButtonClick();
    }

    private void OnEnable()
    {
        _shopPanel.ItemViewClicked += OnItemViewClick;
        _buyButton.Click += OnBuyButtonClicked;
        _selectionButton.onClick.AddListener(OnSelectionButtonClicked);

        _characterSkinsButton.Click += OnCharacterSkinsButtonClick;
        _otherButton.Click += OnOtherButtonClick;
    }

    private void OnDisable()
    {
        _shopPanel.ItemViewClicked -= OnItemViewClick;
        _buyButton.Click -= OnBuyButtonClicked;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClicked);

        _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
        _otherButton.Click -= OnOtherButtonClick;
    }

    private void OnItemViewClick(ShopItemView itemView)
    {
        _previewedItem = itemView;

        _openSkinChecker.Visit(_previewedItem.Item);

        if (_openSkinChecker.IsOpened)
        {
            _selectedSkinChecker.Visit(_previewedItem.Item);
            if (_selectedSkinChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }
            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewedItem.Price, _previewedItem.Item.CurrencyType);
        }
    }

    private void OnBuyButtonClicked()
    {
        if (_targetWallet.IsEnough(_previewedItem.Price))
        {
            _targetWallet.Spend(_previewedItem.Price);
            _skinUnlocker.Visit(_previewedItem.Item);
            SelectSkin();
            _previewedItem.Unlock();
            _dataProvider.Save();
        }
    }

    private void OnSelectionButtonClicked()
    {
        SelectSkin();

        _dataProvider.Save();
    }

    private void OnOtherButtonClick()
    {
        _otherButton.Select();
        _characterSkinsButton.Unselect();
        //_shopPanel.Show(_contentItems.OtherContent.Cast<ShopItem>());
    }

    private void OnCharacterSkinsButtonClick()
    {
        _characterSkinsButton.Select();
        _otherButton.Unselect();
        _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>());
    }

    private void SelectSkin()
    {
        _skinSelector.Visit(_previewedItem.Item);
        _shopPanel.Select(_previewedItem);
        ShowSelectedText();
    }

    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }

    private void ShowSelectedText()
    {
        _selectedImage.gameObject.SetActive(true);
        HideSelectionButton();
        HideBuyButton();
    }

    private void ShowBuyButton(int price, Currency currency)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price, currency);

        _targetWallet = currency == Currency.Coins ? _coinsWallet : _diamondsWallet;

        if (_targetWallet.IsEnough(price))
        {
            _buyButton.Unlock();
        }
        else
        {
            _buyButton.Lock();
        }

        HideSelectionButton();
        HideSelectedText();
    }


    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedImage.gameObject.SetActive(false);
}
