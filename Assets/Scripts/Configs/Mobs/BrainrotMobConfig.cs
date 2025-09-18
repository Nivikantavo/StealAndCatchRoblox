using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrainrotMobConfig", menuName = "Configs/Mobs/BrainrotMobConfig")]
public class BrainrotMobConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float BaseCost { get; private set; }
    [field: SerializeField] public float ValuePerSecond { get; private set; }
    [field: SerializeField] public GameObject MobPrefab { get; private set; }
}
