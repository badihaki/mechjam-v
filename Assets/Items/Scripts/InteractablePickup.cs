using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : ItemPickup, IInteractable
{
	public Rigidbody physicsController { get; protected set; }


	// Start is called before the first frame update
	void Start()
    {
		if(!physicsController)
			physicsController = GetComponent<Rigidbody>();
    }

	public virtual void InitializePickup()
	{
		physicsController = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		PlayerInteractionController interactionController = other.GetComponent<PlayerInteractionController>();
		if (interactionController != null)
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

	public virtual void Interact(Player player)
	{
		throw new System.NotImplementedException();
	}
}
