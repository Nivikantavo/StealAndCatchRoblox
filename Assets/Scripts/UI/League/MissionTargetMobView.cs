using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionTargetMobView : MonoBehaviour
{
    public bool Complited { get; private set; }
    public BrainrotMobConfig MissionTargetMob { get; private set; }

    [SerializeField] private Image _mobView;
    [SerializeField] private GameObject _complitedCheck;
    [SerializeField] private TextMeshProUGUI _mobName;
    [SerializeField] private TextMeshProUGUI _mobRarity;

    public void Initialize(BrainrotMobConfig missionTargetMob)
    {
        Complited = false;
        MissionTargetMob = missionTargetMob;
        _mobView.sprite = MissionTargetMob.Preview;
        _mobName.text = MissionTargetMob.Name;
        _mobRarity.text = MissionTargetMob.Rarity.ToString();
        _complitedCheck.SetActive(Complited);
    }

    public void SetComplited()
    {
        Complited = true;
        _complitedCheck.SetActive(Complited);
    }
}
