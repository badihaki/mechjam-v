using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private Enemy enemy;
    [field: SerializeField] public GunTemplate gun { get; private set; }
    [field: SerializeField] public bool isAttacking { get; private set; }
    [SerializeField] private float waitToAttackTimer;
    [SerializeField] private float isAttackingTimer;
    [SerializeField] private float attackFireRate;
    [SerializeField] private Transform shootPoint;


    public void InitializeController(Enemy _enemy)
    {
        attackFireRate = gun.fireRate;
        enemy = _enemy;
        waitToAttackTimer = UnityEngine.Random.Range(1.0f, 5.5f);
        isAttacking = false;
        isAttackingTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlAttackWaitTimer()
    {
        if(waitToAttackTimer > 0)
        {
            waitToAttackTimer -= Time.deltaTime;
        }
        else
        {
            waitToAttackTimer = 0;
            float min = gun.fireRate * UnityEngine.Random.Range(3.0f, 5.0f);
            float max = min * UnityEngine.Random.Range(2.0f, 3.0f);

            isAttackingTimer = UnityEngine.Random.Range(min, max);

            isAttacking = true;
        }
    }

    public void ControlAttackTimer()
    {
        if (isAttackingTimer > 0)
        {
            isAttackingTimer -= Time.deltaTime;
            
            // attack fireRate timer
            // when it get to 0 this entity attacks
            if(attackFireRate>0)
            {
                attackFireRate -= Time.deltaTime;
            }
            else
            {
                attackFireRate = gun.fireRate;
                Attack();
            }
        }
        else
        {
            isAttackingTimer = 0;
            attackFireRate = 0;
            float min = gun.fireRate * UnityEngine.Random.Range(3.0f, 5.0f);
            float max = min * UnityEngine.Random.Range(2.0f, 3.0f);
            
            waitToAttackTimer = UnityEngine.Random.Range(min, max);

            isAttacking = false;
        }
    }

    protected virtual void Attack()
    {
        print($"{enemy.name} is attacking");

        Projectile projectile = Instantiate(gun.enemyProjectile, shootPoint.position, transform.rotation).GetComponent<Projectile>();
        projectile.InitializeProjectile(enemy.transform, gun.damage);
    }
}
