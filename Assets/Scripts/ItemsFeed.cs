using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsFeed : MonoBehaviour
{
    [SerializeField] Transform _startPosition;
    [SerializeField] ItemsFeedEndPoint _endPosition;
    [SerializeField] List<BrainrotMobConfig> _possibleAssets;
    [SerializeField] float _distanceBetweenAssets;
    [SerializeField] private Transform _spawnedContainer;
    [SerializeField] private BrainrotMob _mobTemplate;

    private List<BrainrotMob> _currentBrainrotAssetsCollection = new List<BrainrotMob>();

    private float _timeBetweenSpawn;
    private bool _isSpawning = false;

    private void Awake() 
    {
        SpawnStartMobs();
        SendBrainrotMob();

        _timeBetweenSpawn = _distanceBetweenAssets / _mobTemplate.Speed;
        _isSpawning = true;

        StartSpawning();
    }

    private async void StartSpawning()
    {
        await SpawnOnCooldown();
    }

    private void SpawnStartMobs()
    {
        int spawnCount = (int)(Vector3.Distance(_startPosition.position, _endPosition.transform.position) / _distanceBetweenAssets);
        int collectionIndex = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            if (collectionIndex >= _possibleAssets.Count)
            {
                collectionIndex = 0;
            }

            float lerpd = Mathf.InverseLerp(0, spawnCount, i);
            Vector3 spawnPosition = Vector3.Lerp(_startPosition.position, _endPosition.transform.position, lerpd);
            SpawnMob(spawnPosition, _possibleAssets[collectionIndex]);
            collectionIndex++;
        }
    }

    private void SendBrainrotMob()
    {
        for (int i = 0; i < _currentBrainrotAssetsCollection.Count; i++)
        {
            _currentBrainrotAssetsCollection[i].SetDestanation(_endPosition.transform.position);
        }
    }

    private async UniTask SpawnOnCooldown()
    {
        while (_isSpawning)
        {
            await UniTask.WaitForSeconds(_timeBetweenSpawn);

            var spawned = GetMob();
            spawned.transform.position = _startPosition.position;
            spawned.gameObject.SetActive(true);
            spawned.SetDestanation(_endPosition.transform.position);
        }
    }

    private BrainrotMob GetMob()
    {
        BrainrotMob resultBrainrot = _currentBrainrotAssetsCollection.FirstOrDefault(brainrot => brainrot.gameObject.activeInHierarchy == false);

        if (resultBrainrot == null)
        {
            resultBrainrot = SpawnMob(_startPosition.position, GetMissingConfig());
        }
        return resultBrainrot;
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

    private BrainrotMob SpawnMob(Vector3 spawnPosition, BrainrotMobConfig config)
    {
        var spawned = Instantiate(_mobTemplate, _spawnedContainer);
        spawned.transform.position = spawnPosition;

        spawned.Initialize(config);

        _currentBrainrotAssetsCollection.Add(spawned);

        return spawned;
    }
}
