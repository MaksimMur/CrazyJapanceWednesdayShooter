using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem 
{
    public float MaxHeatlh { get; }
    public float CurrentHealth { get; }
    public event System.Action onHpChanged;
}
