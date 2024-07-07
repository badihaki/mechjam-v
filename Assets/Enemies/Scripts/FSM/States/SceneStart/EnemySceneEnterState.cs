using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySceneStartState", menuName = "Game/Enemy/Enemy State/Scene Start"), Serializable]
public class EnemySceneEnterState : EnemyState
{
	public override void CheckTransitions()
	{
		base.CheckTransitions();

		if(Time.time >= stateStartTime + 2.55f)
		{
			stateMachine.ChangeState(stateMachine.gameplayState);
		}
	}
}
