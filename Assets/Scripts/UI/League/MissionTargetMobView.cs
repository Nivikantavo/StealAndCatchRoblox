using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTargetMobView : MonoBehaviour
{
    public bool Complited { get; private set; }
    public MissionTargetMob MissionTargetMob { get; private set; }

    [SerializeField] private Image _mobView;
    [SerializeField] private GameObject _complitedCheck;

    public void Initialize(MissionTargetMob missionTargetMob)
    {
        Complited = false;
        MissionTargetMob = missionTargetMob;
        _mobView.sprite = MissionTargetMob.TargetPreview;
        _complitedCheck.SetActive(Complited);
    }

    public void SetComplited()
    {
        Complited = true;
        _complitedCheck.SetActive(Complited);
    }
}
