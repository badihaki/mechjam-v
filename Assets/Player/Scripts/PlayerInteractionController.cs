using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject interactiveIcon;
    private IInteractable interactable;
  
    public void InitializeController(Player _player)
    {
        player = _player;
        interactiveIcon = transform.Find("interactionIcon").gameObject;
        interactiveIcon.SetActive(false);
    }

    public void DetectNewInteractable(IInteractable interactiveEntity)
    {
        // print($"getting new interactable {interactiveEntity}");
        interactable = interactiveEntity;
        interactiveIcon.SetActive(true);
    }

    public void RemoveInteractable(IInteractable interactiveEntity)
    {
        if(interactable == interactiveEntity)
        {
            interactable = null;
            interactiveIcon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
            UseInteract();
    }

    private void UseInteract()
    {
        if (player.Controls.interactInput)
        {
            if(interactable != null)
            {
                interactable.Interact(player);
            }
            player.Controls.UseInteract();
        }
    }
}
