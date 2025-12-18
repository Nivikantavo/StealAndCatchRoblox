using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsPoolConfig", menuName = "Configs/Equipment/SkinsPoolConfig")]
public class SkinsPoolConfig : ScriptableObject
{
    [SerializeField] private List<Animator> _skins;

    public Animator GetRandomSkin()
    {
        int randomIndex = Random.Range(0, _skins.Count);
        return _skins[randomIndex];
    }
}
