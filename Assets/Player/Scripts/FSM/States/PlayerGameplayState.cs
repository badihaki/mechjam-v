using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayState : PlayerState
{
    public PlayerGameplayState(Player _player, PlayerFSM _fsm, string _animName) : base(_player, _fsm, _animName)
    {
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.locomotionController.ControlMovement();
        player.attackController.CanShoot();
        player.attackController.CanMelee();
	}
}
