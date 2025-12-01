using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LeagueMissionsConfig", menuName = "Configs/Mission/LeagueMissionsConfig")]
public class LeagueMissionsConfig : ScriptableObject
{
    [SerializeField] private List<LeagueMission> _leagueMissions;

    public LeagueMission GetMission(League league)
    {
        return _leagueMissions.First(mission => mission.League == league);
    }
}
