using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayState : PlayerState
{
    public PlayerGameplayState(Player _player, PlayerFSM _fsm, string _animName) : base(_player, _fsm, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // player.AttackController.SelectGunStyle();
        player.AttackController.TriggerTriggerGunStyleSwitch();
        Debug.Log("selecting style");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (player.LocomotionController.movementEnabled) player.LocomotionController.ControlMovement();
        player.LocomotionController.CanDashBoost();
        player.AttackController.CanShoot();
        player.AttackController.CanMelee();
        player.AttackController.CanReload();
	}
}
