using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player){
            PickupItem(player);
            Destroy(gameObject);
        }
    }

    protected virtual void PickupItem(Player player)
    {
        print($"{player.name} is picking up {name}");
    }
}
