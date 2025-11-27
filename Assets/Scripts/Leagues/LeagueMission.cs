using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeagueMission", menuName = "Configs/Mission/LeagueMission")]
public class LeagueMission : ScriptableObject
{
    [SerializeField] private int _moneyValueMission;
    [SerializeField] private List<MissionTargetMob> _missionTargets;
}

[Serializable]
public class MissionTargetMob
{
    public Sprite TargetPreview;
    public BrainrotMobConfig BrainrotMobConfig;
}
