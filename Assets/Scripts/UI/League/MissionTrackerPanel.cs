using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionTrackerPanel : MonoBehaviour
{
    public event Action NextLeagueTransferButtonClicked;

    [SerializeField] private Slider _moneyMissionProgressView;
    [SerializeField] private MissionTargetMobView _targetMobViewTemplate;
    [SerializeField] private Transform _missionsContent;
    [SerializeField] private TextMeshProUGUI _moneyMissionProgressText;
    [SerializeField] private Button _nextLeagueTransferButton;

    private List<MissionTargetMobView> _targetMobViews = new List<MissionTargetMobView>();
    private string _moneyProgressTextFormat  = "Money: {0}/{1}";
    private int _targetMoneyValue;

    private void Awake()
    {
        _nextLeagueTransferButton.onClick.AddListener(OnNextLeagueTransferButtonClicked);
    }

    private void OnDestroy()
    {
        _nextLeagueTransferButton.onClick.RemoveListener(OnNextLeagueTransferButtonClicked);
    }

    public void Initialize(int targetMoneyValue, List<MobMission> targetMobs, int currentValue = 0)
    {
        _targetMoneyValue = targetMoneyValue;
        _moneyMissionProgressView.maxValue = targetMoneyValue;
        _moneyMissionProgressView.value = 0;

        SetMoneyValue(currentValue);

        for (int i = 0; i < targetMobs.Count; i++)
        {
            if(_targetMobViews.Count > i)
            {
                if (_targetMobViews[i] != null)
                {
                    _targetMobViews[i].gameObject.SetActive(true);
                    _targetMobViews[i].Initialize(targetMobs[i].TargetMob);
                }
            }
            else
            {
                var spawnedView = Instantiate(_targetMobViewTemplate, _missionsContent);
                spawnedView.Initialize(targetMobs[i].TargetMob);
                _targetMobViews.Add(spawnedView);
            }
        }

        for (int i = targetMobs.Count; i < _targetMobViews.Count; i++)
        {
            _targetMobViews[i].gameObject.SetActive(false);
        }

        _nextLeagueTransferButton.gameObject.SetActive(false);
    }

    public void SetMoneyValue(float moneyValue)
    {
        _moneyMissionProgressView.value = moneyValue;
        _moneyMissionProgressText.text = string.Format(_moneyProgressTextFormat, moneyValue, _targetMoneyValue);
    }

    public void MarkMissionViewComplited(BrainrotMobConfig brainrotMobConfig)
    {
        _targetMobViews.First(view => view.MissionTargetMob.Name == brainrotMobConfig.Name).SetComplited();
    }

    public void ActivateNextLeagueTransferButton()
    {
        _nextLeagueTransferButton.gameObject.SetActive(true);
    }

    private void OnNextLeagueTransferButtonClicked()
    {
        NextLeagueTransferButtonClicked?.Invoke();
    }
}
