using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class UserData
{
    public string UserName;
    
    public League CurrentLeague;
    private int _inLastGameCoins;
    private int _coins;
    private int _diamonds;
    private CharacterSkin _currentCharacterSkin;
    private List<CharacterSkin> _openCharacterSkins;

    public UserData()
    {
        CurrentLeague = League.Bronze5;
        _inLastGameCoins = 1000;
        _coins = 10000;
        _diamonds = 1500;
        _currentCharacterSkin = CharacterSkin.Becon;
        _openCharacterSkins = new List<CharacterSkin>() { _currentCharacterSkin };
    }

    [JsonConstructor]
    public UserData(int money, int diamonds, League league, CharacterSkin currentSkin, List<CharacterSkin> openSkins)
    {
        _inLastGameCoins = 1000;
        CurrentLeague = league;
        _coins = money;
        _diamonds = diamonds;
        _currentCharacterSkin = currentSkin;
        _openCharacterSkins = openSkins;
    }

    public int Coins
    {
        get => _coins;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _coins = value;
        }
    }

    public int Diamonds
    {
        get => _diamonds;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _diamonds = value;
        }
    }

    public int InLastGameCoins
    {
        get => _inLastGameCoins;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _inLastGameCoins = value;
        }
    }

    public CharacterSkin CurrentCharacterSkin
    {
        get => _currentCharacterSkin;
        set
        {
            if(_openCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _currentCharacterSkin = value;
        }
    }

    public IEnumerable<CharacterSkin> OpenCharacterSkins => _openCharacterSkins;

    public void OpenCharacterSkin(CharacterSkin characterSkin)
    {
        if (_openCharacterSkins.Contains(characterSkin))
            throw new ArgumentException(nameof(characterSkin));

        _openCharacterSkins.Add(characterSkin);
    }
}
