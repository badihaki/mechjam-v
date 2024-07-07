using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionController : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private Vector2 minAndMaxRange = new Vector2(3.0f, 5.0f);
    private NavMeshAgent navAgent;
    private Rigidbody physicsController;
    [SerializeField] private Transform navigationTarget;

    public void InitiializeController(Enemy _enemy)
    {
        enemy = _enemy;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        navAgent.stoppingDistance = minAndMaxRange.x;
        physicsController = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindNewNavTarget()
    {
        if (!enemy.target)
        {

            print("need target");
        }
        else
        {
            navigationTarget = enemy.target;

		}
        navAgent.SetDestination(navigationTarget.position);
    }

    public void ZeroVelocity()
    {
        physicsController.velocity = Vector3.zero;
        navAgent.isStopped = true;
    }
}
