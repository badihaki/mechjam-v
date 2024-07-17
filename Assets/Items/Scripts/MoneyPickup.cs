using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : ItemPickup
{
    [SerializeField] private int currencyAmount; 

    // Start is called before the first frame update
    void Start()
    {
        currencyAmount = UnityEngine.Random.Range(1, 10);
    }

	protected override void PickupItem(Player player)
	{
		base.PickupItem(player);
	}
}
