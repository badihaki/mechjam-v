using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematicState : PlayerState
{
    public PlayerCinematicState(Player _player, PlayerFSM _fsm, string _animName) : base(_player, _fsm, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.LocomotionController.ZeroOutVelocity();
    }
}
