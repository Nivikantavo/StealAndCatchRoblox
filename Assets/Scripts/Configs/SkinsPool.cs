using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsPool", menuName = "Configs/Skins/SkinsPool")]
public class SkinsPool : ScriptableObject
{
    public IReadOnlyList<Animator> Skins => _skins;

    [SerializeField] private List<Animator> _skins;

    public Animator GetRandomSkin()
    {
        int randomIndex = Random.Range(0, _skins.Count);
        return _skins[randomIndex];
    }
}
