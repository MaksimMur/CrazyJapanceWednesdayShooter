using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private float _timeSpawn = -2;

    private void OnEnable()
    {
        _timeSpawn = Time.time;
    }

    private void Update()
    {
        if (_timeSpawn + lifeTime < Time.time)
        {
            Destroy(this);
        }
    }
}
