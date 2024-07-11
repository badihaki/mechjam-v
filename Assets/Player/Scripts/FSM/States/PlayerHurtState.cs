using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player _player, PlayerFSM _fsm, string _animName) : base(_player, _fsm, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
}
