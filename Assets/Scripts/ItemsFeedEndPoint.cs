using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFeedEndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BrainrotMob>(out BrainrotMob brainrot))
        {
            brainrot.Stop();
            brainrot.gameObject.SetActive(false);
        }
    }
}
