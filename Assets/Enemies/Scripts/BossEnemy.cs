using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : Enemy
{
	protected override void InitializeEntity(int min, int max)
	{
		// health
		health = transform.AddComponent<Health>();
		int healthValue = 132;
		health.InitializeHealth(healthValue);
		health.onHealthChange += DidEntityDie;

		// items/inventory
		inventory = GetComponent<EnemyInventory>();

		// locomotion
		locomotionController = GetComponent<EnemyLocomotionController>();
		locomotionController.InitiializeController(this);

		// attack
		attackController = GetComponent<EnemyAttackController>();
		attackController.InitializeController(this);

		// state machine
		BuildStateMachine();

		// find player
		FindNewPlayerTarget();
	}
}
