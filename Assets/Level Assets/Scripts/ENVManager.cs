using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENVManager : MonoBehaviour
{
    /*
     * Need enemy list and listr of places enemies can spawn
     */
    [field: SerializeField] public List<Enemy> enemyList { get; private set; }
    [field: SerializeField] public List<EnemySpawn> enemySpawnPositions { get; private set; }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
