using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private Player player;
    private IInteractable interactable;
  
    public void InitializeController(Player _player)
    {
        player = _player;
    }

    public void DetectNewInteractable(IInteractable interactiveEntity)
    {
        print($"getting new interactable {interactiveEntity}");
        interactable = interactiveEntity;
    }

    public void RemoveInteractable(IInteractable interactiveEntity)
    {
        if(interactable == interactiveEntity)
        {
            interactable = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
