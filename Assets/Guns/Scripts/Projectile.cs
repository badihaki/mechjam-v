using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform controllingEntity;
    private Rigidbody physicsController;
    [SerializeField] private int damage;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private Vector2 forces;
    [SerializeField] private GameObject impactVfx;

    public void InitializeProjectile(Transform creator, int dmg)
    {
        physicsController = transform.AddComponent<Rigidbody>();

        controllingEntity = creator;
        damage = dmg;

        float forceX = Random.Range(3, 7);
        float forceY = Random.Range(3.5f, 10);
        forces = new Vector2(forceX, forceY);
        physicsController.useGravity = false;
        Destroy(gameObject, 4.0f);
    }

	private void FixedUpdate()
	{
        if (physicsController != null)
        {
            physicsController.velocity = transform.forward * speed;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform != controllingEntity)
        {
            other.GetComponent<IDamageable>()?.Damage(transform, damage, forces);
            if (impactVfx)
            {
                Instantiate(impactVfx, transform.position, Quaternion.identity);
            }
            else print("didn't instantiate impact");
            Destroy(gameObject);
            
        }
    }
}
