using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 3.5f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
