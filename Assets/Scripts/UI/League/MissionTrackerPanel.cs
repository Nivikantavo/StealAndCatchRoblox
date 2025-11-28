using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrackerPanel : MonoBehaviour
{
    [SerializeField] private Slider _moneyMissionProgressView;
    [SerializeField] private MissionTargetMobView _targetMobViewTemplate;
    [SerializeField] private Transform _missionsContent;

    private List<MissionTargetMobView> _targetMobViews = new List<MissionTargetMobView>();

    public void Initialize(float targetMoneyValue, List<MissionTargetMob> targetMobs)
    {
        _moneyMissionProgressView.maxValue = targetMoneyValue;
        _moneyMissionProgressView.value = 0;

        for (int i = 0; i < targetMobs.Count; i++)
        {
            if(_targetMobViews[i] != null)
            {
                _targetMobViews[i].gameObject.SetActive(true);
                _targetMobViews[i].Initialize(targetMobs[i]);
            }
            else
            {
                var spawnedView = Instantiate(_targetMobViewTemplate, _missionsContent);
                spawnedView.Initialize(targetMobs[i]);
            }
        }

        for (int i = targetMobs.Count; i < _targetMobViews.Count; i++)
        {
            _targetMobViews[i].gameObject.SetActive(false);
        }
    }

    public void SetMoneyValue(float moneyValue)
    {
        _moneyMissionProgressView.value = moneyValue;
    }

    public void SetMissionComplited(MissionTargetMob missionTargetMob)
    {
        _targetMobViews.FirstOrDefault(view => view.MissionTargetMob == missionTargetMob).SetComplited();
    }
}
