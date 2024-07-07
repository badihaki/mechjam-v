using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionController : MonoBehaviour
{
    private Enemy enemy;
    [field: SerializeField] public float speed { get; private set; } = 4.75f;
    [field: SerializeField] public float dashSpeed { get; private set; } = 6.075f;
    [field: SerializeField] public Vector2 minAndMaxRange { get; private set; } = new Vector2(10.25f, 13.557f);
    private NavMeshAgent navAgent;
    private Rigidbody physicsController;

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

    public bool TryMoveToTarget()
    {
        if (enemy.target && enemy.target.position.magnitude > minAndMaxRange.y)
        {
            if(navAgent.isStopped) navAgent.isStopped = false;
            SetAgentSpeed();
            navAgent.SetDestination(enemy.target.position);
            return true;
        }
        navAgent.isStopped = true;
        return false;
    }

    public void SetAgentSpeed()
    {
        float distance = Vector3.Distance(enemy.target.position, transform.position);
        if (distance > minAndMaxRange.y + 2.35f) navAgent.speed = dashSpeed;
        else navAgent.speed = speed;
    }

    public void LookAtTarget()
    {
        if (enemy.target) transform.LookAt(enemy.target.position);
    }

    public void ZeroVelocity()
    {
        physicsController.velocity = Vector3.zero;
        navAgent.isStopped = true;
    }
}
