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
        health.onHealthChange += DidEntityDie;
    }

    private void OnEnable()
    {
        if (health) health.onHealthChange += DidEntityDie;
    }
    private void OnDisable()
    {
        health.onHealthChange -= DidEntityDie;
    }

    private void DidEntityDie(int health)
    {
        if(health <= 0)
        {
            print("entity died");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(Transform entity, int damage, Vector2 force)
    {
        health.ChangeHealth(health.currentHealth - damage);
    }
}
