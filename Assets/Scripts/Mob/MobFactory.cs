using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobFactory
{
    public IReadOnlyList<BrainrotMob> SpawnedMobCollection => _currentBrainrotAssetsCollection;
    public int MobsTypeCount => _possibleAssets.Count;

    private List<BrainrotMob> _currentBrainrotAssetsCollection = new List<BrainrotMob>();
    private Transform _spawnedContainer;
    private List<BrainrotMobConfig> _possibleAssets;
    private BrainrotMob _mobTemplate;

    public MobFactory(Transform spawnedContainer, List<BrainrotMobConfig> possibleAssets, BrainrotMob mobTemplate)
    {
        _spawnedContainer = spawnedContainer;
        _possibleAssets = possibleAssets;
        _mobTemplate = mobTemplate;
    }

    public BrainrotMob GetMob()
    {
        BrainrotMob resultBrainrot = _currentBrainrotAssetsCollection.FirstOrDefault(brainrot => brainrot.gameObject.activeInHierarchy == false);

        return SpawnOrReset(resultBrainrot, GetMissingConfig());
    }

    public BrainrotMob GetMob(BrainrotMobConfig config)
    {
        BrainrotMob resultBrainrot = _currentBrainrotAssetsCollection.FirstOrDefault(brainrot => brainrot.Config == config && brainrot.gameObject.activeInHierarchy == false);

        return SpawnOrReset(resultBrainrot, config);
    }

    private BrainrotMob SpawnOrReset(BrainrotMob targetMob, BrainrotMobConfig config)
    {
        if (targetMob == null)
        {
            targetMob = SpawnMob(config);
        }
        else
        {
            targetMob.ResetMob();
        }
        return targetMob;
    }

    private BrainrotMobConfig GetMissingConfig()
    {
        BrainrotMobConfig missingConfig;

        var usedConfigs = _currentBrainrotAssetsCollection.Select(mob => mob.Config).ToHashSet();
        missingConfig = _possibleAssets.FirstOrDefault(config => !usedConfigs.Contains(config));

        if (missingConfig == null)
        {
            missingConfig = _possibleAssets[Random.Range(0, _possibleAssets.Count)];

        }
        return missingConfig;
    }

    private BrainrotMob SpawnMob(BrainrotMobConfig config)
    {
        var spawned = UnityEngine.Object.Instantiate(_mobTemplate, _spawnedContainer);
        spawned.Initialize(config);

        _currentBrainrotAssetsCollection.Add(spawned);

        return spawned;
    }
}
