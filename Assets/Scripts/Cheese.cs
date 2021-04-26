using System;
using UnityEngine;

public class Cheese : MonoBehaviour, ICollectable
{
    public event Action<Cheese> OnCollected;

    public float eatPoint;

    private void OnDestroy()
    {
        OnCollected?.Invoke(this);
    }
}
