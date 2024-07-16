using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        print($"interacting with {transform.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInteractionController interactionController = other.GetComponent<PlayerInteractionController>();
        if(interactionController != null)
        {
            interactionController.DetectNewInteractable(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerInteractionController interactionController = other.GetComponent<PlayerInteractionController>();
        if (interactionController != null)
        {
            interactionController.RemoveInteractable(this);
        }
    }
}
