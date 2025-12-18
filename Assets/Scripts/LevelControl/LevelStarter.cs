using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelStarter : MonoBehaviour
{
    private List<HousePlace> _housePlaces;
    private NavMeshSurface _navMeshSurface;
    private UserPlayer _userPlayer;
    private PlayerHouse _playerHouse;
    private List<BotsHouse> _botsHouses;
    private List<BotPlayer> _bots;
    private SkinsPoolConfig _skinsPool;
    private BotsNicknameContainer _botNicknameContainer;

    [Inject]
    private void Construct(UserPlayer userPlayer, PlayerHouse playerHouse, NavMeshSurface navMesh, 
        List<HousePlace> housePlaces, List<BotsHouse> botsHouses, List<BotPlayer> bots, 
        SkinsPoolConfig skinsPool)
    {
        _userPlayer = userPlayer;
        _playerHouse = playerHouse;
        _navMeshSurface = navMesh;
        _housePlaces = housePlaces;
        _botsHouses = botsHouses;
        _bots = bots;
        _skinsPool = skinsPool;

        _botNicknameContainer = new BotsNicknameContainer();
    }

    private void Start()
    {
        StartLevel();
    }

    public void RestartLevel()
    {
        foreach(HousePlace housePlace in _housePlaces)
        {
            housePlace.Clear();
        }

        _playerHouse.Restart();
        foreach (var botsHouses in _botsHouses)
        {
            botsHouses.Restart();
        }
        FindObjectOfType<ItemsFeed>().Restart();

        StartLevel();
    }

    private void StartLevel()
    {
        PutHousesInPlace();
    }

    private void PutHousesInPlace()
    {
        PutPlayerHouseOnPlace();
        PutBotsHousesOnPlace();
        _navMeshSurface.BuildNavMesh();
    }

    private void PutPlayerHouseOnPlace()
    {
        _userPlayer.gameObject.SetActive(false);
        int randomIndex = Random.Range(0, _housePlaces.Count);
        _playerHouse.transform.position = _housePlaces[randomIndex].transform.position;
        _playerHouse.transform.rotation = _housePlaces[randomIndex].transform.rotation;
        _housePlaces[randomIndex].SetHouse(_playerHouse);

        _userPlayer.transform.position = _playerHouse.OwnerSpawnPosition.position;
        _userPlayer.gameObject.SetActive(true);
    }

    private void PutBotsHousesOnPlace()
    {
        List<string> names = _botNicknameContainer.GetSeveralUniqueNicknames(_bots.Count);
        for (int i = 0; i < _housePlaces.Count - 1; i++)
        {
            _bots[i].gameObject.SetActive(false);
            var housePlace = _housePlaces.FirstOrDefault(place => place.HasHouse == false);

            _botsHouses[i].transform.position = housePlace.transform.position;
            _botsHouses[i].transform.rotation = housePlace.transform.rotation;
            housePlace.SetHouse(_botsHouses[i]);

            _bots[i].transform.position = _botsHouses[i].OwnerSpawnPosition.position;
            _bots[i].gameObject.SetActive(true);

            _botsHouses[i].Initialzie(_bots[i], i + 8, names[i]);
            _bots[i].SetNewSkin(_skinsPool.GetRandomSkin());
        }
    }
}
