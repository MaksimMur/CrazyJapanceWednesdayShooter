using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour, IHealthSystem
{
    [SerializeField] private float maxHp = 100;
    private float currentHealth;
    public event System.Action onHpChanged = delegate { };

    private static PlayerCharacteristics Instance { get; set; }

    public void Awake()
    {
        Instance = this;
        currentHealth = maxHp;
    }
    public static Vector3 POI => Instance.transform.position;
    public static Transform POI_TRANSFORM => Instance.transform;

    public static void GetDamage(float damage)
    {

        Instance.currentHealth -= damage;
        Instance.onHpChanged?.Invoke();
    }
    public float MaxHeatlh => maxHp;
    public float CurrentHealth => currentHealth;
}
