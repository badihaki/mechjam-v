using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public Health health { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeEntity(new Vector2(5, 12));
    }

    private void InitializeEntity(Vector2 healthRange)
    {
        health = transform.AddComponent<Health>();
        int healthValue = (int)Mathf.Round(UnityEngine.Random.Range(healthRange.x, healthRange.y));
        health.InitializeHealth(healthValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(Transform entity, int damage, Vector2 force)
    {
        // change health
    }
}
