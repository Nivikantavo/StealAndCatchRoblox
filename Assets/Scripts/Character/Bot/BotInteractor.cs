using UnityEngine;

public class BotInteractor : IInteractor
{
    public Transform SelfTransform { get; private set; }
    public Transform HouseTransform { get; private set; }

    public BotInteractor(Transform selfTransform, Transform houseTransform)
    {
        SelfTransform = selfTransform;
        HouseTransform = houseTransform;
    }
}
