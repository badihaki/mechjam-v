using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [field: SerializeField] public int playerId { get; private set; }
    [field: SerializeField] public GameObject playerGameplayPrefab { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetID(int pId) => playerId = pId;

	// Update is called once per frame
	void Update()
    {
        
    }
}
