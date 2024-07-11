using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    Vector2 forces = new Vector2 (3, 5);
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
        if (other.GetComponent<Player>()) return;
        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable?.Damage(transform, 1, forces);
        /*Projectile projectile = other.GetComponent<Projectile>();
        projectile?.ReflectProj();*/
    }
}
