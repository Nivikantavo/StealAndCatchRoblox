using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeagueMission
{
    [field: SerializeField] public League League { get; private set; }
    [field: SerializeField] public MoneyMission MoneyMission;
    [field: SerializeField] public List<MobMission> MobMissions { get; private set; }
}

[Serializable]
public class Mission
{
    [HideInInspector] public bool Complited;
}

[Serializable]
public class MoneyMission : Mission
{
    [field: SerializeField] public int MoneyValueMission { get; private set; }
}

[Serializable]
public class MobMission : Mission
{
    [field: SerializeField] public BrainrotMobConfig TargetMob { get; private set; }
}
