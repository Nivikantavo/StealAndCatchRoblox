using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ItemsFeed : MonoBehaviour
{
    [SerializeField] Transform _startPosition;
    [SerializeField] ItemsFeedEndPoint _endPosition;
    [SerializeField] List<BrainrotMobConfig> _possibleAssets;
    [SerializeField] float _distanceBetweenAssets;
    [SerializeField] float _timeBetweenSpawn;

    private MobFactory _mobFactory;

    private bool _isSpawning = false;

    [Inject]
    private void Construct(MobFactory mobFactory)
    {
        _mobFactory = mobFactory;
    }

    private async void Start() 
    {
        await UniTask.Delay(100);
        
        StartSpawning();
    }

    public void Restart()
    {
        foreach (var mob in _mobFactory.SpawnedMobCollection)
        {
            mob.gameObject.SetActive(false);
        }
        StartSpawning();
    }

    private async void StartSpawning()
    {
        SpawnStartMobs();
        SendBrainrotMob();

        _isSpawning = true;

        await SpawnOnCooldown();
    }

    private void SpawnStartMobs()
    {
        int spawnCount = (int)(Vector3.Distance(_startPosition.position, _endPosition.transform.position) / _distanceBetweenAssets);

        int collectionIndex = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            if (collectionIndex >= _mobFactory.MobsTypeCount)
            {
                collectionIndex = 0;
            }

            float lerpd = Mathf.InverseLerp(0, spawnCount, i);
            Vector3 spawnPosition = Vector3.Lerp(_startPosition.position, _endPosition.transform.position, lerpd);

            var spawned = _mobFactory.GetMob(_possibleAssets[collectionIndex]);
            spawned.transform.position = spawnPosition;
            spawned.gameObject.SetActive(true);
            collectionIndex++;
        }
    }

    private void SendBrainrotMob()
    {
        for (int i = 0; i < _mobFactory.SpawnedMobCollection.Count; i++)
        {
            _mobFactory.SpawnedMobCollection[i].SetDestanation(_endPosition.transform.position);
        }
    }

    private async UniTask SpawnOnCooldown()
    {
        while (_isSpawning)
        {
            await UniTask.WaitForSeconds(_timeBetweenSpawn);

            var spawned = _mobFactory.GetMob();
            spawned.transform.position = _startPosition.position;
            spawned.gameObject.SetActive(true);
            spawned.SetDestanation(_endPosition.transform.position);
        }
    }
}
