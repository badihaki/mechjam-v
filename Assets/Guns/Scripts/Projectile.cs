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

    private bool reflected = false;
    private bool reversed = false;
    private  WaitForSeconds reversedWait = new WaitForSeconds(0.35f);

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
            if (!reflected)
            {
                physicsController.velocity = transform.forward * speed;
            }
            if (reversed)
            {
                physicsController.velocity = transform.forward * -speed;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>()) return;
        if (other.GetComponent<PlayerSword>())
        {
            ReflectProj();
            return;
        }
        if (other.transform != controllingEntity && !reversed)
        {
            other.GetComponent<IDamageable>()?.Damage(transform, damage, forces);
            if (impactVfx)
            {
                Instantiate(impactVfx, transform.position, Quaternion.identity);
            }
            else print("didn't instantiate impact");
                Destroy(gameObject);
        }
        else if (reversed && !other.GetComponent<Player>())
        {
            other.GetComponent<IDamageable>()?.Damage(transform, damage, forces);
            if (impactVfx)
            {
                Instantiate(impactVfx, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void ReflectProj()
    {
        print("reflected!!");
        reflected = true;
        physicsController.velocity = Vector3.zero;
        StartCoroutine(BeginReversedTravel());
    }

    private IEnumerator BeginReversedTravel()
    {
        yield return reversedWait;
        speed *= 2;
        reversed = true;
    }
}
