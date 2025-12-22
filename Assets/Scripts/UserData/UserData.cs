using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class UserData
{
    public string UserName;
    
    public League CurrentLeague;
    private int _money;
    private int _diamonds;
    private CharacterSkin _currentCharacterSkin;
    private List<CharacterSkin> _openCharacterSkins;

    public UserData()
    {
        CurrentLeague = League.Bronze5;
        _money = 10000;
        _diamonds = 1500;
        _currentCharacterSkin = CharacterSkin.Becon;
        _openCharacterSkins = new List<CharacterSkin>() { _currentCharacterSkin };
    }

    [JsonConstructor]
    public UserData(int money, int diamonds, League league, CharacterSkin currentSkin, List<CharacterSkin> openSkins)
    {
        CurrentLeague = league;
        _money = money;
        _diamonds = diamonds;
        _currentCharacterSkin = currentSkin;
        _openCharacterSkins = openSkins;
    }

    public int Money
    {
        get => _money;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _money = value;
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
