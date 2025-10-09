using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{
    public List<IFighter> Attack();
    public List<IFighter> CheckAttackZone();
    public void TakeHit();
}
