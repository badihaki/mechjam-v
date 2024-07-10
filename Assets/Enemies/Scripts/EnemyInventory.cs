using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{
    [SerializeField] private List<ItemPickup> items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        if (CanDropItem())
        {
            print($"dropping item");
            ItemPickup selectedItem = items[UnityEngine.Random.Range(0, items.Count - 1)];
            ItemPickup itemObj = Instantiate(selectedItem, transform.position, transform.rotation);
            float forceX = UnityEngine.Random.Range(-2.0f, 2.0f);
            float forceY = UnityEngine.Random.Range(3.0f, 8.0f);
            float forceZ = UnityEngine.Random.Range(-3.0f, 3.0f);
            Vector3 spawnForce = new Vector3(forceX, forceY, forceZ);
            itemObj.GetComponent<Rigidbody>().AddForce(spawnForce, ForceMode.Impulse);
        }
        else
        {
            print("no item to drop");
        }
    }

    private bool CanDropItem()
    {
        int roll = UnityEngine.Random.Range(0, 100);
        if (roll <= 55) return true;
        else return false;
    }
}
