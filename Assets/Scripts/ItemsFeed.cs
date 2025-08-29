using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFeed : MonoBehaviour
{
    [SerializeField] Transform _startPosition;
    [SerializeField] Transform _endPosition;
    [SerializeField] List<BrainrotAsset> _possibleAssets;
    [SerializeField] float _distanceBetweenAssets;
    [SerializeField] private Transform _spawnedContainer;

    private List<BrainrotAsset> _currentBrainrotAssetsCollection = new List<BrainrotAsset>();

    private void Awake() 
    {
        FillStartAssets();
        SpawnStartAssets();
    }

    private void FillStartAssets()
    {
        int spawnCount = (int)(Vector3.Distance(_startPosition.position, _endPosition.position) / _distanceBetweenAssets);
        int collectionIndex = 0;
        for (int i = 0; i < spawnCount; i++)
        {
            if(collectionIndex >= _possibleAssets.Count)
            {
                collectionIndex = 0;
            }
            
            _currentBrainrotAssetsCollection.Add(_possibleAssets[collectionIndex]);
            collectionIndex++;
        }
    }

    private void SpawnStartAssets()
    {
        for (int i = 0; i < _currentBrainrotAssetsCollection.Count; i++)
        {
            float lerpd = Mathf.InverseLerp(0, _currentBrainrotAssetsCollection.Count, i);
            Vector3 spawnPosition = Vector3.Lerp(_startPosition.position, _endPosition.position, lerpd);
            var spawned = Instantiate(_currentBrainrotAssetsCollection[i], _spawnedContainer);
            spawned.transform.position = spawnPosition;
        }
    }
}
