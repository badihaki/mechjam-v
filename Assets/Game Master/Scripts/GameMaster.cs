using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Entity { get; private set; }
    private void OnEnable()
    {
        if(Entity != this && Entity!=null)
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(Entity == null)
        {
            Entity = this;
        }
        else
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
