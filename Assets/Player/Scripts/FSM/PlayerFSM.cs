using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    [field: SerializeField] public PlayerState currentState { get; private set; }
    private bool ready = false;

    // state list
    public PlayerGameplayState gameplayState { get; private set; }
    public PlayerMenuState menuState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }

    public void InitializeStateMachine(Player player)
    {
        gameplayState = new PlayerGameplayState(player, this, "gameplay");
        menuState = new PlayerMenuState(player, this, "menu");
        hurtState = new PlayerHurtState(player, this, "hurt");

        currentState = menuState;
        currentState.Enter();
        ready = true;
    }

    public void ChangeState(PlayerState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    private void Update()
    {
        if (ready) currentState.LogicUpdate();
    }
    private void LateUpdate()
    {
        if (ready) currentState.PhysicsUpdate();
    }
}
