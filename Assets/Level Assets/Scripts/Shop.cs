using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    private CinemachineVirtualCamera cinematographer;
    private CinemachineVirtualCamera regCam;
    private int priorityRating;
    private Canvas shopUi;

    // Start is called before the first frame update
    void Start()
    {
        cinematographer = transform.Find("ShopCam").GetComponent<CinemachineVirtualCamera>();
        cinematographer.Priority = 0;
        regCam = GameObject.Find("VCam").GetComponent<CinemachineVirtualCamera>();
        priorityRating = regCam.Priority;
        shopUi = transform.Find("ShopUI").GetComponent<Canvas>();
        shopUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        cinematographer.Priority = 10;
        regCam.Priority = 0;

        shopUi.gameObject.SetActive(true);

        print($"interacting with {transform.name}");
        GameMaster.Entity.playerList.ForEach(player =>
        {
            player.EnterMenu();
        });
    }

    public void LeaveShop()
    {
        regCam.Priority = priorityRating;
        cinematographer.Priority = 0;
        
        shopUi.gameObject.SetActive(false);

        GameMaster.Entity.playerList.ForEach(player =>
        {
            player.ReturnToGameplayState();
        });
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
