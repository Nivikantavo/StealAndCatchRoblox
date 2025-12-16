using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MissionTracker : MonoBehaviour
{
    [SerializeField] private LeagueMissionsConfig _config;

    private LeagueMission _currentMission;
    private UserPlayer _player;
    private PlayerHouse _playerHouse;
    private MissionTrackerPanel _missionTrackerPanel;
    private UserData _userData;
    private LevelStarter _levelStarter; //TODO: убрать

    [Inject]
    private void Construct(UserPlayer player, PlayerHouse playerHouse, MissionTrackerPanel missionTrackerPanel, LevelStarter levelStarter)
    {
        _player = player;
        _playerHouse = playerHouse;
        _missionTrackerPanel = missionTrackerPanel;

        _userData = new UserData();//TODO: заправшивать данные с сервака
        _currentMission = _config.GetMission(_userData.CurrentLeague);
        _missionTrackerPanel.Initialize(_currentMission.MoneyMission.MoneyValueMission, _currentMission.MobMissions);
        _missionTrackerPanel.NextLeagueTransferButtonClicked += OnNextLeagueTransferButton;

        _player.Wallet.MoneyCountChanged += OnMoneyValueChange;
        _playerHouse.MobAdded += OnMobCollectionChange;

        _levelStarter = levelStarter; //TODO: убрать
    }

    private void OnDisable() //TODO: заменить на OnDispose
    {
        _player.Wallet.MoneyCountChanged -= OnMoneyValueChange;
        _playerHouse.MobAdded -= OnMobCollectionChange;
        _missionTrackerPanel.NextLeagueTransferButtonClicked -= OnNextLeagueTransferButton;
    }

    public bool AllMissionsComplited()
    {
        return _currentMission.MoneyMission.Complited && _currentMission.MobMissions.Exists(mission => !mission.Complited) == false;
    }

    private void OnMoneyValueChange(int newValue)
    {
        _missionTrackerPanel.SetMoneyValue(newValue);
        _currentMission.MoneyMission.Complited = _player.Wallet.Money >= _currentMission.MoneyMission.MoneyValueMission;

        if (AllMissionsComplited())
        {
            _missionTrackerPanel.ActivateNextLeagueTransferButton();
        }
    }

    private void OnMobCollectionChange(BrainrotMob mob)
    {
        var complitedMission = _currentMission.MobMissions.FirstOrDefault(mission => mission.TargetMob.Name == mob.Config.Name);
        if (complitedMission != null)
        {
            _missionTrackerPanel.MarkMissionViewComplited(mob.Config);
            complitedMission.Complited = true;
        }
    }

    private void OnNextLeagueTransferButton()
    {
        _userData.CurrentLeague++;
        _currentMission = _config.GetMission(_userData.CurrentLeague);
        _missionTrackerPanel.Initialize(_currentMission.MoneyMission.MoneyValueMission, _currentMission.MobMissions, _player.Wallet.Money);
        _levelStarter.RestartLevel();
    }
}
