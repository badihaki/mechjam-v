using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGameplayState", menuName = "Game/Enemy/Enemy State/Regular Gameplay"), Serializable]
public class EnemyGameplayState : EnemyState
{
    protected float newDestinationTimer;

    public override void EnterState()
    {
        base.EnterState();

        if (!entity.locomotionController.TryMoveToTarget())
        {
            entity.FindNewPlayerTarget();
            CreateNewDestinationWait(2.3f, 5.5f);
        }
    }

    private void CreateNewDestinationWait(float min, float max)
    {
        float newTime = UnityEngine.Random.Range(min, max);
        // Debug.Log($"adding {newTime} to {entity.name}'s wait time");
        newDestinationTimer = newTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (entity.attackController.isAttacking)
        {
            entity.attackController.ControlAttackTimer();
        }
        else
        {
            entity.attackController.ControlAttackWaitTimer();
        }
        ControlDestinationTimer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.locomotionController.LookAtTarget();
    }

    private void ControlDestinationTimer()
    {
        if(newDestinationTimer > 0)
        {
            newDestinationTimer -= Time.deltaTime;
        }
        else
        {
            if (!entity.locomotionController.TryMoveToTarget())
            {
                entity.FindNewPlayerTarget();
                CreateNewDestinationWait(1.0f, 3.0f);
            }
            CreateNewDestinationWait(2.35f, 6.75f);
        }
    }
}
