using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySceneStartState", menuName = "Game/Enemy/Enemy State/Scene Start"), Serializable]
public class EnemySceneEnterState : EnemyState
{
	private bool endAnim;
    public override void EnterState()
    {
        base.EnterState();
        endAnim = false;
    }

    public override void CheckTransitions()
	{
		base.CheckTransitions();

		if(Time.time >= stateStartTime + 2.55f || endAnim)
		{
			stateMachine.ChangeState(stateMachine.gameplayState);
		}
	}

    public override void AnimationEnd()
    {
        base.AnimationEnd();
		endAnim = true;
    }
}
