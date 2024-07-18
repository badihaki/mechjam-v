using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAmmoPickup : ItemPickup
{
    private int ammoStockAmt;
    // Start is called before the first frame update
    void Start()
    {
        ammoStockAmt = UnityEngine.Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PickupItem(Player player)
    {
        base.PickupItem(player);
        player.AttackController.GetMoreAmmoStocks(ammoStockAmt);
    }
}
