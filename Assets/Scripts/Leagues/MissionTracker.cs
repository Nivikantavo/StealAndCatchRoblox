using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissionTracker : MonoBehaviour
{
    private LeagueMission _currentMission;
    private UserPlayer _player;
    private PlayerHouse _playerHouse;
    private MissionTrackerPanel _missionTrackerPanel;

    [Inject]
    private void Construct(UserPlayer player, PlayerHouse playerHouse, MissionTrackerPanel missionTrackerPanel)
    {
        _player = player;
        _playerHouse = playerHouse;
        _missionTrackerPanel = missionTrackerPanel;
    }

    private void OnMoneyValueChange()
    {

    }

    private void OnMobCollectionChange()
    {

    }

}
