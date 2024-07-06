using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody physicsController;
    [SerializeField] private float speed = 8.0f;

    public void InitializeProjectile()
    {
        physicsController = transform.AddComponent<Rigidbody>();
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
}
