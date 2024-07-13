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
    private Animator animationController;
    [SerializeField] private GameObject goGoArrow;
    [SerializeField] private Transform continueArrow;
    [field: SerializeField] public Transform p1Start { get; private set; }
    [field: SerializeField] public Transform p2Start { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animationController = GetComponent<Animator>();
        continueArrow = transform.Find("Arrow");
        continueArrow.gameObject.SetActive(false);
        p1Start = transform.Find("P1Start");
        p2Start = transform.Find("P2Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishBoard()
    {
        continueArrow.gameObject.SetActive(true);
        animationController.SetBool("doorOpened", true);
    }
}
