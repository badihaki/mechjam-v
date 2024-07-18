using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform controllingEntity;
    private Rigidbody physicsController;
    [field: SerializeField] public int damage { get; private set; }
    [SerializeField] private float speed = 8.0f;
    [field: SerializeField] public Vector2 forces { get; private set; }
    [SerializeField] private GameObject impactVfx;
    [SerializeField] private AudioClip impactSoundfx;

    private bool reflected = false;
    private bool reversed = false;
    private  WaitForSeconds reversedWait = new WaitForSeconds(0.35f);

    [SerializeField] private float lifeTime = 4.0f;

    public void InitializeProjectile(Transform creator, int dmg)
    {
        physicsController = transform.AddComponent<Rigidbody>();

        controllingEntity = creator;
        damage = dmg;

        float forceX = Random.Range(3, 7);
        float forceY = Random.Range(3.5f, 10);
        forces = new Vector2(forceX, forceY);
        physicsController.useGravity = false;
        Destroy(gameObject, lifeTime);
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
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (other.GetComponent<Projectile>()) return;
        if (other.GetComponent<PlayerSword>())
        {
            ReflectProj();
            return;
        }
        if (other.transform != controllingEntity && !reversed)
        {
            damageable?.Damage(transform, damage, forces);
            if (impactVfx)
            {
                Instantiate(impactVfx, transform.position, Quaternion.identity);
            }
            else print("didn't instantiate impact");
                Destroy(gameObject);
            if (damageable != null && impactSoundfx)
            {
                GameMaster.Entity.audioController.PlayOneShot(impactSoundfx, 0.785f);
            }
        }
        else if (reversed && !other.GetComponent<Player>())
        {
            damageable?.Damage(transform, damage, forces);
            if (damageable != null && impactVfx)
            {
                Instantiate(impactVfx, transform.position, Quaternion.identity);
                GameMaster.Entity.audioController.PlayOneShot(impactSoundfx, 0.785f);
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
