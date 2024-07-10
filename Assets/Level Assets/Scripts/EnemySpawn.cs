using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [field: SerializeField] public bool isOccupied { get; private set; } // if occupied, you can't spawn another enemy from here
    private WaitForSeconds waitTime = new WaitForSeconds(10.0f);

    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
    }

    public void OccupySpawn()
    {
        isOccupied = true;
        StartCoroutine(SetUnoccupied());
    }

    private IEnumerator SetUnoccupied()
    {
        yield return waitTime;
        isOccupied = false;
    }
}
