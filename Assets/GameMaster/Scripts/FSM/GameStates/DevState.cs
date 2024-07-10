using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevState : GameplayState
{
	public DevState(GMFiniteStateMachine fsm, string name) : base(fsm, name)
	{
	}

	public override void EnterState()
	{
		GameMaster.Entity.playerList.ForEach(player =>
		{
			player.StartDevMode();
		});

		base.EnterState();
	}
}
